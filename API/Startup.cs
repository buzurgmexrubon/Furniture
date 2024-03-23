namespace API;

public static class Startup
{
  private const string _defaultCorsPolicyName = "localhost";

  public static void AddDIServices(this WebApplicationBuilder builder)
  {
    #region Default DI Services
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(option =>
    {
      option.SwaggerDoc("v1", new OpenApiInfo { Title = "Furniture API", Version = "v1" });
      option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
      {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
      });
      option.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    new string[]{}
                }
            });
      var filePath = Path.Combine(AppContext.BaseDirectory, "Furniture_API.xml");
      if (File.Exists(filePath))
      {
        option.IncludeXmlComments(filePath);
      }
    });
    #endregion

    #region DBContext
    builder.Services.AddDbContext<AppDbContext>(options =>
    {
      options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL"));
    });
    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    #endregion

    #region Identity
    builder.Services.AddIdentity<User, IdentityRole>(options =>
    {
      options.Password.RequireDigit = true;
      options.Password.RequiredLength = 6;
      options.Password.RequireNonAlphanumeric = false;
      options.Password.RequireUppercase = false;
    }).AddEntityFrameworkStores<AppDbContext>()
      .AddDefaultTokenProviders();

    //add role manager to DI

    #endregion

    #region Custom DI Services
    builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

    builder.Services.AddTransient<ICategoryService, CategoryService>();
    builder.Services.AddTransient<IColorService, ColorService>();
    builder.Services.AddTransient<IImageService, ImageService>();
    builder.Services.AddTransient<IUserService, UserService>();
    builder.Services.AddTransient<IFurnitureService, FurnitureService>();

    builder.Services.AddTransient<MobileBLL.Interfaces.IMobileService,
                                  MobileBLL.Services.MobileService>();
    #endregion

    #region CORS Policy for all origins
    builder.Services.AddCors(options =>
    {
      options.AddPolicy(_defaultCorsPolicyName, builder =>
      {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
      });
    });
    #endregion

    #region JWT
    var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
    var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>() ?? "key";
    builder.Services.AddAuthentication(options =>
    {
      options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
        .AddJwtBearer(options =>
        {
          options.TokenValidationParameters = new()
          {
            ValidateIssuer = true,
            ValidIssuer = jwtIssuer,
            ValidateAudience = true,
            ValidAudience = jwtIssuer,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            ClockSkew = TimeSpan.Zero,
            RoleClaimType = ClaimTypes.Role
          };
        });

    #endregion

    #region Add rate limiting to DI services

    builder.Services.Configure<IpRateLimitOptions>
        (builder.Configuration.GetSection("IpRateLimit"));
    builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
    builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
    builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
    builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
    builder.Services.AddMemoryCache();
    builder.Services.AddHttpContextAccessor();
    #endregion

    #region API Versioning
    builder.Services.AddApiVersioning(options =>
    {
      options.ReportApiVersions = true;
      options.DefaultApiVersion = new ApiVersion(1, 0);
      options.AssumeDefaultVersionWhenUnspecified = true;
      options.ApiVersionReader = new UrlSegmentApiVersionReader();
    });
    #endregion

    #region Redis
    builder.Services.Configure<ConfigurationOptions>(builder.Configuration.GetSection("RedisCacheOptions"));
    builder.Services.AddStackExchangeRedisCache(setupAction =>
    {
      setupAction.Configuration = builder.Configuration.GetConnectionString("RedisConnectionString");
    });
    #endregion

  }

  public static void AddMiddleware(this WebApplication app)
  {
    app.UseIpRateLimiting();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHsts();
    app.UseHttpsRedirection();
    app.UseCors(_defaultCorsPolicyName);
    app.UseRouting();
    app.UseStaticFiles();
    app.UseAuthentication();
    //disabled for development
    // app.UseMiddleware<JwtValidation>();
    app.UseAuthorization();
    var versionSet = app.NewApiVersionSet()
                        .HasApiVersion(new ApiVersion(1, 0))
                        .Build();
    app.MapControllers().WithApiVersionSet(versionSet);
    app.SeedRolesToDatabase().Wait();
  }

  private static async Task SeedRolesToDatabase(this WebApplication app)
  {
    var scope = app.Services.CreateScope();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { "Admin", "User", "SuperAdmin" };
    foreach (var role in roles)
    {
      if (!roleManager.RoleExistsAsync(role).Result)
      {
        var result = roleManager.CreateAsync(new IdentityRole(role)).Result;
      }
    }

    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    var admin = new User
    {
      UserName = "+998999999999",
      PhoneNumberConfirmed = true,
      PhoneNumber = "+998999999999",
      Address = "Database",
      AvatarUrl = "https://cdn-icons-png.flaticon.com/512/5564/5564849.png",
      BirthDate = DateTime.Now,
      Gender = 0
    };
    var adminPassword = "Adm1nj0nm1san$";
    var user = await userManager.FindByNameAsync(admin.UserName);
    if (user == null)
    {
      var createAdmin = await userManager.CreateAsync(admin, adminPassword);
      if (createAdmin.Succeeded)
      {
        await userManager.AddToRoleAsync(admin, "SuperAdmin");
      }
    }
  }
}

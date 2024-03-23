using Microsoft.AspNetCore.Authorization;

namespace API.Core;

public class JwtValidation
{
  private readonly RequestDelegate _next;

  public JwtValidation(RequestDelegate next)
  {
    _next = next;
  }

  public async Task Invoke(HttpContext context)
  {
    var allowAnonymous = context.GetEndpoint()?.Metadata.GetMetadata<AllowAnonymousAttribute>() != null;

    if (allowAnonymous)
    {
      await _next(context);
      return;
    }

    var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

    if (string.IsNullOrEmpty(token))
    {
      context.Response.StatusCode = 401;
      await context.Response.WriteAsync("Unauthorized");
      return;
    }

    var dbContext = context.RequestServices.GetRequiredService<AppDbContext>();
    var tokens = dbContext.UserTokens.ToList();

    if (tokens.Any(x => x.Value == token))
    {
      await _next(context);
      return;
    }

    context.Response.StatusCode = 401;
    await context.Response.WriteAsync("Unauthorized");
  }
}

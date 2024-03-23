using BLL.Common;

namespace API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize(Roles = "User, Admin, SuperAdmin")]
public class AuthController(IUserService userService)
    : ControllerBase
{
  private readonly IUserService _userService = userService;

  /// <summary>
  /// [All] Login qilish (Telefon raqam tasdiqlangan bo'lishi kerak)
  /// </summary>
  [HttpPost("login")]
  [AllowAnonymous]
  [ProducesResponseType(typeof(LoginResult), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> Login([FromBody] LoginUserDto request)
  {
    try
    {
      var response = await _userService.LoginAsync(request);
      return Ok(response);
    }
    catch (ArgumentNullException ex)
    {
      return NotFound(ex.Message);
    }
    catch (FurnitureException ex)
    {
      return BadRequest(ex.Message);
    }
    catch (Exception ex)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
    }
  }

  /// <summary>
  /// [User] Ro'yxatdan o'tish
  /// </summary>
  [HttpPost("register")]
  [AllowAnonymous]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> Register([FromBody] RegisterUserDto request)
  {
    try
    {
      await _userService.CreateAsync(request, UserRoles.User);
      return Ok();
    }
    catch (ArgumentNullException ex)
    {
      return NotFound(ex.Message);
    }
    catch (FurnitureException ex)
    {
      return BadRequest(ex.Message);
    }
    catch (Exception ex)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
    }
  }

  /// <summary>
  /// [All] Telefon raqamni tasdiqlash uchun sms yuborish
  /// </summary>
  [HttpPost("send-otp")]
  [AllowAnonymous]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> SendOtp([FromBody] SendOtpDto request)
  {
    try
    {
      await _userService.SendOtpAsync(request);
      return Ok();
    }
    catch (FurnitureException ex)
    {
      return BadRequest(ex.Message);
    }
    catch (Exception ex)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
    }
  }

  /// <summary>
  /// [All] Kodni tasdiqlash 2 daqiqa
  /// </summary>
  [HttpPost("verify-otp")]
  [AllowAnonymous]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> VerifyOtp([FromBody] ConfirmPhoneNumberDto request)
  {
    try
    {
      await _userService.ConfirmPhoneNumberAsync(request);
      return Ok();
    }
    catch (ArgumentNullException ex)
    {
      return NotFound(ex.Message);
    }
    catch (FurnitureException ex)
    {
      return BadRequest(ex.Message);
    }
    catch (Exception ex)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
    }
  }

  /// <summary>
  /// [All] Logout qilish joriy tokenni o'chiradi
  /// </summary>
  [HttpPut("logout")]
  [Authorize(Roles = "User, Admin, SuperAdmin")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> Logout([FromBody] LoginUserDto dto)
  {
    try
    {
      await _userService.LogoutAsync(dto);
      return Ok();
    }
    catch (ArgumentNullException ex)
    {
      return NotFound(ex.Message);
    }
    catch (Exception ex)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
    }
  }

  /// <summary>
  /// [All] Telefon raqamni o'zgartirish
  /// </summary>
  [HttpPut("change-password")]
  [Authorize(Roles = "User, Admin, SuperAdmin")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
  {
    try
    {
      await _userService.ChangePasswordAsync(dto);
      return Ok();
    }
    catch (ArgumentNullException ex)
    {
      return NotFound(ex.Message);
    }
    catch (FurnitureException ex)
    {
      return BadRequest(ex.Message);
    }
    catch (Exception ex)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
    }
  }

  /// <summary>
  /// [SuperAdmin , Admin] Foydalanuvchini o'chirish
  /// </summary>
  /// <param name="dto"></param>
  /// <returns></returns>
  [HttpDelete("delete")]
  //[Authorize(Roles = "User, Admin, SuperAdmin")] // Development uchun ochiq qoldirildi
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> Delete([FromBody] LoginUserDto dto)
  {
    try
    {
      await _userService.DeleteAccountAsync(dto);
      return Ok();
    }
    catch (ArgumentNullException ex)
    {
      return NotFound(ex.Message);
    }
    catch (FurnitureException ex)
    {
      return BadRequest(ex.Message);
    }
    catch (Exception ex)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
    }
  }

  [HttpPost("profile/set-avatar")]
  [Authorize(Roles = "User, Admin, SuperAdmin")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> SetProfileImage(SetAvatarDto avatarDto)
  {
    try
    {
      await _userService.SetProfilePictureAsync(avatarDto);
      return Ok();
    }
    catch (ArgumentNullException ex)
    {
      return NotFound(ex.Message);
    }
    catch (FurnitureException ex)
    {
      return BadRequest(ex.Message);
    }
    catch (Exception ex)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
    }
  }

  [HttpPut("profile/change-avatar")]
  [Authorize(Roles = "User, Admin, SuperAdmin")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> ChangeProfileImage(SetAvatarDto avatarDto)
  {
    try
    {
      await _userService.UpdateProfileImageAsync(avatarDto);
      return Ok();
    }
    catch (ArgumentNullException ex)
    {
      return NotFound(ex.Message);
    }
    catch (FurnitureException ex)
    {
      return BadRequest(ex.Message);
    }
    catch (Exception ex)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
    }
  }

  [HttpDelete("profile/delete-avatar/{userId}")]
  [Authorize(Roles = "User, Admin, SuperAdmin")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> DeleteProfileImage([FromRoute] string userId)
  {
    try
    {
      await _userService.DeleteProfilePictureAsync(userId);
      return Ok();
    }
    catch (ArgumentNullException ex)
    {
      return NotFound(ex.Message);
    }
    catch (FurnitureException ex)
    {
      return BadRequest(ex.Message);
    }
    catch (Exception ex)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
    }
  }


  [HttpGet("valitade-token")]
  [Authorize(Roles = "User, Admin, SuperAdmin")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  public IActionResult ValidateToken()
  {
    return Ok();
  }

  [HttpPut("update-profile")]
  [Authorize(Roles = "User, Admin, SuperAdmin")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> UpdateProfile([FromBody] UpdateUserDto dto)
  {
    try
    {
      await _userService.UpdateUserAsync(dto);
      return Ok();
    }
    catch (ArgumentNullException ex)
    {
      return NotFound(ex.Message);
    }
    catch (FurnitureException ex)
    {
      return BadRequest(ex.Message);
    }
    catch (Exception ex)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
    }
  }
}
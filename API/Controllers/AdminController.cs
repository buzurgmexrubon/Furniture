using BLL.Common;

namespace API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AdminController(IUserService userService)
    : ControllerBase
{
  private readonly IUserService _userService = userService;

  /// <summary>
  /// [SuperAdmin] Yangi admin yaratish
  /// </summary>
  [HttpPost("create")]
  [Authorize(Roles = "SuperAdmin")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> CreateAdmin([FromBody] NewAdminDto request)
  {
    try
    {
      await _userService.CreateAdminAsync(request);
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

  [HttpPut("update")]
  [Authorize(Roles = "SuperAdmin, Admin")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> UpdateAdmin([FromBody] UpdateUserDto request)
  {
    try
    {
      await _userService.UpdateUserAsync(request);
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

  [HttpGet("get-all")]
  [Authorize(Roles = "SuperAdmin")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> GetAllAdmins()
  {
    try
    {
      var admins = await _userService.GetUsersAsync(UserRoles.Admin);
      return Ok(admins);
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

  [HttpDelete("{id}")]
  [Authorize(Roles = "SuperAdmin")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> DeleteAdmin(string id)
  {
    try
    {
      await _userService.DeleteUserAsync(id);
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

  [HttpPut("activate/{userId}")]
  [Authorize(Roles = "SuperAdmin")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> ActivateAdmin(string userId)
  {
    try
    {
      await _userService.ActivateAdminAsync(userId);
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

  [HttpPut("reset-password/{userId}")]
  [Authorize(Roles = "SuperAdmin")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> ResetPassword(string userId)
  {
    try
    {
      await _userService.ResetPassword(userId);
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
using MobileBLL.Interfaces;
using MobileDTOs.Common;
using MobileDTOs.FeedbackDtos;

namespace API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class MobileController(IMobileService mobileService)
    : ControllerBase
{
  private readonly IMobileService _mobileService = mobileService;

  #region Category

  [HttpGet("categories/{lang}")]
  [AllowAnonymous]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> GetCategories(string lang)
  {
    try
    {
      var categories = await _mobileService.Categories.GetCategoriesAsync(lang.ToLanguage());
      return Ok(categories);
    }
    catch (Exception ex)
    {
      return StatusCode(500, ex.Message);
    }
  }

  [HttpGet("categories/{id}/{lang}")]
  [AllowAnonymous]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> GetCategory(int id, string lang)
  {
    try
    {
      var category = await _mobileService.Categories.GetSingleCategoryAsync(id, lang.ToLanguage());
      return Ok(category);
    }
    catch (ArgumentNullException ex)
    {
      return NotFound(ex.InnerException);
    }
    catch (Exception ex)
    {
      return StatusCode(500, ex.Message);
    }
  }

  #endregion

  #region Furniture

  [HttpGet("furnitures/{lang}")]
  [AllowAnonymous]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> GetFurnitures(string lang)
  {
    try
    {
      var furnitures = await _mobileService.Furnitures.GetFurnituresAsync(lang.ToLanguage());
      return Ok(furnitures);
    }
    catch (Exception ex)
    {
      return StatusCode(500, ex.Message);
    }
  }

  [HttpGet("furnitures/{id}/{lang}")]
  [AllowAnonymous]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> GetFurniture(int id, string lang)
  {
    try
    {
      var furniture = await _mobileService.Furnitures.GetSingleFurnitureAsync(id, lang.ToLanguage());
      return Ok(furniture);
    }
    catch (ArgumentNullException ex)
    {
      return NotFound(ex.Message);
    }
    catch (Exception ex)
    {
      return StatusCode(500, ex.Message);
    }
  }



  [HttpGet("furniture/feedbacks/{furnitureId}")]
  [AllowAnonymous]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> GetFeedbacks(int furnitureId)
  {
    try
    {
      var feedbacks = await _mobileService.Feedbacks.GetFeedbacksListAsync(furnitureId);
      return Ok(feedbacks);
    }
    catch (ArgumentNullException ex)
    {
      return NotFound(ex.Message);
    }
    catch (Exception ex)
    {
      return StatusCode(500, ex.Message);
    }
  }

  #endregion

  #region Feedback

  [HttpGet("feedbacks/{id}")]
  [AllowAnonymous]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> GetFeedback(int id)
  {
    try
    {
      var feedback = await _mobileService.Feedbacks.GetFeedbackAsync(id);
      return Ok(feedback);
    }
    catch (ArgumentNullException ex)
    {
      return NotFound(ex.Message);
    }
    catch (Exception ex)
    {
      return StatusCode(500, ex.Message);
    }
  }

  [HttpPost("feedbacks")]
  [AllowAnonymous]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> CreateFeedback(AddFeedbackDto feedbackDto)
  {
    try
    {
      var feedback = await _mobileService.Feedbacks.CreateFeedbackAsync(feedbackDto);
      return Ok(feedback);
    }
    catch (ArgumentNullException ex)
    {
      return BadRequest(ex.Message);
    }
    catch (Exception ex)
    {
      return StatusCode(500, ex.Message);
    }
  }


  [HttpPost("feedbacks/ban")]
  [Authorize(Roles = "User, Admin, SuperAdmin")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> BanFeedback([FromQuery] int feedbackId, [FromQuery] string userId)
  {
    try
    {
      await _mobileService.Feedbacks.BanFeedbackAsync(feedbackId, userId);
      return Ok();
    }
    catch (ArgumentNullException ex)
    {
      return BadRequest(ex.Message);
    }
    catch (Exception ex)
    {
      return StatusCode(500, ex.Message);
    }
  }

  #endregion
}
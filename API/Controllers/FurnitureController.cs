﻿using BLL.Common;
using DTOs.FurnitureDtos;

namespace API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class FurnitureController(IFurnitureService furnitureService)
    : ControllerBase
{
  private readonly IFurnitureService _furnitureService = furnitureService;

  [HttpGet("{lang}/all")]
  [AllowAnonymous]
  [ProducesResponseType(typeof(List<FurnitureDto>), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> GetAllAsync(string lang)
  {
    try
    {
      var categories = await _furnitureService.GetAllAsync(lang.ToLanguage());
      return Ok(categories);
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError);
    }
  }

  [HttpGet("{lang}/paged")]
  [AllowAnonymous]
  [ProducesResponseType(typeof(List<FurnitureDto>), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> GetAllAsync([FromRoute] string lang,
                                               [FromQuery] int pageSize = 10,
                                               [FromQuery] int pageNumber = 1)
  {
    try
    {
      var categories = await _furnitureService.GetAllAsync(pageSize,
                                                      pageNumber,
                                                      lang.ToLanguage());
      var metadata = new
      {
        categories.TotalCount,
        categories.PageSize,
        categories.PageIndex,
        categories.TotalPages,
        categories.HasNextPage,
        categories.HasPreviousPage
      };

      Response.Headers["X-Pagination"] = JsonConvert.SerializeObject(metadata);

      return Ok(categories.Items);
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError);
    }
  }

  [HttpGet("{lang}/{id}")]
  [AllowAnonymous]
  [ProducesResponseType(typeof(FurnitureDto), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> GetByIdAsync([FromRoute] string lang, [FromRoute] int id)
  {
    try
    {
      var furniture = await _furnitureService.GetByIdAsync(id, lang.ToLanguage());
      return Ok(furniture);
    }
    catch (FurnitureException ex)
    {
      return NotFound(ex.Message);
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError);
    }
  }

  [HttpGet("{id}")]
  [Authorize(Roles = "SuperAdmin, Admin")]
  [ProducesResponseType(typeof(FurnitureDto), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
  {
    try
    {
      var furniture = await _furnitureService.GetByIdAsync(id);
      return Ok(furniture);
    }
    catch (FurnitureException ex)
    {
      return NotFound(ex.Message);
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError);
    }
  }

  [HttpPost("{lang}")]
  [Authorize(Roles = "SuperAdmin, Admin")]
  [ProducesResponseType(typeof(FurnitureDto), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> CreateAsync([FromRoute] string lang, [FromBody] AddFurnitureDto furnitureDto)
  {
    try
    {
      var result = await _furnitureService.CreateAsync(furnitureDto, lang.ToLanguage());
      return Ok(result);
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

  [HttpPut("{lang}")]
  [Authorize(Roles = "SuperAdmin, Admin")]
  [ProducesResponseType(typeof(FurnitureDto), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> UpdateAsync([FromRoute] string lang, [FromBody] UpdateFurnitureDto furnitureDto)
  {
    try
    {
      var furniture = await _furnitureService.UpdateAsync(furnitureDto, lang.ToLanguage());
      return Ok(furniture);
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

  [HttpDelete("delete/{id}")]
  [Authorize(Roles = "SuperAdmin, Admin")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> DeleteAsync(int id)
  {
    try
    {
      await _furnitureService.ActionAsync(id, ActionType.Delete);
      return Ok();
    }
    catch (FurnitureException ex)
    {
      return NotFound(ex.Message);
    }
    catch (Exception ex)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
    }
  }

  [HttpPatch("archive/{id}")]
  [Authorize(Roles = "SuperAdmin, Admin")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> ArchiveAsync(int id)
  {
    try
    {
      await _furnitureService.ActionAsync(id, ActionType.Archive);
      return Ok();
    }
    catch (FurnitureException ex)
    {
      return NotFound(ex.Message);
    }
    catch (Exception ex)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
    }
  }

  [HttpPatch("unarchive/{id}")]
  [Authorize(Roles = "SuperAdmin, Admin")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> RecoverAsync(int id)
  {
    try
    {
      await _furnitureService.ActionAsync(id, ActionType.UnArchive);
      return Ok();
    }
    catch (FurnitureException ex)
    {
      return NotFound(ex.Message);
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError);
    }
  }
}

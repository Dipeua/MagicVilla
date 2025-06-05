using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaAPI.Controllers;

[ApiController]
// [Route("api/[controller]")]
[Route("api/VillaAPI")]
public class VillaAPIController : Controller
{
    private readonly ILogger<VillaAPIController> _logger; // This adding logger to the console for debugging purposes
    public VillaAPIController(ILogger<VillaAPIController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<VillaDTO>> GetVillas()
    {
        _logger.LogInformation("Fetching all villas");
        return Ok(VillaStore.VillaList);
    }

    [HttpGet("{requestId:int}", Name = "GetVilla")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<VillaDTO> GetVilla(int requestId)
    {
        if (requestId <= 0)
        {
            _logger.LogInformation("Get Villa Error with Id: " + requestId);
            return BadRequest();
        }
        var villa = VillaStore.VillaList.FirstOrDefault(v => v.Id == requestId);
        if (villa == null) return NotFound();
        return Ok(villa);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public ActionResult<VillaDTO> CreateVilla([FromBody] VillaDTO requestVilla)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(requestVilla);
        }
        
        if(VillaStore.VillaList.FirstOrDefault(v => v.Name.ToLower() == requestVilla.Name.ToLower()) != null)
        {
            ModelState.AddModelError("CustomError", "Villa already exists!");
            return BadRequest(ModelState);
        }

        if (requestVilla == null)
        {
            return BadRequest(requestVilla);
        }

        if (requestVilla.Id > 0)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        requestVilla.Id = VillaStore.VillaList.OrderByDescending(v => v.Id).FirstOrDefault().Id + 1;
        VillaStore.VillaList.Add(requestVilla);
        return CreatedAtRoute("GetVilla", new { requestId = requestVilla.Id }, requestVilla);
    }

    [HttpDelete("{requestId:int}", Name ="DeleteVilla")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult DeleteVilla(int requestId)
    {
        if (requestId <= 0)
        {
            return BadRequest("Invalid ID");
        }
        var villa = VillaStore.VillaList.FirstOrDefault(v => v.Id == requestId);
        if (villa == null)
        {
            return NotFound();
        }
        VillaStore.VillaList.Remove(villa);
        return NoContent();
    }

    [HttpPut("{requestId:int}", Name = "Update villa")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult UpdateVilla(int requestId, [FromBody] VillaDTO requestVilla)
    {
        if (requestId <= 0 || requestVilla == null || (requestId != requestVilla.Id))
        {
            return BadRequest("Invalid ID");
        }
        var villa = VillaStore.VillaList.FirstOrDefault(v => v.Id == requestId);
        if (villa == null)
        {
            return NotFound();
        }

        villa.Id = requestVilla.Id;
        villa.Name = requestVilla.Name;
        villa.Occupancy = requestVilla.Occupancy;
        villa.Sqft = requestVilla.Sqft;

        return NoContent();
    }

    [HttpPatch("{requestId:int}", Name = "UpdatePartialvilla")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult UpdatePartialvilla(int requestId, JsonPatchDocument<VillaDTO> patchDTO)
    {
        if(requestId == 0 || patchDTO == null)
        {
            return BadRequest();
        }
        var villa = VillaStore.VillaList.FirstOrDefault(v => v.Id == requestId);
        if (villa == null) return NotFound();

        patchDTO.ApplyTo(villa, ModelState);
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        return NoContent();
    }
}
﻿using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaAPI.Controllers;

[ApiController]
// [Route("api/[controller]")]
[Route("api/VillaAPI")]
public class VillaAPIController : Controller
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<VillaDTO>> GetVillas()
    {
        return Ok(VillaStore.VillaList);
    }

    [HttpGet("{requestId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<VillaDTO> GetVilla(int requestId)
    {
        if (requestId <= 0)
            return BadRequest("Invalid ID");
        var villa = VillaStore.VillaList.FirstOrDefault(v => v.Id == requestId);
        if (villa == null) return NotFound();
        return Ok(villa);
    }
}
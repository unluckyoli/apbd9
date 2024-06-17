using Microsoft.AspNetCore.Mvc;
using ZadanieDomoweAPBD9.DTOs;
using ZadanieDomoweAPBD9.Models;
using ZadanieDomoweAPBD9.Services;

namespace ZadanieDomoweAPBD9.Controllers;

[ApiController]
[Route("[controller]")]
public class PrescriptionsController : ControllerBase
{
    private readonly IDbService _service;

    public PrescriptionsController(IDbService service)
    {
        _service = service;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddPrescription([FromBody] AddPrescriptionRequest request)
    {
        var success = await _service.AddPrescription(request);
        if (!success)
        {
            return BadRequest("Failed to add prescription.");
        }
        return Ok("Prescription added successfully.");
    }

    [HttpGet("details/{id}")]
    public async Task<ActionResult<PatientDetailsDto>> GetPatientDetails(int id)
    {
        var details = await _service.GetPatientDetails(id);
        if (details == null)
        {
            return NotFound("Patient not found.");
        }
        return Ok(details);
    }
}


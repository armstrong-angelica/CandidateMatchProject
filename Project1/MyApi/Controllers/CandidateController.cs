//using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using MyApi.DTOs;
using MyApi.Services;
using MyApp.Core.Models;

[Route("app/[controller]")]
[ApiController]

public class CandidateController : ControllerBase
{
    private readonly ICandidateService _candidateService;
    public CandidateController(ICandidateService candidateService)
    {
        _candidateService = candidateService;
    }

    //Get All Candidates (From Admin View, Recruiters only see matched Candidates)
    [HttpGet("All")]
    public async Task<ActionResult<IEnumerable<Candidate>>> GetAllCandidates()
    {
        try
        {

            return await _candidateService.GetAllCandidatesAsync();

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    //Get Candidate by Id
    [HttpGet("CandidateId")]
    public async Task<ActionResult<Candidate>> GetCandidateById(int CandidateId)
    {
        try
        {

            return await _candidateService.GetCandidateByIdAsync(CandidateId);

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    //Filter Candidates via Status (Active/Hired)
    [HttpGet("StatusFilter/{status}")]
    public async Task<ActionResult<IEnumerable<Candidate>>> GetCandidatesByStatus(string status)
    {
        try
        {

            return await _candidateService.GetCandidatesByStatusAsync(status);

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    //Sort Candidates via Education
    [HttpGet("EducationSort/EducationSort")]
    public async Task<ActionResult<IEnumerable<Candidate>>> GetCandidatesByEducation()
    {
        try
        {

            return await _candidateService.GetCandidatesByEducationAsync();

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    //Create Candidate via name
    //Later, use of emails will check for existing candidate
    [HttpPost]
    public async Task<ActionResult<Candidate>> CreateCandidate(NewCandidateDTO newCandidate)
    {
        try
        {
            return await _candidateService.CreateCandidateAsync(newCandidate);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    //Update status col for candidate
    [HttpPatch("/StatusUpdate/{CandidateId}")]
    public async Task<ActionResult<Candidate>> UpdateCandidateStatus(int CandidateId, string status)
    {
        try
        {
            return await _candidateService.UpdateCandidateStatusAsync(CandidateId, status);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    //Update education col for candidate
    [HttpPatch("/HighestEducationUpdate/{CandidateId}")]
    public async Task<ActionResult<Candidate>> UpdateCandidateEducation(int CandidateId, string education)
    {
        try
        {
            return await _candidateService.UpdateCandidateEducationAsync(CandidateId, education);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
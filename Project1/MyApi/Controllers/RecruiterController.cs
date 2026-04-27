//using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using MyApi.DTOs;
using MyApi.Services;
using MyApp.Core.Models;

[Route("app/[controller]")]
[ApiController]

public class RecruiterController : ControllerBase
{
    private readonly IRecruiterService _recruiterService;

    public RecruiterController(IRecruiterService recruiterService)
    {
        _recruiterService = recruiterService;
    }

    //Get All Recruiters (From Admin View)
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RecruiterDto>>> GetAllRecruiter()
    {
        try
        {

            return await _recruiterService.GetAllRecruitersAsync();

        }
        catch (NullReferenceException)
        {
            return NotFound("There are no recruiters");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }



    //Get Recruiters By Id
    [HttpGet("{RecruiterId}")]
    public async Task<ActionResult<RecruiterDto>> GetRecruiterId(int RecruiterId)
    {
        try
        {

            return await _recruiterService.GetRecruiterByIdAsync(RecruiterId);

        }
        catch (NullReferenceException)
        {
            return NotFound("Recruiter doesn't exist");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    //Creater Recruiter via name (can have duplicate names)
    //Later, use of emails will check for existing recruiters
    [HttpPost]
    public async Task<ActionResult<Recruiter>> CreateRecruiter(NewRecruiterDTO newRecruiter)
    {
        try
        {
            return await _recruiterService.CreateRecruiterAsync(newRecruiter);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
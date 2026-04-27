//using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using MyApi.DTOs;
using MyApi.Services;
using MyApp.Core.Models;

[Route("app/[controller]")]
[ApiController]

public class JobController : ControllerBase
{
    private readonly IJobService _jobService;
    public JobController(IJobService jobService)
    {
        _jobService = jobService;
    }

    //Get All Jobs (From Admin View)
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Job>>> GetAllJobs()
    {
        try
        {

            return await _jobService.GetAllJobsAsync();

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    //Get All Jobs (From Recruiter View)  (based on recruiters Id)
    [HttpGet("ByRecruiter/{RecruiterId}")]
    public async Task<ActionResult<IEnumerable<Job>>> GetAllJobsByRecruiter(int RecruiterId)
    {
        try
        {

            return await _jobService.GetAllJobsByRecruiterAsync(RecruiterId);

        }
        catch (NullReferenceException)
        {
            return NotFound("Recruiter hasn't posted any jobs");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    //Get Job By Id 
    [HttpGet("ById/{JobId}")]
    public async Task<ActionResult<Job>> GetJobById(int JobId)
    {
        try
        {

            return await _jobService.GetJobByIdAsync(JobId);

        }
        catch (NullReferenceException)
        {
            return NotFound("Job doesn't exist");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    //Get Jobs By Status 
    [HttpGet("ByStatus/{status}")]
    public async Task<ActionResult<IEnumerable<Job>>> GetJobsByStatus(string status)
    {
        try
        {

            return await _jobService.GetJobsByStatusAsync(status);

        }
        catch (NullReferenceException)
        {
            return NotFound("No jobs match that status");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    //post - add new job
    [HttpPost]
    public async Task<ActionResult<Job>> CreateJob(NewJobDTO newJob)
    {
        try
        {
            return await _jobService.CreateJobAsync(newJob);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    //delete by id, no dto needed bc just need id
    [HttpDelete("{jobId}")]
    public async Task<ActionResult> DeleteJob(int jobId)
    {
        //not returning the row being deleted 

        //global exception handeling example so no try/catch

        try
        {
            await _jobService.DeleteJobAsync(jobId);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
        
        return NoContent();
    }


    //Update status col for job
    [HttpPatch("/StatusUpdate/{JobId}")]
    public async Task<ActionResult<Job>> UpdateJobStatus(int JobId, string status)
    {
        try
        {
            return await _jobService.UpdateJobStatusAsync(JobId, status);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    //Hire Process
    [HttpPatch("Job={JobId}/Hire={CandidateId}")]
    public async Task<ActionResult<Job>> Hire(int JobId, int CandidateId)
    {
        try
        {
            return await _jobService.HireAsync(JobId, CandidateId);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }




    //put - replacing row
    //patch - update specific cols/info in row

    /* [HttpPatch]
    public async Task<ActionResult> AddCandidatesToJob(JobCandidateDTO updateInfo)
    {
        try
        {
            await _jobService.AddCandidatesToJob(updateInfo);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }

        //return updated entity
        return NoContent(); // not echoing updated object
    } */
    
//gets are safe because no risk in data loss (item potent) - changes when first called but if called again it doesn't do anything
//post, unless checking if item already exists, 
//can end up with same entity with different PK
//
}
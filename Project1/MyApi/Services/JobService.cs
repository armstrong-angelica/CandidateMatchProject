using MyApi.Data;
using MyApi.DTOs;
using MyApp.Core.Models;

namespace MyApi.Services;

public class JobService : IJobService
{

    private readonly IJobRepo _repo;
    public JobService(IJobRepo repo)
    {
        _repo = repo;
    }


    public async Task<List<Job>> GetAllJobsAsync()
    {
        List<Job> result = await _repo.GetAllJobs();

        if (result is null)
        {
            throw new NullReferenceException("No job postings created yet");
        }

        return result;
    }


    public async Task<List<Job>> GetAllJobsByRecruiterAsync(int id)
    {
        List<Job> result = await _repo.GetAllJobsByRecruiter(id);

        if (result is null)
        {
            throw new NullReferenceException("No job postings created yet");
        }

        return result;
    }

    public async Task<Job> GetJobByIdAsync(int id)
    {
        Job? result = await _repo.GetJobByID(id);

        if (result is null)
        {
            throw new NullReferenceException($"No job postings matching id={id}");
        }

        return result;
    }

    public async Task<List<Job>> GetJobsByStatusAsync(string status)
    {
        if(status != "Closed" && status != "Opened")
        {
            throw new ArgumentException("The status must be either \"Closed\" or \"Opened\"");
        }

        List<Job> result = await _repo.GetJobsByStatus(status);

        if (result is null)
        {
            throw new NullReferenceException($"No job postings match the status {status}");
        }

        return result;
    }

    public async Task<Job> CreateJobAsync(NewJobDTO newJob)
    {

        var errors = new List<string>();

        string title = newJob.Title ?? throw new ArgumentNullException("Title field is required");

        if (newJob.Title is null)
        {
            errors.Add("Title field is required");
        }
        else if (newJob.Title.All(char.IsDigit))
        {
         errors.Add("Title field shouldn't be all digits");   
        }

        if (!Enum.IsDefined(typeof(EducationLevel), newJob.RequiredEducation))
        {
            errors.Add($"{newJob.RequiredEducation} is not a valid education level");
        }

        if (errors.Any())
        {
            throw new ArgumentException(string.Join(", ", errors));
        }
        if(newJob.RecruiterId <= 0)
        {
            throw new ArgumentOutOfRangeException("Recruiter id must be greater than 0");
        }

        Boolean exist = await _repo.FindRecruiter(newJob.RecruiterId);

        if(!exist)
        {
            throw new KeyNotFoundException($"Recruiter with id={newJob.RecruiterId} doesn't exist");
        }

        //map from dto to actual object
        Job job = new Job();
        job.Title = title;
        job.RequiredEducation = newJob.RequiredEducation;
        job.RecruiterId = newJob.RecruiterId;
        //job.Skills = newJob.Skills;

        Job? createdRow = await _repo.CreateJob(job);
        if(createdRow is null)
        {
            throw new NullReferenceException("Job not created");
        }

        return createdRow;

    }

    public async Task DeleteJobAsync(int id)
    {
        if(id <= 0)
        {
            throw new ArgumentOutOfRangeException("ID must be greater than 0");
        }

        Job? job = await _repo.GetJobByID(id);

        if(job is null)
        {
            throw new KeyNotFoundException("The job doesn't exist");
        }

        await _repo.DeleteJob(job);
    }

    
    /* public async Task<Job> UpdateJobStatusAsync(int id, string status)
    {
        if(status != "Opened" && status != "Closed")
        {
            throw new ArgumentException("The status must be either \"Opened\" or \"Closed\"");
        }

        return await _repo.UpdateStatus(id, status);
    } */

    public async Task<Job> HireAsync(int jobId, int candidateId)
    {
        if(jobId <= 0 || candidateId <= 0)
        {
            throw new ArgumentOutOfRangeException("IDs must be greater than 0");
        }

        return await _repo.Hire(jobId, candidateId);
    }

    /* public async Task AddCandidatesToJob(JobCandidateDTO updateInfo)
    {
        await _repo.UpdateJobCandidates(updateInfo);
        //or updateInfo.Id1, updateInfo.Id2
    } */
}
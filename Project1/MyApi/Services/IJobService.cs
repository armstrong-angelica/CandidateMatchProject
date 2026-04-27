using MyApi.DTOs;
using MyApp.Core.Models;

namespace MyApi.Services;

public interface IJobService
{
    Task<List<Job>> GetAllJobsAsync();
    Task<List<Job>> GetAllJobsByRecruiterAsync(int id);
    Task<Job> GetJobByIdAsync(int id);
    Task<List<Job>> GetJobsByStatusAsync(string status);
    Task<Job> CreateJobAsync(NewJobDTO newJob);
    Task DeleteJobAsync(int id);
    Task<Job> UpdateJobStatusAsync(int id, string status);
    Task<Job> HireAsync(int jobId, int candidateId);
    //Task AddCandidatesToJob(JobCandidateDTO updateInfo);
}
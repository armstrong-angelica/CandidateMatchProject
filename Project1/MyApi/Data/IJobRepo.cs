using MyApi.DTOs;
using MyApp.Core.Models;

namespace MyApi.Data;

public interface IJobRepo
{
    Task GenerateMatchesForAllJobs(List<Job> jobs);
    Task<List<Job>> GetAllJobs();
    Task<List<Job>> GetAllJobsByRecruiter(int id);
    Task<List<Job>> GetJobsByStatus(string status);
    Task<Job?> CreateJob(Job newJob);
    Task<Boolean> FindRecruiter(int id);
    Task<Job?> GetJobByID(int id);
    Task DeleteJob(Job job);
    Task<Job> UpdateStatus(int id, string status);
    Task<Job> Hire(int jobId, int candidateId);
    
    //Task Match(Job newJob);
    //Task UpdateJobCandidates(JobCandidateDTO updateInfo);
}
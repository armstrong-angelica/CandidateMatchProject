using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using MyApi.DTOs;
using MyApp.Core.Models;
using MyApp.Data;

namespace MyApi.Data;

public class JobRepo : IJobRepo
{
    private readonly AppDbContext _context;

    public JobRepo(AppDbContext context)
    {
        _context = context;
    }

    public async Task GenerateMatchesForAllJobs(List<Job> jobs)
    {
        //var jobs = await _context.Jobs.ToListAsync();

        foreach (var job in jobs)
        {
            var existingCandidateIds = await _context.JobCandidateMatches
                .Where(m => m.JobId == job.Id)
                .Select(m => m.CandidateId)
                .ToListAsync();

            var candidates = await _context.Candidates
                .Where(c => c.HighestEducation >= job.RequiredEducation
                        && c.Status != "Hired"
                         && !existingCandidateIds.Contains(c.Id))
                .ToListAsync();

            var matches = candidates.Select(c => new JobCandidateMatch
            {
                JobId = job.Id,
                CandidateId = c.Id,
            });

            _context.JobCandidateMatches.AddRange(matches);
        }

        await _context.SaveChangesAsync();
    }

    public async Task<List<Job>> GetAllJobs()
    {
        var jobs = await _context.Jobs.ToListAsync();
        await GenerateMatchesForAllJobs(jobs);

        return await _context.Jobs
        .Include(j => j.Match)
        .ThenInclude(m => m.Candidate)
        .ToListAsync();
    }


    public async Task<List<Job>> GetAllJobsByRecruiter(int id)
    {
        List<Job> jobs = await _context.Jobs.Where(j => j.RecruiterId == id).ToListAsync();
        await GenerateMatchesForAllJobs(jobs);


        return await _context.Jobs
        .Include(j => j.Recruiter)
        .Include(j => j.Match)
        .ThenInclude(m => m.Candidate)
        .Where(j => j.RecruiterId == id)
        .ToListAsync();
        //return jobs;
    }

    public async Task<List<Job>> GetJobsByStatus(string status)
    {
        List<Job> jobs = await _context.Jobs.Where(j => j.Status == status).ToListAsync();
        // await GenerateMatchesForAllJobs(jobs);


        return await _context.Jobs
        .Include(j => j.Match)
        .ThenInclude(m => m.Candidate)
        .Where(j => j.Status == status)
        .ToListAsync(); 
        //return jobs;
    }


    public async Task<Job?> CreateJob(Job newJob)
    {
        _context.Jobs.Add(newJob);
        await _context.SaveChangesAsync();

        List<Job> jobs = await _context.Jobs.Where(j => j.Id == newJob.Id).ToListAsync();
        await GenerateMatchesForAllJobs(jobs);


        return await _context.Jobs
        .Include(j => j.Match)
        .ThenInclude(m => m.Candidate)
        .FirstOrDefaultAsync(j => j.Id == newJob.Id);

        //object updated to reflact what was created/updated
        //return newJob;
    }

    public async Task<Boolean> FindRecruiter(int id)
    {
        var jobs = _context.Jobs.Include(j => j.Recruiter).ToListAsync;

        Recruiter? r = _context.Recruiters.Find(id);

        if (r is null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public async Task<Job?> GetJobByID(int id)
    {
        List<Job> jobs = await _context.Jobs
    .Where(j => j.Id == id)
    .ToListAsync();

        await GenerateMatchesForAllJobs(jobs);

        await _context.Jobs
        .Include(j => j.Match)
        .ThenInclude(m => m.Candidate)
        .FirstAsync(j => j.Id == id);
        //load candidates

        return await _context.Jobs.FindAsync(id);
        //if not found -> null
    }

    public async Task DeleteJob(Job job)
    {
        _context.Jobs.Remove(job);
        await _context.SaveChangesAsync();
    }

    public async Task<Job> UpdateStatus(int id, string status)
    {
        Job? job = await _context.Jobs.FirstOrDefaultAsync(c => c.Id == id);
        if (job is null)
        {
            throw new KeyNotFoundException($"Job id={id} not found");
        }

        //does status already exists
        if (job.Status == status)
        {
            throw new InvalidOperationException($"Job's status already set to {status}");
        }
        job.Status = status;
        await _context.SaveChangesAsync();
        return job;
    }

    public async Task<Job> Hire(int jobId, int candidateId)
    {
        Job? job = await _context.Jobs.Include(j => j.Match).FirstOrDefaultAsync(j => j.Id == jobId);
        if (job is null)
        {
            throw new KeyNotFoundException($"Job id={jobId} not found");
        }
        if (job.Status == "Closed")
        {
            throw new ArgumentException($"Job id={jobId} is already Closed");
        }

        Candidate? can = await _context.Candidates.FirstOrDefaultAsync(c => c.Id == candidateId);
        if (can is null)
        {
            throw new KeyNotFoundException($"Candidate id={candidateId} not found");
        }
        if (can.Status == "Hired")
        {
            throw new ArgumentException($"Candidate id={candidateId} is already Hired for a different job");
        }
        if (can.HighestEducation < job.RequiredEducation)
        {
            throw new InvalidOperationException($"Candidate with id={can.Id} has highest education of {can.HighestEducation}, which is lower then the required education of {job.RecruiterId}");
        }

        job.Status = "Closed";
        job.HiredCandidateId = candidateId;
        job.HiredCandidate = can;
        can.Status = "Hired";

        await _context.SaveChangesAsync();
        return job;
    }

    /*public async Task UpdateJobCandidates(JobCandidateDTO updateInfo)
    {
        //grab job using PK, else if null then excception
        //Job job = await _context.Jobs.FindAsync(); //default loading = explicit - loads needed data
        Job? job = await _context.Jobs.Include(j => j.Candidates).FirstOrDefaultAsync(j => j.Id == updateInfo.JobId);
        if (job is null)
        {
            throw new KeyNotFoundException($"Job {updateInfo.JobId} not found");
        }
        //grab Candidate using PK, else if null then excception
        Candidate? can = await _context.Candidates.FindAsync(updateInfo.CandidateId);
        if (can is null) //dont want db operations to silently fail
        {
            throw new KeyNotFoundException($"Candidate {updateInfo.CandidateId} not found");
        }

        //does relationship already exists
        if (job.Candidates.Any(c => c.Id == updateInfo.CandidateId))
        {
            throw new Exception("Job already contains this Candidate");
        }

        job.Candidates.Add(can);
        await _context.SaveChangesAsync();
    }
    */




}
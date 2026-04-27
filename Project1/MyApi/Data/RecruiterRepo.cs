using Microsoft.EntityFrameworkCore;
using MyApi.DTOs;
using MyApp.Core.Models;
using MyApp.Data;

namespace MyApi.Data;

public class RecruiterRepo : IRecruiterRepo
{
    private readonly AppDbContext _context;

    public RecruiterRepo(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<RecruiterDto>> GetAllRecruiters()
    {
        //List<Recruiter> result = await _context.Recruiters.Include(r => r.Jobs).ToListAsync();
        return await _context.Recruiters
    .Include(r => r.Jobs)
    .Select(r => new RecruiterDto
    {
        Id = r.Id,
        Name = r.Name,
        Jobs = r.Jobs.Select(j => new JobDto
        {
            Id = j.Id,
            Title = j.Title
        }).ToList()
    })
    .ToListAsync();

        //return result;
    }

    public async Task<RecruiterDto?> GetRecruiter(int id)
    {
        //Recruiter? result = await _context.Recruiters.Include(r => r.Jobs).FirstOrDefaultAsync(r => r.Id == id);
        //return result;
        return await _context.Recruiters
    .Include(r => r.Jobs)
    .Select(r => new RecruiterDto
    {
        Id = r.Id,
        Name = r.Name,
        Jobs = r.Jobs.Select(j => new JobDto
        {
            Id = j.Id,
            Title = j.Title
        }).ToList()
    })
    .FirstAsync(r => r.Id == id);
    }

    public async Task<Recruiter> CreateRecruiter(Recruiter newRecruiter)
    {
        //later check id recruiter already exists based on login email
        _context.Recruiters.Add(newRecruiter);
        await _context.SaveChangesAsync();

        return newRecruiter;
    }
    
}
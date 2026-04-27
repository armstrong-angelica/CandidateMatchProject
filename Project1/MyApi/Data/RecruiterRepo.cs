using Microsoft.EntityFrameworkCore;
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

    public async Task<List<Recruiter>> GetAllRecruiters()
    {
        List<Recruiter> result = await _context.Recruiters.Include(r => r.Jobs).ToListAsync();

        return result;
    }

    public async Task<Recruiter?> GetRecruiter(int id)
    {
        Recruiter? result = await _context.Recruiters.Include(r => r.Jobs).FirstOrDefaultAsync(r => r.Id == id);;

        return result;
    }

    public async Task<Recruiter> CreateRecruiter(Recruiter newRecruiter)
    {
        //later check id recruiter already exists based on login email
        _context.Recruiters.Add(newRecruiter);
        await _context.SaveChangesAsync();

        return newRecruiter;
    }
    
}
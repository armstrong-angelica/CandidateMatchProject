using Microsoft.EntityFrameworkCore;
using MyApp.Core.Models;
using MyApp.Data;

namespace MyApi.Data;

public class CandidateRepo : ICandidateRepo
{
    private readonly AppDbContext _context;

    public CandidateRepo(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Candidate>?> GetAllCandidates() 
    {
        List<Candidate>? result = await _context.Candidates.ToListAsync();

        return result;
    }

    public async Task<Candidate?> GetCandidate(int id)
    {
        Candidate result = await _context.Candidates.FirstAsync(c => c.Id == id);//without ?, exception throws here and doesn't bubble up to service because return wasnt expecting null

        return result;
    }

    public async Task<List<Candidate>?> FilterCandidatesByStatus(string status)
    {
        List<Candidate> result = await _context.Candidates.Where(c => c.Status == status).ToListAsync();

        return result;
    }

    public async Task<List<Candidate>?> SortCandidatesByEducation()
    {
        List<Candidate> result = await _context.Candidates.OrderByDescending(c => c.HighestEducation).ToListAsync();

        return result;
    }

    public async Task<Candidate> CreateCandidate(Candidate newCandidate)
    {
        _context.Candidates.Add(newCandidate);
        await _context.SaveChangesAsync();

        return newCandidate;
    }


    public async Task<Candidate> UpdateStatus(int id, string status)
    {
      
        Candidate? can = await _context.Candidates.FirstOrDefaultAsync(c => c.Id == id);
        if (can is null)
        {
            throw new KeyNotFoundException($"Candidate {id} not found");
        }
        
        //does status already exists
        if (can.Status == status)
        {
            throw new InvalidOperationException($"Candidate's status already set to {status}");
        }
        can.Status = status;
        await _context.SaveChangesAsync();
        return can;
    }


    public async Task<Candidate> UpdateEducation(int id, string education)
    {
      
        Candidate? can = await _context.Candidates.FirstOrDefaultAsync(c => c.Id == id);
        if (can is null)
        {
            throw new KeyNotFoundException($"Candidate {id} not found");
        }
        
        //does education already exists
        if (can.HighestEducation == Enum.Parse<EducationLevel>(education))
        {
            throw new InvalidOperationException($"Candidate's education already set to {education}");
        }
        can.HighestEducation = Enum.Parse<EducationLevel>(education);
        await _context.SaveChangesAsync();
        return can;
    }

}
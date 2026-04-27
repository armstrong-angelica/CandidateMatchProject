using MyApi.Data;
using MyApi.DTOs;
using MyApp.Core.Models;

namespace MyApi.Services;

public class RecruiterService : IRecruiterService
{

    private readonly IRecruiterRepo _repo;
    public RecruiterService(IRecruiterRepo repo)
    {
        _repo = repo;
    }


    public async Task<List<Recruiter>> GetAllRecruitersAsync()
    {
        List<Recruiter> result = await _repo.GetAllRecruiters();

        if (result is null)
        {
            throw new NullReferenceException("No recruiters found");
        }

        return result;
    }


    public async Task<Recruiter> GetRecruiterByIdAsync(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentOutOfRangeException("Id must be greater than 0");
        }
        
        Recruiter? result = await _repo.GetRecruiter(id);

        if (result is null)
        {
            throw new NullReferenceException($"No recruiter with id={id} found");
        }

        return result;
    }


    public async Task<Recruiter> CreateRecruiterAsync(NewRecruiterDTO newRecruiter)
    {
        if (newRecruiter.Name is null)
        {
            throw new ArgumentException("Name field is required");
        }
        else if (newRecruiter.Name.Any(char.IsDigit))
        {
            throw new ArgumentException("Name field shouldn't contain a digit");
        }

        //map from dto to actual object
        Recruiter recruiter = new Recruiter();
        recruiter.Name = newRecruiter.Name;

        //later check that user/recruiter doesn't already exist via email


        return await _repo.CreateRecruiter(recruiter);

    }

}
using MyApi.Data;
using MyApi.DTOs;
using MyApp.Core.Models;

namespace MyApi.Services;

public class CandidateService : ICandidateService
{

    private readonly ICandidateRepo _repo;
    public CandidateService(ICandidateRepo repo)
    {
        _repo = repo;
    }


    public async Task<List<Candidate>> GetAllCandidatesAsync()
    {
        List<Candidate>? result = await _repo.GetAllCandidates();

        if (result is null)
        {
            throw new NullReferenceException("No candidates found");
        }

        return result;
    }

    public async Task<Candidate> GetCandidateByIdAsync(int id)
    {
        Candidate? result = await _repo.GetCandidate(id);

        if (result is null)
        {
            throw new InvalidOperationException($"No candidate with id={id} found");
        }

        return result;
    }

    public async Task<List<Candidate>> GetCandidatesByStatusAsync(string status)
    {

        if (status != "Hired" && status != "Active")
        {
            throw new ArgumentException("The status must be either \"Active\" or \"Hired\"");
        }

        List<Candidate>? result = await _repo.FilterCandidatesByStatus(status);

        if (result is null)
        {
            throw new NullReferenceException($"No candidates found with status={status}");
        }

        return result;
    }

    public async Task<List<Candidate>> GetCandidatesByEducationAsync()
    {
        List<Candidate>? result = await _repo.SortCandidatesByEducation();

        if (result is null)
        {
            throw new NullReferenceException("No candidates found");
        }

        return result;
    }

    public async Task<Candidate> CreateCandidateAsync(NewCandidateDTO newCandidate)
    {
        var errors = new List<string>();

        string name = newCandidate.Name?? throw new ArgumentException("Name field is required");


        if (newCandidate.Name is null)
        {
            errors.Add("Name field is required");
        }
        else if (newCandidate.Name.Any(char.IsDigit))
        {
            errors.Add("Name field shouldn't contain a digit");
        }

        if (!Enum.IsDefined(typeof(EducationLevel), newCandidate.HighestEducation))
        {
            errors.Add($"{newCandidate.HighestEducation} is not a valid education level");
        }

        if (errors.Any())
        {
            throw new ArgumentException(string.Join(", ", errors));
        }
        //map from dto to actual object
        Candidate candidate = new Candidate();
        candidate.Name = name;
        candidate.HighestEducation = newCandidate.HighestEducation;
        //candidate.Skills = newCandidate.Skills;

        return await _repo.CreateCandidate(candidate);

    }

    public async Task<Candidate> UpdateCandidateStatusAsync(int id, string status)
    {
        if (status != "Hired" && status != "Active")
        {
            throw new ArgumentException("The status must be either \"Active\" or \"Hired\"");
        }

        Candidate? can = await _repo.GetCandidate(id);
        if (can is null)
        {
            throw new NullReferenceException($"No candidate {id} found");
        }


        return await _repo.UpdateStatus(id, status);
    }

    public async Task<Candidate> UpdateCandidateEducationAsync(int id, string education)
    {
        if (!Enum.IsDefined(typeof(EducationLevel), Enum.Parse<EducationLevel>(education)))
        {
            throw new ArgumentException($"{education} is not a valid education level");
        }

        Candidate? can = await _repo.GetCandidate(id);
        if (can is null)
        {
            throw new NullReferenceException($"No candidate {id} found");
        }

        return await _repo.UpdateEducation(id, education);
    }

}
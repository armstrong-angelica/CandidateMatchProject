using MyApi.DTOs;
using MyApp.Core.Models;

namespace MyApi.Services;

public interface ICandidateService
{
    Task<List<Candidate>> GetAllCandidatesAsync();
    Task<Candidate> GetCandidateByIdAsync(int id);
    Task<List<Candidate>> GetCandidatesByStatusAsync(string status);
    Task<List<Candidate>> GetCandidatesByEducationAsync();
    Task<Candidate> CreateCandidateAsync(NewCandidateDTO newCandidate);
    Task<Candidate> UpdateCandidateStatusAsync(int id, string status);
    Task<Candidate> UpdateCandidateEducationAsync(int id, string education);

}
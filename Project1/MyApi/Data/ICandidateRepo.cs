using MyApp.Core.Models;

namespace MyApi.Data;

public interface ICandidateRepo
{
    Task<List<Candidate>?> GetAllCandidates(); 
    Task<Candidate?> GetCandidate(int id);
    Task<List<Candidate>?> FilterCandidatesByStatus(string status);
    Task<List<Candidate>?> SortCandidatesByEducation();
    Task<Candidate> CreateCandidate(Candidate newCandidate);
    Task<Candidate> UpdateStatus(int id, string status);
    Task<Candidate> UpdateEducation(int id, string education);
}
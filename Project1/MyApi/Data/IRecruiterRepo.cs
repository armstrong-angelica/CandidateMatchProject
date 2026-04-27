using MyApi.DTOs;
using MyApp.Core.Models;

namespace MyApi.Data;

public interface IRecruiterRepo
{
    Task<List<RecruiterDto>> GetAllRecruiters();
    Task<RecruiterDto?> GetRecruiter(int id);
    Task<Recruiter> CreateRecruiter(Recruiter newRecruiter);
}
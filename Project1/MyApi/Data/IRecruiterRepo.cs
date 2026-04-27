using MyApp.Core.Models;

namespace MyApi.Data;

public interface IRecruiterRepo
{
    Task<List<Recruiter>> GetAllRecruiters();
    Task<Recruiter?> GetRecruiter(int id);
    Task<Recruiter> CreateRecruiter(Recruiter newRecruiter);
}
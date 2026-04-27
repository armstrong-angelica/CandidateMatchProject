using MyApi.DTOs;
using MyApp.Core.Models;

namespace MyApi.Services;

public interface IRecruiterService
{
    Task<List<Recruiter>> GetAllRecruitersAsync();
    Task<Recruiter> GetRecruiterByIdAsync(int id);
    Task<Recruiter> CreateRecruiterAsync(NewRecruiterDTO newRecruiter);

}
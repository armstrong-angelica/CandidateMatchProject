using MyApi.DTOs;
using MyApp.Core.Models;

namespace MyApi.Services;

public interface IRecruiterService
{
    Task<List<RecruiterDto>> GetAllRecruitersAsync();
    Task<RecruiterDto> GetRecruiterByIdAsync(int id);
    Task<Recruiter> CreateRecruiterAsync(NewRecruiterDTO newRecruiter);

}
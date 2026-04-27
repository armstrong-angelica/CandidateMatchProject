using System.ComponentModel.DataAnnotations;
using MyApp.Core.Models;

namespace MyApi.DTOs;

public class JobDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? RequiredEducation { get; set; }
    public string Status { get; set; } = "Opened";
    public List<CandidateDto>? Candidates { get; set; }
}

public class CandidateDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? HighestEducation { get; set; }
}
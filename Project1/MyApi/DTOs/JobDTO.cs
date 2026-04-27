using System.ComponentModel.DataAnnotations;
using MyApp.Core.Models;

namespace MyApi.DTOs;

public class NewJobDTO
{
    [Required]
    //[MaxLength(100)]
    public string? Title { get; set; }

    [Required]
    public EducationLevel RequiredEducation { get; set; }  // High School Diploma || Bachelors Degree || Masters Degree || None

    [Required]
    public int RecruiterId { get; set; }

    //public List<Skills> Skills { get; set; } = new();
}

public class JobDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
}
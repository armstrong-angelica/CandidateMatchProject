using System.ComponentModel.DataAnnotations;
using MyApp.Core.Models;

namespace MyApi.DTOs;

public class NewCandidateDTO
{
    [Required]
    [MaxLength(100)]
    public string? Name { get; set; }

    public EducationLevel HighestEducation { get; set; } // High School Diploma || Bachelors Degree || Masters Degree || None


}
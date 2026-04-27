using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MyApp.Core.Models;

public class Candidate
{
    //[Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string? Name { get; set; }

    [Required]
    public EducationLevel HighestEducation { get; set; } // High School Diploma || Bachelors Degree || Masters Degree || None

    //public List<Skills> Skills { get; set; } = new();

    public string Status { get; set; } = "Active"; // Active, Hired


    //public List<Job> Jobs { get; set; } = new();
    [NotMapped]
    [JsonIgnore] // so not in swagger
    public List<JobCandidateMatch> Match { get; set; } = new();


}
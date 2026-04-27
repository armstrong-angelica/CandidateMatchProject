using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MyApp.Core.Models;

public class Job
{
    //[Key]
    public int Id { get; set; }

    [Required]
    //[MaxLength(100)]
    public string? Title { get; set; }
    //public string Location { get; set; }
    //public string Description { get; set; }

    [Required]
    public EducationLevel RequiredEducation { get; set; }  // High School Diploma || Bachelors Degree || Masters Degree || None

    //public List<Skills> Skills { get; set; } = new();

    public string Status { get; set; } = "Opened"; // Opened || Closed


    [Required]
    public int RecruiterId { get; set; } // navigation to reach company // foreign key

    //[NotMapped]
    //[JsonIgnore] // so not in swagger
    //[ForeignKey("RecruiterId")] not needed because conventional name used for id
    public Recruiter? Recruiter { get; set; }


    //public List<Candidate> Candidates { get; set; } = new();
    //[NotMapped]
    public List<JobCandidateMatch> Match { get; set; } = new();



    //hired
    public int? HiredCandidateId { get; set; }
    public Candidate? HiredCandidate { get; set; }
    //rejected List

    //public string Company{get; set;}

}
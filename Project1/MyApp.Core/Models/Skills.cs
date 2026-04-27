using System.ComponentModel.DataAnnotations;

namespace MyApp.Core.Models;

public class Skills
{
    public int Id { get; set; }

    [Required]
    [MaxLength(20)]
    public string? Name { get; set; }

    //juntion tables created
    //CandidatesSkills
    //JobsSkills
    public List<Candidate> Candidates { get; set; } = new();
    public List<Job> Jobs { get; set; } = new();


}
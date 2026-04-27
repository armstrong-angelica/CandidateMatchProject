using System.ComponentModel.DataAnnotations;

namespace MyApp.Core.Models;
public class Recruiter
{
    //[Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string? Name { get; set; }

    // 1 Recruiter -> many Job Postings
    public List<Job> Jobs { get; set; } = new();
    
    /* [Required]
    public string Company { get; set; } */
}
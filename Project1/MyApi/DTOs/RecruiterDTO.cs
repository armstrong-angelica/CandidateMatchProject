using System.ComponentModel.DataAnnotations;

namespace MyApi.DTOs;

public class NewRecruiterDTO
{
    [Required]
    [MaxLength(100)]
    public string? Name { get; set; }
}

public class RecruiterDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public List<JobDto>? Jobs { get; set; }
}
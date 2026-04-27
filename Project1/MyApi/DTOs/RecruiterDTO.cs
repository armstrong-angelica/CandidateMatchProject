using System.ComponentModel.DataAnnotations;

namespace MyApi.DTOs;

public class NewRecruiterDTO
{
    [Required]
    [MaxLength(100)]
    public string? Name { get; set; }
}
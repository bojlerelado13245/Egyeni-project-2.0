using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApp.Backend.Models;

public class Tasks
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    public Guid OwnerId { get; set; }

    [Required]
    public string TaskName { get; set; }

    [Required]
    public string TaskDesc { get; set; }

    public bool IsDone { get; set; } = false;
}
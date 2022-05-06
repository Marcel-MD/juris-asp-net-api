﻿using System.ComponentModel.DataAnnotations;

namespace Juris.Api.Dtos.Experience;

public class CreateExperienceDto
{
    [Required]
    [MinLength(3)]
    [MaxLength(50)]
    public string Company { get; set; }

    [Required]
    [MinLength(3)]
    [MaxLength(50)]
    public string Position { get; set; }

    [Required] public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }
}
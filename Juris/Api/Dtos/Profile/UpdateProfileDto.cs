﻿using System.ComponentModel.DataAnnotations;

namespace Juris.Api.Dtos.Profile;

public class UpdateProfileDto
{
    [Required]
    [MinLength(3)]
    public string FirstName { get; set; }

    [Required]
    [MinLength(3)]
    public string LastName { get; set; }

    [Required]
    [MinLength(8)]
    public string PhoneNumber { get; set; }

    [Required]
    [MinLength(16)]
    public string Description { get; set; }
    
    [Required]
    public string ProfileType { get; set; }
    
    [Required]
    public int Price { get; set; }

    [Required]
    public string City { get; set; }

    [Required]
    [MinLength(8)]
    public string AddressLine { get; set; }
}
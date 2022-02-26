﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Juris.Models.Entities;

public class Education : BaseEntity
{
    public string Institution { get; set; }

    public string Speciality { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public long ProfileId { get; set; }

    public Profile Profile { get; set; }
}
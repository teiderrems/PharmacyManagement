﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyManagement.Models
{

    [Table("Users")]
    public class ApplicationUser:IdentityUser
    {

        [StringLength(150, MinimumLength = 4)]
        public string FirstName { get; set; }=String.Empty;

        [StringLength(150,MinimumLength =4)]
        public string LastName { get; set; } = String.Empty;

        [DataType(DataType.Date)]
        [DisplayName("Birth Day")]
        public DateTime BirthDay { get; set; }

        [DisplayName("Created Date")]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }

        [DisplayName("Last Modified")]
        [DataType(DataType.Date)]
        public DateTime UpdatedAt { get; set; }
    }
}
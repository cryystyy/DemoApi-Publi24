﻿using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models.User
{
    public class UserDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public bool Active { get; set; }

        [Key]
        public Guid Id { get; set; }
    }
}

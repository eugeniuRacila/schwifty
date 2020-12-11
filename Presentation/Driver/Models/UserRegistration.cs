﻿using System.ComponentModel.DataAnnotations;

namespace Driver.Models
{
    public class UserRegistration
    {
        [Required]
        public string FirstName { get; set; } = "";
        
        [Required]
        public string LastName { get; set; } = "";
        
        [Required]
        public string Email { get; set; } = "";
        
        [Required]
        public string Password { get; set; } = "";
        
        [Required]
        public string ConfirmPassword { get; set; } = "";
    }
}
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Driver.Models
{
    public class UserRegistration
    {
       
        public string FirstName { get; set; } = "";
        
     
        public string LastName { get; set; } = "";
        
     
        public string Email { get; set; } = "";
        
      
        public string Password { get; set; } = "";
        
      
        public string PhoneNumber { get; set; } = "";
        
        
        public string ConfirmPassword { get; set; } = "";
    }
}
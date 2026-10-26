using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleBlogger.Models
{
    //Has everything that IdentityUser has, but need to extend it to add more properties, 
    //like address, phone number, etc.
    public class ApplicationUser : IdentityUser
    {

    }
}

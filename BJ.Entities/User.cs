using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace BJ.Entities
{
    [Table("AspNetUsers")]
    public class User : IdentityUser
    {
    }
}

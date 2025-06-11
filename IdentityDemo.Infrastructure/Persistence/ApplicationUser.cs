using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityDemo.Infrastructure.Persistence;
public class ApplicationUser : IdentityUser
{
    [MaxLength(30)]
    public string FirstName { get; set; } = null!;
    [MaxLength(30)]
    public string LastName { get; set; } = null!;
}


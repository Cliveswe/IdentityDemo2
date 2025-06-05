using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityDemo.Application.Dtos;

// Representerar en användare
public record UserProfileDto(string Email, string FirstName, string LastName);

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using Transavia.Core.Enums;

namespace Transavia.Core.Entities
{
    public class User : IdentityUser
    {
        [PersonalData]
        public string FirstName { get; set; }
        [PersonalData]
        public string LastName { get; set; }
        [PersonalData]
        public Gender Gender { get; set; }
        [PersonalData]
        public DateTime DateOfBirth { get; set; }
    }
}

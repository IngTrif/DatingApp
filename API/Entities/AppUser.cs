using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using API.Extensions;
using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class AppUser
    {
     
        public int Id { get; set; } //if we call id the nt framework will recognize as primary key; if other name you have to tell that it's a primary key
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public string KnownAs { get; set; }

        public DateTime Created { get; set; } = DateTime.UtcNow;

        public DateTime LastActive { get; set; } = DateTime.UtcNow;

        public string Gender { get; set; }

        public string Introduction { get; set; }
         
         public string LookingFor { get; set; }


        public string Interests { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public List<Photo> Photos { get; set; } = new ();

        //public int GetAge()
        //{
          //  return DateOfBirth.CalculateAge();
        //}



       // public static implicit operator AppUser(AppUser v)
       // {
          //  throw new NotImplementedException();
       // }
    }
}
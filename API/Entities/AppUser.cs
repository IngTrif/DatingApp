using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class AppUser
    {
        public int Id { get; set; } //if we call id the nt framework will recognize as primary key; if other name you have to tell that it's a primary key
        public string UserName { get; set; }
        
    }
}
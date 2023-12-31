

namespace API.DTOs
{
    public class MemberDto
    {
        public int Id { get; set; } //if we call id the nt framework will recognize as primary key; if other name you have to tell that it's a primary key
        public string UserName { get; set; }

         public string PhotoUrl { get; set; }

        public int Age { get; set; }

        public string KnownAs { get; set; }

        public DateTime Created { get; set; } 

        public DateTime LastActive { get; set; }

        public string Gender { get; set; }

        public string Introduction { get; set; }
         
         public string LookingFor { get; set; }


        public string Interests { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public List<PhotoDto> Photos { get; set; }

        //public int GetAge()
       // {
          //  return DateOfBirth.CalculateAge();
        }
        
    }

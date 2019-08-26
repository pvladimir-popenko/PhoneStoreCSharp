using System.Collections.Generic;

namespace MvcApplicationCSharp5.Models
{
    public class Company : Entity
    {
        public Company()
        {
            Phones = new List<Phone>();
        }
        
        public string Name { get; set; }

        public ICollection<Phone> Phones { get; set; }
    }
}
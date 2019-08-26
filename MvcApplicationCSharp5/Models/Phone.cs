using System.Collections;

namespace MvcApplicationCSharp5.Models
{
    public class Phone : Entity
    {
        public string Name { get; set; }
        public int CompanyId { get; set; }
        public decimal Price { get; set; }

        public Company Company { get; set; }
    }
}

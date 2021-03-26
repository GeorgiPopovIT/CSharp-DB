using System.Collections.Generic;

namespace RealEstates.Models
{
    public class Tag
    {
        public Tag()
        {
            Properties = new HashSet<PropertyObject>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Importance { get; set; }

        public virtual ICollection<PropertyObject> Properties { get; set; }
    }
}
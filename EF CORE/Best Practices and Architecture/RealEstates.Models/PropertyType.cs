using System.Collections.Generic;

namespace RealEstates.Models
{
    public class PropertyType
    {
        public PropertyType()
        {
            Properties = new HashSet<PropertyObject>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<PropertyObject> Properties { get; set; }
    }
}
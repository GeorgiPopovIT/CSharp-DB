using System;
using System.Collections.Generic;
using System.Text;

namespace ProductShop.DTO
{
    public class UserExportDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Age { get; set; }
        public SoldProductsDTO SoldProducts { get; set; }
    }
}

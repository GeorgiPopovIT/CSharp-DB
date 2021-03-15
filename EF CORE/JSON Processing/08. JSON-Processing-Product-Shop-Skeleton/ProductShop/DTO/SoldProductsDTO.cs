using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductShop.DTO
{
    public class SoldProductsDTO
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("products")]
        public List<ProductDTO> Products { get; set; }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductShop.DTO
{
    public class ProductUserDTO
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("price")]
        public decimal Price { get; set; }
        [JsonProperty("buyerfirstname")]
        public string BuyerFirstName { get; set; }
        [JsonProperty("buyerlastname")]
        public string BuyerLastName { get; set; }
    }
}

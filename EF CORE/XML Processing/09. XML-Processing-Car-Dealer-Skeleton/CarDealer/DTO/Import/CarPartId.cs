﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CarDealer.DTO.Import
{
    [XmlType("partId")]
    public class CarPartId
    {
        [XmlAttribute("id")]
        public int PartId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace TeisterMask.DataProcessor.ExportDto
{
    [XmlType("Task")]
    public class TaskXmlDto
    {
        [XmlElement]
        public string Name { get; set; }

        [XmlElement]
        public string Label { get; set; }
    }
}

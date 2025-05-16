using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lab2
{
    public class Donor
    {
        [XmlAttribute("DonorId")]
        public string DonorId { get; set; }

        [XmlAttribute("DonorName")]
        public string DonorName { get; set; }
    }

}

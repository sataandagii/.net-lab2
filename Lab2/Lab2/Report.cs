using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lab2
{
    public class Report
    {
        [XmlAttribute("OrganisationId")]
        public string OrganisationId { get; set; }

        [XmlAttribute("DonorId")]
        public string DonorId { get; set; }

        [XmlAttribute("ReceivedMoney")]
        public decimal ReceivedMoney { get; set; }

        [XmlAttribute("SpentMoney")]
        public decimal SpentMoney { get; set; }

        [XmlAttribute("DateWhenReceived")]
        public string DateWhenReceived { get; set; }
    }

}

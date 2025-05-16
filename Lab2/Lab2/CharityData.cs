using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lab2
{
    [XmlRoot("CharityFoundation")]
    public class CharityData
    {
        [XmlArray("Donors")]
        [XmlArrayItem("Donor")]
        public List<Donor> Donors { get; set; }

        [XmlArray("Organisations")]
        [XmlArrayItem("Organisation")]
        public List<Organisation> Organisations { get; set; }

        [XmlArray("Reports")]
        [XmlArrayItem("Report")]
        public List<Report> Reports { get; set; }
    }

}

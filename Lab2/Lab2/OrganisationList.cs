using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lab2
{
    [XmlRoot("Organisations")]
    public class OrganisationList
    {
        [XmlElement("Organisation")]
        public List<Organisation> Organisations { get; set; }
    }
}

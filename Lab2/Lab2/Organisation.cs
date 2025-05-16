using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lab2
{
    public class Organisation
    {
        [XmlAttribute("OrganisationId")]
        public string OrganisationId { get; set; }

        [XmlAttribute("OrganisationName")]
        public string OrganisationName { get; set; }
    }

}

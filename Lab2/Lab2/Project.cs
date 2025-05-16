using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lab2
{
    public class Project
    {
        [XmlAttribute("ProjectId")]
        public string ProjectId { get; set; }

        [XmlAttribute("ProjectName")]
        public string ProjectName { get; set; }

        [XmlAttribute("OrganisationId")]
        public string OrganisationId { get; set; }
    }

}

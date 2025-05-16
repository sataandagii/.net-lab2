using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lab2
{
    [XmlRoot("Projects")]
    public class ProjectList
    {
        [XmlElement("Project")]
        public List<Project> Projects { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lab2
{
    [XmlRoot("Donors")]
    public class DonorList
    {
        [XmlElement("Donor")]
        public List<Donor> Donors { get; set; }
    }
}

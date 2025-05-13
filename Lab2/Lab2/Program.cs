using System;
using System.Xml;
using System.Xml.Linq;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        XmlDocument doc = new XmlDocument();
        doc.Load("charity.xml");
        Console.WriteLine(doc.OuterXml);

        XDocument xmlDoc = XDocument.Load("charity.xml");

        //донори та їх середній взнос
        var query1 = xmlDoc.Descendants("Report")
            .GroupBy(r => r.Attribute("DonorId").Value)
            .Select(g => new
            {
                DonorId = g.Key,
                TotalDonated = g.Sum(r => decimal.Parse(r.Attribute("ReceivedMoney").Value)),
                AvgDonation = g.Average(r => decimal.Parse(r.Attribute("ReceivedMoney").Value))
            })
            .OrderByDescending(d => d.TotalDonated);

        Console.WriteLine("Знайти донорів, які жертвували найбільшу кількість коштів на проєкти, і для кожного донора вивести середній розмір жертви");
        foreach (var donor in query1)
        {
            Console.WriteLine($"Донор {donor.DonorId}: Всього пожертвував: {donor.TotalDonated}, Середній взнос: {donor.AvgDonation}");
        }
        Console.WriteLine();


        //огранізації та їх донати
        var query2 = xmlDoc.Descendants("Report")
            .GroupBy(r => r.Attribute("OrganisationId").Value)
            .Select(g => new
            {
                OrganisationId = g.Key,
                TotalRecieved = g.Sum(r => decimal.Parse(r.Attribute("ReceivedMoney").Value))
            })
            .OrderByDescending(d => d.TotalRecieved);

        Console.WriteLine("Визначити організації, які отримали найбільше фінансування від спонсорів");
        foreach (var organisation in query2)
        {
            Console.WriteLine($"Організація {organisation.OrganisationId}: Отримала пожертвувань: {organisation.TotalRecieved}");
        }
        Console.WriteLine();
    }
}
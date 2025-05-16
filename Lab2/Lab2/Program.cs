using Lab2;
using System;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

class Program
{
    static void DemonstrateXmlSerializer(string filePath)
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(CharityData));
            using (FileStream stream = File.OpenRead(filePath))
            {
                CharityData data = (CharityData)serializer.Deserialize(stream);

                Console.WriteLine("\nЗавантаження через XmlSerializer:");
                Console.WriteLine($"Донорів: {data.Donors.Count}");
                Console.WriteLine($"Організацій: {data.Organisations.Count}");
                Console.WriteLine($"Звітів: {data.Reports.Count}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка при завантаженні XML: {ex.Message}");
        }
    }

    static void DemonstrateXmlDocument(string filePath)
    {
        try
        {
            Console.WriteLine("\nЗавантаження через XmlDocument:");

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);

            XmlNode root = xmlDoc.DocumentElement;

            // Донори
            XmlNodeList donors = root.SelectNodes("//Donor");
            Console.WriteLine($"Донорів: {donors?.Count ?? 0}");

            // Організації
            XmlNodeList orgs = root.SelectNodes("//Organisation");
            Console.WriteLine($"Організацій: {orgs?.Count ?? 0}");

            // Звіти
            XmlNodeList reports = root.SelectNodes("//Report");
            Console.WriteLine($"Звітів: {reports?.Count ?? 0}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❗ Помилка: {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"Деталі: {ex.InnerException.Message}");
            }
        }
    }

    static void query1(XDocument xmlDoc)
    {
        //донори та їх середній взнос
        var query1 = xmlDoc.Descendants("Report")
            .GroupBy(r => r.Attribute("DonorId").Value)
            .Select(g => new
            {
                DonorId = g.Key,
                TotalDonated = g.Sum(r => decimal.Parse(r.Attribute("RecievedMoney").Value)),
                AvgDonation = g.Average(r => decimal.Parse(r.Attribute("RecievedMoney").Value))
            })
            .OrderByDescending(d => d.TotalDonated);

        Console.WriteLine("Знайти донорів, які жертвували найбільшу кількість коштів на проєкти, і для кожного донора вивести середній розмір жертви");
        foreach (var donor in query1)
        {
            Console.WriteLine($"Донор {donor.DonorId}: Всього пожертвував: {donor.TotalDonated}, Середній взнос: {donor.AvgDonation}");
        }
        Console.WriteLine();
    }

    static void query2(XDocument xmlDoc)
    {
        //огранізації та їх донати
        var query2 = xmlDoc.Descendants("Report")
            .GroupBy(r => r.Attribute("OrganisationId").Value)
            .Select(g => new
            {
                OrganisationId = g.Key,
                TotalRecieved = g.Sum(r => decimal.Parse(r.Attribute("RecievedMoney").Value))
            })
            .OrderByDescending(d => d.TotalRecieved);

        Console.WriteLine("Визначити організації, які отримали найбільше фінансування від спонсорів");
        foreach (var organisation in query2)
        {
            Console.WriteLine($"Id Організації: {organisation.OrganisationId} Отримала пожертвувань: {organisation.TotalRecieved}");
        }
        Console.WriteLine();
    }

    static void query3(XDocument xmlDoc)
    {
        //Вивести всіх донорів
        var query3 = xmlDoc.Descendants("Donor")
            .Select(d => new
            {
                Id = d.Attribute("DonorId").Value,
                Name = d.Attribute("DonorName").Value
            });
        Console.WriteLine($"Знайти всіх донорів");
        foreach (var donor in query3)
        {
            Console.WriteLine($"Донор: {donor.Name}, Id: {donor.Id}");
        }
        Console.WriteLine();
    }

    static void query4(XDocument xmlDoc)
    {
        //Знайти всі організації, до яких робили хочаб 1 пожертвування
        var query4 = xmlDoc.Descendants("Report")
            .GroupBy(r => r.Attribute("OrganisationId").Value)
            .Select(g => new
            {
                OrganisationId = g.Key,
            });

        Console.WriteLine("Знайти всі організації, до яких робили хочаб 1 пожертвування");
        foreach (var organisation in query4)
        {
            Console.WriteLine($"Id Організації: {organisation.OrganisationId}");
        }
        Console.WriteLine();
    }

    static void query5(XDocument xmlDoc)
    {
        //Знайти всі звіти, де сума пожертвувань більше 4000
        var query5 = xmlDoc.Descendants("Report")
            .Where(r => decimal.Parse(r.Attribute("RecievedMoney").Value) > 4000)
            .Select(r => new
            {
                OrganisationId = r.Attribute("OrganisationId").Value,
                DonorId = r.Attribute("DonorId").Value,
                Amount = decimal.Parse(r.Attribute("RecievedMoney").Value)
            });

        Console.WriteLine("Знайти всі звіти, де сума пожертвувань більше 4000");
        foreach (var report in query5)
        {
            Console.WriteLine($"Id Організації: {report.OrganisationId}, " + $"Id Донора: {report.DonorId}, " + $"Пожертвування: {report.Amount}");
        }
        Console.WriteLine();
    }

    static void query6(XDocument xmlDoc)
    {
        //Порахувати загальну суму всіх пожертвувань
        var query6 = xmlDoc.Descendants("Report")
        .Sum(r => decimal.Parse(r.Attribute("RecievedMoney").Value));

        Console.WriteLine("Порахувати загальну суму всіх пожертвувань");
        Console.WriteLine($"Загальна сума пожертвувань: {query6} грн");
        Console.WriteLine();
    }

    static void query7(XDocument xmlDoc)
    {
        //Порахувати середнє значення серед пожертвувань
        var query7 = xmlDoc.Descendants("Report")
            .Average(r => decimal.Parse(r.Attribute("RecievedMoney").Value));
        Console.WriteLine("Порахувати середнє значення серед пожертвувань");
        Console.WriteLine($"Середній розмір пожертвування: {query7:N2}");
        Console.WriteLine();
    }

    static void query8(XDocument xmlDoc)
    {
        //Для кожної організації визначити кількість та загальну суму пожертвувань
        var query8 = xmlDoc.Descendants("Report")
            .GroupBy(r => r.Attribute("OrganisationId").Value)
            .Select(g => new
            {
                OrganisationId = g.Key,
                TotalReceived = g.Sum(r => decimal.Parse(r.Attribute("RecievedMoney").Value)),
                PaymentCount = g.Count()
            });

        Console.WriteLine("Для кожної організації визначити кількість та загальну суму пожертвувань");
        foreach (var report in query8)
        {
            Console.WriteLine($"Id Організації: {report.OrganisationId}, Сума всіх пожертвувань: {report.TotalReceived}, Кількість пожертвувань: {report.PaymentCount}");
        }
        Console.WriteLine();
    }

    static void query9(XDocument xmlDoc)
    {
        //Знайти топ-3 донорів по сумі пожертвувань
        var query9 = xmlDoc.Descendants("Report")
            .GroupBy(r => r.Attribute("DonorId").Value)
            .Select(g => new
            {
                DonorId = g.Key,
                Total = g.Sum(r => decimal.Parse(r.Attribute("RecievedMoney").Value))
            })
            .OrderByDescending(d => d.Total)
            .Take(3);

        Console.WriteLine("Знайти топ-3 донорів по сумі пожертвувань");
        foreach (var donor in query9)
        {
            Console.WriteLine($"Донор {donor.DonorId}: {donor.Total} грн");
        }
        Console.WriteLine();
    }

    static void query10(XDocument xmlDoc)
    {
        //Знайти організації, які витратили менше, ніж отримали
        var query10 = xmlDoc.Descendants("Report")
            .GroupBy(r => r.Attribute("OrganisationId").Value)
            .Select(g => new
            {
                OrganisationId = g.Key,
                TotalReceived = g.Sum(r => decimal.Parse(r.Attribute("RecievedMoney").Value)),
                TotalSpent = g.Sum(r => decimal.Parse(r.Attribute("SpentMoney").Value))
            })
            .Where(org => org.TotalSpent < org.TotalReceived);

        Console.WriteLine("Знайти організації, які витратили менше, ніж отримали");
        foreach (var report in query10)
        {
            Console.WriteLine($"Id Організації: {report.OrganisationId}, Отримано: {report.TotalReceived}, Витрачено: {report.TotalSpent}");
        }
        Console.WriteLine();
    }

    static void query11(XDocument xmlDoc)
    {
        //Для кожного донора визначити організацію, який він допомагав
        var query11 = xmlDoc.Descendants("Report")
            .Join(xmlDoc.Descendants("Donor"),
                report => report.Attribute("DonorId").Value,
                donor => donor.Attribute("DonorId").Value,
                (report, donor) => new {
                    DonorName = donor.Attribute("DonorName").Value,
                    OrgId = report.Attribute("OrganisationId").Value
                })
            .GroupBy(x => x.DonorName)
            .Select(g => new {
                Donor = g.Key,
                Orgs = string.Join(", ", g.Select(x => x.OrgId).Distinct())
            });

        Console.WriteLine("Донори та організації, яким вони допомагали:");
        foreach (var item in query11)
        {
            Console.WriteLine($"{item.Donor}: {item.Orgs}");
        }
        Console.WriteLine();
    }

    static void query12(XDocument xmlDoc)
    {
        //Для кожної організації визначити який проєкт вона реалізувала
        var query12 = xmlDoc.Descendants("Project")
            .GroupBy(p => p.Attribute("OrganisationId").Value)
            .Select(g => new
            {
                OrganisationId = g.Key,
                ProjectIds = g.Select(p => p.Attribute("ProjectId").Value).ToList()
            });

        Console.WriteLine("Для кожної організації визначити який проєкт вона реалізувала");
        foreach (var result in query12)
        {
            Console.WriteLine($"Id Організації: {result.OrganisationId}, Id Проекта: {string.Join(", ", result.ProjectIds)}");
        }
    }

    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        XmlDocument doc = new XmlDocument();
        doc.Load("charity.xml");
        Console.WriteLine(doc.OuterXml);

        XDocument xmlDoc = XDocument.Load("charity.xml");

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Оберіть потрібний запит:");
            Console.WriteLine("1. Знайти донорів, які жертвували найбільшу кількість коштів на проєкти, " +
                "\nі для кожного донора вивести середній розмір жертви");
            Console.WriteLine("2. Визначити організації, які отримали найбільше фінансування від спонсорів");
            Console.WriteLine("3. Знайти всіх донорів");
            Console.WriteLine("4. Знайти всі організації, до яких робили хочаб 1 пожертвування");
            Console.WriteLine("5. Знайти всі звіти, де сума пожертвувань більше 4000");
            Console.WriteLine("6. Порахувати загальну суму всіх пожертвувань");
            Console.WriteLine("7. Порахувати середнє значення серед пожертвувань");
            Console.WriteLine("8. Для кожної організації визначити кількість та загальну суму пожертвувань");
            Console.WriteLine("9. Знайти топ-3 донорів по сумі пожертвувань");
            Console.WriteLine("10. Знайти організації, які витратили менше, ніж отримали");
            Console.WriteLine("11. Для кожного донора визначити організацію, який він допомагав");
            Console.WriteLine("12. Для кожної організації визначити який проєкт вона реалізувала");
            Console.WriteLine("13. Завантажити дані через XmlSerializer");
            Console.WriteLine("14. Завантажити дані через XmlDocument");
            Console.WriteLine("15. Вихід ");
            Console.Write("Оберіть пункт: ");

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Помилка! Нажміть будь-яку клавішу...");
                Console.ReadKey();
                continue;
            }

            switch (choice)
            {
                case 1:
                    query1(xmlDoc);
                    break;
                case 2:
                    query2(xmlDoc);
                    break;
                case 3:
                    query3(xmlDoc);
                    break;
                case 4:
                    query4(xmlDoc);
                    break;
                case 5:
                    query5(xmlDoc);
                    break;
                case 6:
                    query6(xmlDoc);
                    break;
                case 7:
                    query7(xmlDoc);
                    break;
                case 8:
                    query8(xmlDoc);
                    break;
                case 9:
                    query9(xmlDoc);
                    break;
                case 10:
                    query10(xmlDoc);
                    break;
                case 11:
                    query11(xmlDoc);
                    break;
                case 12:
                    query12(xmlDoc);
                    break;
                case 13:
                    DemonstrateXmlSerializer("charity.xml");
                    break;
                case 14:
                    DemonstrateXmlDocument("charity.xml");
                    break;
                case 15:
                    return;
                default:
                    Console.WriteLine("Неправильний пункт меню!");
                    break;
            }

            Console.WriteLine("\nНажміть будь яку клавішу для продовження...");
            Console.ReadKey();
        }
    }
}
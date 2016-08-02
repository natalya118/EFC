using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static PagesApp.Model;

namespace PagesApp
{
    public class Program
    {
        public static void Main()
        {
            using (var db = new BloggingContext())
            {
                //db.Pages.Add(new Page { UrlName = "hellololo2"});
                //var page = new Page
                //{
                //    UrlName = "mkfmlkf",
                //    NavLinks = new List<NavLink>
                //    {
                //        new NavLink {
                //        Title = "hjhj"
                //        }
                //    }
                //};
                //db.Pages.Add(page);
                //var count = db.SaveChanges();

                Console.WriteLine("Input '--help' to get info about supported commands");
               
                string correctCommand = @"add\s\w+\s\""{.*?}\""|update\s\w+\s\d+\s\""{.*?}\""|delete\s\w+\s\d+|list\sall|--help";
                while (true)
                {
                    Console.Write("task2>");
                    var command = Console.ReadLine();

                    string[] arr = command.Split(' ');
                    Match m = Regex.Match(command, correctCommand);
                    if (m.Success)
                    {
                        switch (arr[0])
                        {
                            case "add":
                                Add(command);
                                break;
                            case "update":
                                Update(command);
                                break;

                            case "delete":
                                Delete(command);
                                break;
                            case "list":
                                ListAll();
                                break;
                            case "--help":
                                Help();
                                break;
                            case "":
                                Console.WriteLine("Input the command");
                                break;

                            default:
                                Console.WriteLine(arr[0] + " is not recognized as an internal or external command.\nInput '--help' to get info about supported commands");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine(arr[0] + "is not recognized as an internal or external command.\nInput '--help' to get info about supported commands");

                    }
                }

            }
        }

        public static void Add(string command)
        {
            string[] arr = command.Split(' ');
            string json = Regex.Match(command, "{.*?}").Value;
            using (var db = new BloggingContext())
            {
                switch (arr[1])
                {
                    case "Pages":
                        
                        Page addPage = JsonConvert.DeserializeObject<Page>(json);
                        db.Pages.Add(addPage);
                        db.SaveChanges();
                        Console.WriteLine("New page added: " + addPage.ToString());
                        break;

                    case "NavLinks":

                        //NavLink addLink = JsonConvert.DeserializeObject<NavLink>(json);
                        //db.NavLinks.Add(addLink);
                        //db.SaveChanges();
                        //Console.WriteLine("New link added: " + addLink.ToString());
                        break;

                    case "RelatedPages":
                        RelatedPage addRel = JsonConvert.DeserializeObject<RelatedPage>(json);
                        db.RelatedPages.Add(addRel);
                        db.SaveChanges();
                        Console.WriteLine("New relations added: " + addRel.ToString());
                        break;
                    default:
                        Console.WriteLine("there is no such table in this database");
                        break;
                }
            }
        }
        public static void Update(string command)
        {
            string[] arr = command.Split(' ');
            string json = Regex.Match(command, "{.*?}").Value;
            using (var db = new BloggingContext())
            {
                switch (arr[1])
                {
                    case "Pages":


                        var pageForUpdate = db.Pages
                            .Single(p => p.PageId == int.Parse(arr[2]));

                        Type pType = pageForUpdate.GetType();
                        IList<PropertyInfo> props = new List<PropertyInfo>(pType.GetProperties());
                        Page newProperties = JsonConvert.DeserializeObject<Page>(json);
                        Type type = pageForUpdate.GetType();
                        foreach (PropertyInfo prop in props)
                        {
                            object propForUpdate = prop.GetType();
                            string name = prop.Name;
                            PropertyInfo pi = type.GetProperty(propForUpdate.ToString());
                            object propNew = prop.GetValue(newProperties, null);
                            if (propNew != null && !name.Equals("PageId"))
                            {
                                Console.WriteLine("Updated " + name);
                                prop.SetValue(pageForUpdate, propNew);
                            }

                            db.SaveChanges();
                        }
                        break;

                    case "NavLinks":

                        var linkForUpdate = db.NavLinks
                            .Single(nl => nl.Id == int.Parse(arr[2]));

                        Type lType = linkForUpdate.GetType();
                        IList<PropertyInfo> props2 = new List<PropertyInfo>(lType.GetProperties());
                        NavLink newLink = JsonConvert.DeserializeObject<NavLink>(json);
                        Type type2 = linkForUpdate.GetType();
                        foreach (PropertyInfo prop in props2)
                        {
                            object propForUpdate = prop.GetType();
                            string name = prop.Name;
                            PropertyInfo pi = type2.GetProperty(propForUpdate.ToString());
                            object propNew = prop.GetValue(newLink, null);
                            if (propNew != null && !name.EndsWith("Id"))
                            {
                                Console.WriteLine("Updated " + name);
                                prop.SetValue(linkForUpdate, propNew);
                            }

                            db.SaveChanges();
                        }
                        break;

                    case "RelatedPages":

                        break;
                    default:
                        Console.WriteLine("there is no such table in this database");
                        break;
                }
            }
        }
        public static void Delete(string command)
        {
            string[] arr = command.Split(' ');
            string json = Regex.Match(command, "{.*?}").Value;
            using (var db = new BloggingContext())
            {
                switch (arr[1])
                {
                    case "Pages":

                        var pageToDelete = db.Pages
                                         .Single(p => p.PageId == int.Parse(arr[2]));
                        db.Pages.Remove(pageToDelete);
                        db.SaveChanges();
                        Console.WriteLine("Page with id " + arr[2] + " was deleted");
                        break;

                    case "NavLinks":

                        var linkToDelete = db.NavLinks
                                         .Single(l => l.Id == int.Parse(arr[2]));
                        db.NavLinks.Remove(linkToDelete);
                        db.SaveChanges();
                        Console.WriteLine("Link with id " + arr[2] + " was deleted");
                        break;

                    case "RelatedPages":
                        var relToDelete = db.RelatedPages
                         .Single(l => l.RPId == int.Parse(arr[2]));
                        db.RelatedPages.Remove(relToDelete);
                        db.SaveChanges();
                        Console.WriteLine("Relations with id " + arr[2] + " was deleted");
                        break;
                    default:
                        Console.WriteLine("there is no such table in this database");
                        break;
                }
            }
        }
        public static void ListAll()
        {
            using (var db = new BloggingContext())
            {
                Console.WriteLine("All records in database");
                Console.WriteLine("Pages:");
                foreach (var p in db.Pages)
                {
                    Console.WriteLine(p.ToString());
                }
                Console.WriteLine("NavLinks:");
                foreach (var l in db.NavLinks)
                {
                    Console.WriteLine(l.ToString());
                }
                Console.WriteLine("RelatedPages:");
                foreach (var r in db.RelatedPages)
                {
                    Console.WriteLine(r.ToString());
                }
            }
        }
        public static void Help()
        {
            Console.WriteLine("Supported commands:\nadd <modelName> <json_with_record_data>\nupdate <modelName> <id> <json_with_changes>\ndelete <modelName> <id>\nlist all");
        }
    }
}
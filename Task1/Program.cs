using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task2._2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var db = new BloggingContext())
            {
                db.Blogs.Add(new Blog { Url = "http://distedu.ukma.edu.ua/" });
                db.Blogs.Add(new Blog { Url = "https://www.dotnetfoundation.org" });
                db.Blogs.Add(new Blog { Url = "http://dotnetblogengine.net/" });


                var blog = new Blog
                {
                    Url = "http://blogs.msdn.com/dotnet",
                    Posts = new List<Post>
                    {
                        new Post { Title = "Intro to C#" },
                        new Post { Title = "Intro to VB.NET" },
                        new Post { Title = "Intro to F#" }
                    }
                };

                db.Blogs.Add(blog);
		db.SaveChanges();
		Console.WriteLine("Blogs after adding");
		foreach (var b in db.Blogs)
                {
                    Console.WriteLine(" - {0}", b.Url);
                }

		Console.WriteLine("Blogs after deleting the first item");
		var blogRem = db.Blogs.First();
                db.Blogs.Remove(blogRem);
		db.SaveChanges();
                foreach (var b in db.Blogs)
                {
                    Console.WriteLine(" - {0}", b.Url);
                }

            }
        }
    }
}

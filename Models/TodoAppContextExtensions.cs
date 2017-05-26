using System;
using System.Linq;

namespace TodoApp.Models
{
    public static class TodoAppContextExtensions
    {
        public static void EnsureSeedData(this TodoAppContext context)
        {
            // Add some people
            if (!context.Persons.Any())
            {
                var travis = context.Persons.Add(new Person {FirstName = "Travis", LastName = "Wright"}).Entity;
                var joe = context.Persons.Add(new Person {FirstName = "Ken", LastName = "Van Hyning"}).Entity;
                var vin = context.Persons.Add(new Person {FirstName = "Vinson", LastName = "Yu"}).Entity;
                var mohamed = context.Persons.Add(new Person {FirstName = "Mohamed", LastName = "Shabar"}).Entity;

                // Add some tasks
                context.Tasks.AddRange(
                    new Task { Title = "ship helsinki", DueDate = new DateTime(2017,06,01), Priority = 1, IsComplete = false, Assignment = travis },
                    new Task { Title = "ship orcas", DueDate = new DateTime(2016,09,01), Priority = 3, IsComplete = false, Assignment = joe },
                    new Task { Title = "write SQL blog", DueDate = new DateTime(2016,06,01), Priority = 1, IsComplete = false, Assignment = joe },
                    new Task { Title = "publish sqlcmd", DueDate = new DateTime(2016,03,30), Priority = 2, IsComplete = false, Assignment = vin },
                    new Task { Title = "publish bcp", DueDate = new DateTime(2016,03,30), Priority = 2, IsComplete = false, Assignment = vin },
                    new Task { Title = "take our garbage", DueDate = new DateTime(2016,02,28), Priority = 1, IsComplete = true, Assignment = vin },
                    new Task { Title = "take a vacation", DueDate = new DateTime(2016,03,01), Priority = 0, IsComplete = false, Assignment = mohamed },
                    new Task { Title = "ship Carbon", DueDate = new DateTime(2016,04,01), Priority = 3, IsComplete = false, Assignment = mohamed }
                );
            }
            context.SaveChanges();
        }
    }
}
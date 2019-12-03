using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhereToGoWebApi.Models;

namespace WhereToGoWebApi.DataBaseContext.SeedData
{
    public class SampleData
    {
        public static void Proceed(EventDbContext dbContext)
        {
            User user;
            Organizer organizer;

            if (!dbContext.Users.Any())
            {
                user = new User { UserName = "Dmytro", LastName = "Laskuryk", Email = "dmytro@gmail.com", SecurityStamp = Guid.NewGuid().ToString() };

                dbContext.Users.Add(user);
                dbContext.SaveChanges();
            }
            else
            {
                user = dbContext.Users.FirstOrDefault();
            }

            if (!dbContext.Organizers.Any())
            {
                organizer = new Organizer { InstType = "some_info", PlaceName = "Cafe", TelNumber="11111", Position = "Boss", User = user };
                
                dbContext.Organizers.Add(organizer);
                dbContext.SaveChanges();
            }
            else
            {
                organizer = dbContext.Organizers.FirstOrDefault();
            }

            if (!dbContext.Events.Any())
            {
                var list = new List<Event> 
                {
                    new Event { Name = "Event_1", Description = "Description_1", Address = "Address_1", StartTime = DateTime.Now.AddDays(5), StartDate = DateTime.Now.AddDays(5), Price = 1_000, Quantity = 100, OrganizerId = organizer.OrganizerId },
                    new Event { Name = "Event_2", Description = "Description_2", Address = "Address_2", StartTime = DateTime.Now.AddDays(7), StartDate = DateTime.Now.AddDays(7), Price = 1_001, Quantity = 200, OrganizerId = organizer.OrganizerId },
                    new Event { Name = "Event_3", Description = "Description_3", Address = "Address_3", StartTime = DateTime.Now.AddDays(8), StartDate = DateTime.Now.AddDays(8), Price = 1_002, Quantity = 300, OrganizerId = organizer.OrganizerId },
                    new Event { Name = "Event_4", Description = "Description_4", Address = "Address_4", StartTime = DateTime.Now.AddDays(11), StartDate = DateTime.Now.AddDays(11), Price = 1_003, Quantity = 400, OrganizerId = organizer.OrganizerId },
                    new Event { Name = "Event_5", Description = "Description_5", Address = "Address_5", StartTime = DateTime.Now.AddDays(13), StartDate = DateTime.Now.AddDays(13), Price = 1_004, Quantity = 500, OrganizerId = organizer.OrganizerId },
                    new Event { Name = "Event_6", Description = "Description_6", Address = "Address_6", StartTime = DateTime.Now.AddDays(15), StartDate = DateTime.Now.AddDays(15), Price = 1_005, Quantity = 500, OrganizerId = organizer.OrganizerId },
                };

                dbContext.Events.AddRange(list);

                dbContext.SaveChanges();
            }
        }
    }
}

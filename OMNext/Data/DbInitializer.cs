using OMNext.Models;
using System;
using System.Linq;

namespace OMNext.Data
{
    public class DbInitializer
    {
        public static void Initialize(OM2018Context context)
        {
            //context.Database.EnsureCreated();
            // Look for any running missions.
            if (context.RunningMissions.Any())
            {
                return;     // DB has been seeded
            }

            var runningmission = new RunningMission()
            {
                FlightDirector = "Test Mission",
                Booth = "CLC1",
                MissionEnding = MissionEnding.Hurricane1,
                MissionDate = DateTime.Now,
            };

            context.RunningMissions.Add(runningmission);
            context.SaveChanges();

            var administrator = new Administrator()
            {
                FirstName = "Test",
                LastName = "User",
                UserName = "TestAdmin",
                Password = "Test"
            };

            context.Administrators.Add(administrator);
            context.SaveChanges();
        }
    }
}

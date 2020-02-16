using FCArsenal.Models;
using System;
using System.Linq;

namespace FCArsenal.Data
{
    public static class DbInitializer
    {
        public static void Initialize(FootballContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Players.Any())
            {
                return;   // DB has been seeded
            }

            var students = new Player[]
            {
            new Player{FirstMidName="Carson",LastName="Alexander",SigningDate=DateTime.Parse("2005-09-01")},
            new Player{FirstMidName="Meredith",LastName="Alonso",SigningDate=DateTime.Parse("2002-09-01")},
            new Player{FirstMidName="Arturo",LastName="Anand",SigningDate=DateTime.Parse("2003-09-01")},
            new Player{FirstMidName="Gytis",LastName="Barzdukas",SigningDate=DateTime.Parse("2002-09-01")},
            new Player{FirstMidName="Yan",LastName="Li",SigningDate=DateTime.Parse("2002-09-01")},
            new Player{FirstMidName="Peggy",LastName="Justice",SigningDate=DateTime.Parse("2001-09-01")},
            new Player{FirstMidName="Laura",LastName="Norman",SigningDate=DateTime.Parse("2003-09-01")},
            new Player{FirstMidName="Nino",LastName="Olivetto",SigningDate=DateTime.Parse("2005-09-01")}
            };
            foreach (Player s in students)
            {
                context.Players.Add(s);
            }
            context.SaveChanges();

            var courses = new Training[]
            {
            new Training{TrainingID=1050,Title="Field practice",Credits=3},
            new Training{TrainingID=4022,Title="Field practice",Credits=3},
            new Training{TrainingID=4041,Title="Gym",Credits=3},
            new Training{TrainingID=1045,Title="Stamina",Credits=4},
            new Training{TrainingID=3141,Title="Sprints",Credits=4},
            new Training{ TrainingID = 2021,Title="Tactics",Credits=3},
            new Training{ TrainingID = 2042,Title="Gym",Credits=4}
            };
            foreach (Training c in courses)
            {
                context.Trainings.Add(c);
            }
            context.SaveChanges();

            var enrollments = new Signing[]
            {
            new Signing{PlayerID=1,TrainingID=1050,Form=Form.A},
            new Signing{PlayerID=1,TrainingID=4022,Form=Form.C},
            new Signing{PlayerID=1,TrainingID=4041,Form=Form.B},
            new Signing{PlayerID=2,TrainingID=1045,Form=Form.B},
            new Signing{PlayerID=2,TrainingID=3141,Form=Form.F},
            new Signing{PlayerID=2,TrainingID=2021,Form=Form.F},
            new Signing{PlayerID=3,TrainingID=1050},
            new Signing{PlayerID=4,TrainingID=1050},
            new Signing{PlayerID=4,TrainingID=4022,Form=Form.F},
            new Signing{PlayerID=5,TrainingID=4041,Form=Form.C},
            new Signing{PlayerID=6,TrainingID=1045},
            new Signing{PlayerID=7,TrainingID=3141,Form=Form.A},
            };
            foreach (Signing e in enrollments)
            {
                context.Signings.Add(e);
            }
            context.SaveChanges();
        }
    }
}
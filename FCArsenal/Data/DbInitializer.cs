using FCArsenal.Models;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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

            var staffs = new Staff[]
            {
                new Staff { FirstMidName = "Kim",     LastName = "Abercrombie",
                    HireDate = DateTime.Parse("1995-03-11") },
                new Staff { FirstMidName = "Fadi",    LastName = "Fakhouri",
                    HireDate = DateTime.Parse("2002-07-06") },
                new Staff { FirstMidName = "Roger",   LastName = "Harui",
                    HireDate = DateTime.Parse("1998-07-01") },
                new Staff { FirstMidName = "Candace", LastName = "Kapoor",
                    HireDate = DateTime.Parse("2001-01-15") },
                new Staff { FirstMidName = "Roger",   LastName = "Zheng",
                    HireDate = DateTime.Parse("2004-02-12") }
            };

            foreach (Staff i in staffs)
            {
                context.Staffs.Add(i);
            }
            context.SaveChanges();

            var departments = new Department[]
            {
                new Department { Name = "Grass Training Pitch", Budget = 350000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    StaffID  = staffs.Single( i => i.LastName == "Abercrombie").ID },
                new Department { Name = "Artifical Grass Training Pitch", Budget = 100000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    StaffID  = staffs.Single( i => i.LastName == "Fakhouri").ID },
                new Department { Name = "Gym", Budget = 350000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    StaffID  = staffs.Single( i => i.LastName == "Harui").ID },
                new Department { Name = "Stadion",   Budget = 100000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    StaffID  = staffs.Single( i => i.LastName == "Kapoor").ID },
                new Department { Name = "Tactics Class",   Budget = 200000,
                    StartDate = DateTime.Parse("2009-07-12"),
                    StaffID  = staffs.Single( i => i.LastName == "Zheng").ID }
            };

            foreach (Department d in departments)
            {
                context.Departments.Add(d);
            }
            context.SaveChanges();


            var trainings = new Training[]
            {
            new Training{TrainingID=1050,Title="Field practice",Credits=3, DepartmentID = departments.Single( s => s.Name == "Grass Training Pitch").DepartmentID},
            new Training{TrainingID=4022,Title="Field practice",Credits=3, DepartmentID = departments.Single( s => s.Name == "Artifical Grass Training Pitch").DepartmentID},
            new Training{TrainingID=4041,Title="Gym",Credits=3, DepartmentID = departments.Single( s => s.Name == "Gym").DepartmentID},
            new Training{TrainingID=1045,Title="Stamina",Credits=4, DepartmentID = departments.Single( s => s.Name == "Stadion").DepartmentID},
            new Training{TrainingID=3141,Title="Sprints",Credits=4, DepartmentID = departments.Single( s => s.Name == "Stadion").DepartmentID},
            new Training{ TrainingID = 2021,Title="Tactics",Credits=3, DepartmentID = departments.Single( s => s.Name == "Tactics Class").DepartmentID},
            new Training{ TrainingID = 2042,Title="Gym",Credits=4, DepartmentID = departments.Single( s => s.Name == "Gym").DepartmentID}
            };
            foreach (Training c in trainings)
            {
                context.Trainings.Add(c);
            }
            context.SaveChanges();


            var officeAssignments = new OfficeAssignment[]
           {
                new OfficeAssignment {
                    StaffID = staffs.Single( i => i.LastName == "Fakhouri").ID,
                    Location = "Smith 17" },
                new OfficeAssignment {
                    StaffID = staffs.Single( i => i.LastName == "Harui").ID,
                    Location = "Gowan 27" },
                new OfficeAssignment {
                    StaffID = staffs.Single( i => i.LastName == "Kapoor").ID,
                    Location = "Thompson 304" },
           };

            foreach (OfficeAssignment o in officeAssignments)
            {
                context.OfficeAssignments.Add(o);
            }
            context.SaveChanges();


            var trainingStaffs = new TrainingAssignment[]
            {
            new TrainingAssignment {
                TrainingID = trainings.Single(c => c.Title == "Stamina" && c.DepartmentID == departments.Single(s => s.Name == "Stadion").DepartmentID).TrainingID,
                StaffID = staffs.Single(i => i.LastName == "Kapoor").ID
            },
            new TrainingAssignment {
                TrainingID = trainings.Single(c => c.Title == "Gym" && c.DepartmentID == departments.Single(s => s.Name == "Gym").DepartmentID && c.Credits == 3).TrainingID,
                StaffID = staffs.Single(i => i.LastName == "Harui").ID
            },
            new TrainingAssignment {
                TrainingID = trainings.Single(c => c.Title == "Tactics" && c.DepartmentID == departments.Single(s => s.Name == "Tactics Class").DepartmentID).TrainingID,
                StaffID = staffs.Single(i => i.LastName == "Zheng").ID
            },
            new TrainingAssignment {
                TrainingID = trainings.Single(c => c.Title == "Tactics" && c.DepartmentID == departments.Single(s => s.Name == "Tactics Class").DepartmentID).TrainingID,
                StaffID = staffs.Single(i => i.LastName == "Kapoor").ID
            },
            new TrainingAssignment {
                TrainingID = trainings.Single(c => c.Title == "Field practice" && c.DepartmentID == departments.Single(s => s.Name == "Grass Training Pitch").DepartmentID).TrainingID,
                StaffID = staffs.Single(i => i.LastName == "Fakhouri").ID
            },
            new TrainingAssignment {
                TrainingID = trainings.Single(c => c.Title == "Gym" && c.DepartmentID == departments.Single(s => s.Name == "Gym").DepartmentID && c.Credits == 4).TrainingID,
                StaffID = staffs.Single(i => i.LastName == "Harui").ID
            },
            new TrainingAssignment {
                TrainingID = trainings.Single(c => c.Title == "Field practice" && c.DepartmentID == departments.Single(s => s.Name == "Grass Training Pitch").DepartmentID).TrainingID,
                StaffID = staffs.Single(i => i.LastName == "Abercrombie").ID
            },
            new TrainingAssignment
            {
                TrainingID = trainings.Single(c => c.Title == "Field practice" && c.DepartmentID == departments.Single(s => s.Name == "Artifical Grass Training Pitch").DepartmentID).TrainingID,
                StaffID = staffs.Single(i => i.LastName == "Abercrombie").ID
            }
            };

            foreach (TrainingAssignment ci in trainingStaffs)
            {
                context.TrainingAssignments.Add(ci);
            }
            context.SaveChanges();


            var signings = new Signing[]
            {
            
                new Signing {
                    PlayerID = students.Single(s => s.LastName == "Alexander").ID,
                    TrainingID = trainings.Single(c => c.Title == "Field practice" && c.DepartmentID == departments.Single(s => s.Name == "Grass Training Pitch").DepartmentID).TrainingID,
                    Form = Form.A
                },
                    new Signing {
                    PlayerID = students.Single(s => s.LastName == "Alexander").ID,
                    TrainingID = trainings.Single(c => c.Title == "Tactics"  && c.DepartmentID == departments.Single(s => s.Name == "Tactics Class").DepartmentID).TrainingID,
                    Form = Form.C
                    },
                    new Signing {
                    PlayerID = students.Single(s => s.LastName == "Alexander").ID,
                    TrainingID = trainings.Single(c => c.Title == "Field practice" && c.DepartmentID == departments.Single(s => s.Name == "Artifical Grass Training Pitch").DepartmentID).TrainingID,
                    Form = Form.B
                    },
                    new Signing {
                    PlayerID = students.Single(s => s.LastName == "Alonso").ID,
                    TrainingID = trainings.Single(c => c.Title == "Field practice" && c.DepartmentID == departments.Single(s => s.Name == "Grass Training Pitch").DepartmentID).TrainingID,
                    Form = Form.B
                    },
                    new Signing {
                    PlayerID = students.Single(s => s.LastName == "Alonso").ID,
                    TrainingID = trainings.Single(c => c.Title == "Tactics"  && c.DepartmentID == departments.Single(s => s.Name == "Tactics Class").DepartmentID).TrainingID,
                    Form = Form.B
                    },
                    new Signing {
                    PlayerID = students.Single(s => s.LastName == "Alonso").ID,
                    TrainingID = trainings.Single(c => c.Title == "Gym" && c.DepartmentID == departments.Single(s => s.Name == "Gym").DepartmentID && c.Credits == 4).TrainingID,
                    Form = Form.B
                    },
                    new Signing {
                    PlayerID = students.Single(s => s.LastName == "Anand").ID,
                    TrainingID = trainings.Single(c => c.Title == "Gym" && c.DepartmentID == departments.Single(s => s.Name == "Gym").DepartmentID && c.Credits == 3).TrainingID
                    },
                    new Signing {
                    PlayerID = students.Single(s => s.LastName == "Anand").ID,
                    TrainingID = trainings.Single(c => c.Title == "Field practice" && c.DepartmentID == departments.Single(s => s.Name == "Artifical Grass Training Pitch").DepartmentID).TrainingID,
                    Form = Form.B
                    },
                    new Signing {
                    PlayerID = students.Single(s => s.LastName == "Barzdukas").ID,
                    TrainingID = trainings.Single(c => c.Title == "Tactics" && c.DepartmentID == departments.Single(s => s.Name == "Tactics Class").DepartmentID).TrainingID,
                    Form = Form.B
                    },
                    new Signing {
                    PlayerID = students.Single(s => s.LastName == "Li").ID,
                    TrainingID = trainings.Single(c => c.Title == "Gym" && c.DepartmentID == departments.Single(s => s.Name == "Gym").DepartmentID && c.Credits == 4).TrainingID,
                    Form = Form.B
                    },
                    new Signing {
                    PlayerID = students.Single(s => s.LastName == "Justice").ID,
                    TrainingID = trainings.Single(c => c.Title == "Gym" && c.DepartmentID == departments.Single(s => s.Name == "Gym").DepartmentID && c.Credits == 3).TrainingID,
                    Form = Form.B
                    }
            };
            foreach (Signing e in signings)
            {
                var signingInDataBase = context.Signings.Where(
                    s =>
                            s.Player.ID == e.PlayerID &&
                            s.Training.TrainingID == e.TrainingID).SingleOrDefault();
                if (signingInDataBase == null)
                {
                    context.Signings.Add(e);
                }
            }
            context.SaveChanges();
        }
    }
}
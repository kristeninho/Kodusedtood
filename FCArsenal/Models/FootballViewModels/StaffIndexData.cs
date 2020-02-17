using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FCArsenal.Models.SchoolViewModels
{
    public class StaffIndexData
    {
        public IEnumerable<Staff> Staffs { get; set; }
        public IEnumerable<Training> Trainings { get; set; }
        public IEnumerable<Signing> Signings { get; set; }
    }
}
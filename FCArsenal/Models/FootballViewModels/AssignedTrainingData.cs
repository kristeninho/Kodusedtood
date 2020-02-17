using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FCArsenal.Models.FootballViewModels
{
    public class AssignedTrainingData
    {
        public int TrainingID { get; set; }
        public string Title { get; set; }
        public bool Assigned { get; set; }
    }
}
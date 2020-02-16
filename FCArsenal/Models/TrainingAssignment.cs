using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FCArsenal.Models
{
    public class TrainingAssignment
    {
        public int StaffID { get; set; }
        public int TrainingID { get; set; }
        public Staff Staff { get; set; }
        public Training Training { get; set; }
    }
}
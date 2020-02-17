using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FCArsenal.Models
{
    public class Staff : Person
    {
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }

        public ICollection<TrainingAssignment> TrainingAssignments { get; set; }
        public OfficeAssignment OfficeAssignment { get; set; }
    }
}
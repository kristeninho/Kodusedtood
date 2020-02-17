using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FCArsenal.Models
{
    public class Player : Person
    {
       
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Signing Date")]
        public DateTime SigningDate { get; set; }

        public ICollection<Signing> Signings { get; set; }
    }
}
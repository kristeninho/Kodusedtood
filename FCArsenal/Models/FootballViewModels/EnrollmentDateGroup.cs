using System;
using System.ComponentModel.DataAnnotations;

namespace FCArsenal.Models.FootballViewModels
{
    public class SigningDateGroup
    {
        [DataType(DataType.Date)]
        public DateTime? SigningDate { get; set; }

        public int PlayerCount { get; set; }
    }
}
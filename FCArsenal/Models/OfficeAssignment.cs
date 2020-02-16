using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FCArsenal.Models
{
    public class OfficeAssignment
    {
        [Key]
        public int StaffID { get; set; }
        [StringLength(50)]
        [Display(Name = "Office Location")]
        public string Location { get; set; }

        public Staff Staff { get; set; }
    }
}
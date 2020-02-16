using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FCArsenal.Models
{
    public enum Form
    {
        A, B, C, D, F
    }

    public class Signing
    {
        public int SigningID { get; set; }
        public int TrainingID { get; set; }
        public int PlayerID { get; set; }
        [DisplayFormat(NullDisplayText = "No feedback")]
        public Form? Form { get; set; }

        public Training Training { get; set; }
        public Player Player { get; set; }
    }
}
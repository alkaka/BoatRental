using System.ComponentModel.DataAnnotations;

namespace BoatRental.Models
{

    public class BoatType
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Boat type is required"), StringLength(20),]
        [Display(Name = "Boat Type")]
        public string BoatsType { get; set; }

    }
}
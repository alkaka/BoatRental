using System;
using System.ComponentModel.DataAnnotations;

namespace BoatRental.Models
{
    public class Boat
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Boat Number is required"), StringLength(6)]
        [Display(Name = "Boat Nmber")]
        public string BoatNmber { get; set; }

        [Required(ErrorMessage = "Booking Nmber is required"), StringLength(6)]
        [Display(Name = "Booking Nmber")]
        public string BookingNmber { get; set; }

        [Display(Name = "Check In Date")]
        public DateTime CheckInTime { get; set; }

        [Required(ErrorMessage = "Customer personal number is required")]
        [Display(Name = "CPN")]
        public int CustomerPersonalNumber { get; set; }


        //Navigation properties, not in EF-database
        public int BoatTypeId { get; set; }
        public BoatType BoatType { get; set; }

    }
}

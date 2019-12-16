using System;
using System.ComponentModel.DataAnnotations;

namespace BoatRental.Models
{
    public class RecieptViewModel
    {
        [Display(Name = "Boat Nmber")]
        public string BoatNmber { get; set; }
        [Display(Name = "Booking Nmber")]
        public string BookingNmber { get; set; }
        [Display(Name = "CPN")]
        public int CustomerPersonalNumber { get; set; }
        [Display(Name = "Boat Type")]
        public string BoatType { get; set; }
        [Display(Name = "Check In Time")]
        public DateTime CheckInTime { get; set; }
        [Display(Name = "Check Out Time")]
        public DateTime CheckOutTime { get; set; }
        [Display(Name = "Total Time")]
        [DisplayFormat(DataFormatString = "{0:%d} days {0:%h} hours {0:%m} minutes")]
        public string TotalTime { get; set; }
        [Display(Name = "Total Price")]
        public string Price { get; set; }
    }
}
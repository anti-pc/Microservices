using System.ComponentModel.DataAnnotations;

namespace FreeCourse.Web.Models.Order
{
    public class CheckoutInfoInput
    {
        [Display(Name="City")]
        public string Province { get; set; }

        public string District { get; set; }

        public string Street { get; set; }

        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }

        [Display(Name = "Address")]
        public string Line { get; set; }


        [Display(Name = "Firstname Lastname")]
        public string CardName { get; set; }

        [Display(Name = "Card Number")]
        public string CardNumber { get; set; }

        [Display(Name = "Expiration Date (month/year)")]
        public string Expiration { get; set; }

        [Display(Name = "CVV / CVC2")]
        public string CVV { get; set; }

        [Display(Name = "Total Price")]
        public decimal TotalPrice { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace aspnetcore_sagepay.Models
{
    public class CardholderModel
    {
        [Required(ErrorMessage = "This is a required field")]
        [Display(Name = "Card holder name")]
        public string CardholderName { get; set; }

        [Required(ErrorMessage = "This is a required field")]
        [Display(Name = "Card number")]
        //[StringLength(16, MinimumLength = 16, ErrorMessage = "Card number should not exceed 16 characters")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "This is a required field")]
        [Display(Name = "Expiry date")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Expiry date must be 4 characters in MMYY format")]
        public string ExpiryDate { get; set; }

        [Required(ErrorMessage = "This is a required field")]
        [Display(Name = "Security code")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Security code must be 3 characters")]
        public string SecurityCode { get; set; }
    }
}
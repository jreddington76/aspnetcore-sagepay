using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace aspnetcore_sagepay.Models.Request
{
    public class MerchantSessionKeyRequestModel
    {
        [Required]
        [Description("Your Sage Pay vendor name.")]
        public string VendorName { get; set; }
    }
}
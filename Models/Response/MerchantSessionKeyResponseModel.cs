using System;
using System.ComponentModel;

namespace aspnetcore_sagepay.Models.Response
{
    public class MerchantSessionKeyResponseModel
    {
        [Description("Unique key used in card identifier creation and transaction registration.")]
        public string MerchantSessionKey { get; set; }

        [Description("Date/Time the merchant session key will expire in ISO 8601 format.")]
        public DateTime Expiry { get; set; }
    }
}
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace aspnetcore_sagepay.Models
{
    public class TransactionModel
    {
        [Required]
        [Description("The type of transaction requested [Payment, Deferred]")]
        public string TransactionType { get; set; }

        [Required]
        [Description("The payment method for the transaction. In this object you will need to provide us with the cardIdentifier and the merchantSessionKey used to create that Card Identifier.")]
        public PaymentMethodModel PaymentMethod { get; set; }

        [Required]
        [Description("Your unique reference for this transaction. Maximum of 40 characters.")]
        public string VendorTxCode { get; set; }

        [Required]
        [Description("The amount in the smallest currency unit. (e.g. 100 pence to charge £1.00).")]
        public int Amount { get; set; }

        [Required]
        [Description("The currency of the amount in 3 letter ISO 4217 format.")]
        public string Currency { get; set; }

        [Required]
        [Description("A brief description of the goods or services purchased.")]
        public string Description { get; set; }

        [Required]
        [Description("Customer’s first names.")]
        public string CustomerFirstName { get; set; }

        [Required]
        [Description("Customer’s last name.")]
        public string CustomerLastName { get; set; }

        [Required]
        [Description("Details for the customer’s billing address.")]
        public AddressModel BillingAddress { get; set; }

        [Description("The method used to capture card data [Ecommerce, MailOrder, TelephoneOrder] - Default Ecommerce")]
        public string EntryMethod { get; set; }

        [Description("Identifies the customer has ticked a box to indicate that they wish to receive tax back on their donation. Default false")]
        public bool GiftAid { get; set; }

        [Description("Use this field to override your default account level 3-D Secure settings: [UseMSPSetting - Use default MySagePay settings, Force - Apply authentication even if turned off, Disable - Disable authentication and rules, ForceIgnoringRules - Apply authentication but ignore rules] Default UseMSPSetting")]
        public string Apply3DSecure { get; set; }

        [Description("Use this field to override your default account level AVS CVC settings: [UseMSPSetting - Use default MySagePay settings, Force - Apply authentication even if turned off, Disable - Disable authentication and rules, ForceIgnoringRules - Apply authentication but ignore rules] Default UseMSPSetting")]
        public string ApplyAvsCvcCheck { get; set; }

        [Description("Customer’s email address.")]
        public string CustomerEmail { get; set; }

        [Description("Customer’s phone number.")]
        public string CustomerPhone { get; set; }

        [Description("Defaults to billingAddress object property values if not supplied.")]
        public ShippingDetailsModel ShippingDetails { get; set; }

        [Description("This can be used to send the unique reference for the partner that referred the merchant to Sage Pay. Maximum of 40 characters.")]
        public string ReferrerId { get; set; }
    }


    [Description("The paymentMethod object specifies the payment method for the transaction.")]
    public class PaymentMethodModel
    {
        [Description("Details of the customer’s credit or debit card.")]
        public CardModel Card { get; set; }
    }


    [Description("The card object represents the credit or debit card details for this transaction.")]
    public class CardModel
    {
        [Required]
        [Description("The merchant session key used to generate the cardIdentifier.")]
        public string MerchantSessionKey { get; set; }

        [Required]
        [Description("The unique reference of the card you want to charge.")]
        public string CardIdentifier { get; set; }

        [Description("A flag to indicate the card identifier is reusable, i.e. it has been created previously. Default false")]
        public bool Reuseable { get; set; }

        [Description("A flag to indicate that you want to save the card identifier, i.e. make it reusable. Default false")]
        public bool Save { get; set; }
    }


    [Description("The billingAddress object is used to define the customer’s billing address details. The billing address details will also be used as shipping address details if the shippingDetails object is not provided for a transaction.")]
    public class AddressModel
    {
        [Required]
        [Description("Billing address line 1, used in AVS check.")]
        public string Address1 { get; set; }

        [Description("Billing address line 2.")]
        public string Address2 { get; set; }

        [Required]
        [Description("Billing city.")]
        public string City { get; set; }

        // todo: create custom attribute that makes this NOT required if Country is "IE" [Ireland]
        [Description("Billing postal code, used in AVS check. Not required when country is IE.")]
        public string PostalCode { get; set; }

        [Required]
        [Description("Two letter country code defined in ISO 3166-1.")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "Country must be 2 characters in ISO 3166-1 format")]
        public string Country { get; set; }

        // todo: create custom attribute that makes this required if Country is "US"
        [Description("Two letter state code defined in ISO 3166-2. Required when country is US.")]
        public string State { get; set; }
    }


    [Description("The shippingDetails object is used to specify the shipping address details for a transaction. If not provided the values provided in the billingAddress object will be used for shipping information instead.")]
    public class ShippingDetailsModel
    {
        [Required]
        [Description("Recipient’s first names.")]
        public string RecipientFirstName { get; set; }

        [Required]
        [Description("Recipient’s last name.")]
        public string RecipientLastName { get; set; }

        [Required]
        [Description("Shipping address line 1.")]
        public string ShippingAddress1 { get; set; }

        [Description("Shipping address line 2.")]
        public string ShippingAddress2 { get; set; }

        [Required]
        [Description("Shipping city.")]
        public string ShippingCity { get; set; }

        // todo: create custom attribute that makes this NOT required if Country is "IE" [Ireland]
        [Description("Shipping postal code. Not Required when shippingCountry is IE.")]
        public string ShippingPostalCode { get; set; }

        [Required]
        [Description("Shipping country. Two letter country code defined in ISO 3166-1.")]
        public string ShippingCountry { get; set; }

        // todo: create custom attribute that makes this required if Country is "US"
        [Description("Two letter state code defined in ISO 3166-2. Required when shippingCountry is US.")]
        public string ShippingState { get; set; }
    }
}
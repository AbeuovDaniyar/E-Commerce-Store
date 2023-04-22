using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProject.Models
{

    /* The Supplements class contains properties and a constructor for supplement products. */
    public class Supplements
    {
        public int supplementId { get; set; }
        public string productExpirationDate { get; set; }
        public string productCode { get; set; }
        public string productUPCCode { get; set; }
        public int productPckgQuantity { get; set; }
        public float productShippingWeight { get; set; }
        public string productDimensions { get; set; }
        public string productKeyWords { get; set; }
        public string productSuggestedUse { get; set; }
        public string productIngridients { get; set; }
        public string productWarnings { get; set; }
        public string productDisclaimer { get; set; }
        public int productId { get; set; }
        public string productManufacturerLink { get; set; }
        
        
        /* This is a constructor for the Supplements class. It takes in 14 parameters, each
        representing a property of a supplement product. The constructor then sets the values of
        these properties for a specific instance of the Supplements class using the "this" keyword. */
        public Supplements(int supplementId, string productExpirationDate, string productCode, string productUPCCode, int productPckgQuantity, float productShippingWeight, string productDimensions, string productKeyWords, string productSuggestedUse, string productIngridients, string productWarnings, string productDisclaimer, int productId, string productManufacturerLink)
        {
            this.supplementId = supplementId;
            this.productExpirationDate = productExpirationDate;
            this.productCode = productCode;
            this.productUPCCode = productUPCCode;
            this.productPckgQuantity = productPckgQuantity;
            this.productShippingWeight = productShippingWeight;
            this.productDimensions = productDimensions;
            this.productKeyWords = productKeyWords;
            this.productSuggestedUse = productSuggestedUse;
            this.productIngridients = productIngridients;
            this.productWarnings = productWarnings;
            this.productDisclaimer = productDisclaimer;
            this.productId = productId;
            this.productManufacturerLink = productManufacturerLink;
        }
    }
}

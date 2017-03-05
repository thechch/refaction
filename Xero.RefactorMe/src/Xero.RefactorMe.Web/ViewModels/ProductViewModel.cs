using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Xero.RefactorMe.Web.ViewModels
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        
        [Range(0, 9999999999999999.99)]
        [RegularExpression(@"^\d+\.\d{0,2}$")]
        public decimal Price { get; set; }
        
        [Range(0, 9999999999999999.99)]
        [RegularExpression(@"^\d+\.\d{0,2}$")]
        public decimal DeliveryPrice { get; set; }
        
        public ICollection<ProductOptionViewModel> ProductOptions { get; set; }
    }
}
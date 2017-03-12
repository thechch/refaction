using System;
using System.ComponentModel.DataAnnotations;

namespace Xero.RefactorMe.Web.ViewModels
{
    public class ProductViewModel
    {
        public Guid ProductId { get; set; }

        [StringLength(100, MinimumLength = 1, ErrorMessage = "Must be shorter than 100 symbols")]
        [Required(ErrorMessage = "Please, enter the name of the product")]
        public string Name { get; set; }

        [StringLength(500, MinimumLength = 1, ErrorMessage = "Must be shorter than 100 symbols")]
        public string Description { get; set; }

        [Range(0, 9999999999999999.99, ErrorMessage = "Must be greater than 0")]
        [RegularExpression(@"^\d+\.\d{0,2}$", ErrorMessage = "Just two decimal places please")]
        [Required(ErrorMessage = "Please, enter a value")]
        public decimal Price { get; set; }

        [Range(0, 9999999999999999.99, ErrorMessage = "Must be greater than 0")]
        [RegularExpression(@"^\d+\.\d{0,2}$", ErrorMessage = "Just two decimal places please")]
        [Required(ErrorMessage = "Please, enter a value")]
        public decimal DeliveryPrice { get; set; }
    }
}
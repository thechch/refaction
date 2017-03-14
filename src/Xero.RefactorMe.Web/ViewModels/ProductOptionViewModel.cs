using System;
using System.ComponentModel.DataAnnotations;

namespace Xero.RefactorMe.Web.ViewModels
{
    public class ProductOptionViewModel
    {
        public Guid Id { get; set; }

        [StringLength(100, MinimumLength = 1, ErrorMessage = "Must be shorter than 100 symbols")]
        [Required(ErrorMessage = "Please, enter the name of the product")]
        public string Name { get; set; }

        [StringLength(500, MinimumLength = 1, ErrorMessage = "Must be shorter than 100 symbols")]
        public string Description { get; set; }

        public Guid ProductId { get; set; }
    }
}
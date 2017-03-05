using System;

namespace Xero.RefactorMe.Web.ViewModels
{
    public class ProductOptionViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Guid ProductId { get; set; }
    }
}
using Microsoft.AspNetCore.Mvc;

namespace Xero.RefactorMe.Web.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        //GET api/products
        [HttpGet]
        public Products GetAll() {
            
        }
    }
}
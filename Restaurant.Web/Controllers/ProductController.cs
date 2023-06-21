using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Restaurant.Web.Models;
using Restaurant.Web.Services.IServices;

namespace Restaurant.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
                _productService = productService;
        }
        public async Task<IActionResult> ProductIndex()
        {
            List<ProductDto> list = new();
            var response = await _productService.GetSAllProductsAsync<ResponseDto>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
    }
}

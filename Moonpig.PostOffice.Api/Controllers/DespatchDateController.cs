using Moonpig.PostOffice.Core;
using Moonpig.PostOffice.Core.Model;

namespace Moonpig.PostOffice.Api.Controllers
{
    using Data;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    public class DespatchDateController : Controller
    {
        private OrderService _orderService;

        public DespatchDateController()
        {
            _orderService = new OrderService(new ProductRepository(new DbContext()));
        }

        [HttpGet]
        public DespatchDate Get(DespatchDateRequest request)
        {
            return _orderService.GetDespatchDate(request);
        }
    }
     
}

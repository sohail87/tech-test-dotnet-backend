using Moonpig.PostOffice.Api.Model;
using Moonpig.PostOffice.Core;

namespace Moonpig.PostOffice.Api.Controllers
{
    using System;
    using System.Collections.Generic;
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
        public DespatchDate Get(List<int> productIds, DateTime orderDate)
        {
            return _orderService.GetDespatchDate(productIds, orderDate);
        }
    }
}

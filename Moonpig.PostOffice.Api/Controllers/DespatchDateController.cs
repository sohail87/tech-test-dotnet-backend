namespace Moonpig.PostOffice.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Microsoft.AspNetCore.Mvc;
    using Model;

    public class OrderService
    {
        private DbContext _dbContext;

        public OrderService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public DespatchDate GetDespatchDate(List<int> productIds, DateTime orderDate)
        {
            var maxDespatchDate = orderDate;

            foreach (var productId in productIds)
            {
                var supplierId = _dbContext.Products.Single(x => x.ProductId == productId).SupplierId;
                var leadTimeInDays = _dbContext.Suppliers.Single(x => x.SupplierId == supplierId).LeadTime;
                if (orderDate.AddDays(leadTimeInDays) > maxDespatchDate)
                    maxDespatchDate = orderDate.AddDays(leadTimeInDays);
            }

            if (maxDespatchDate.DayOfWeek == DayOfWeek.Saturday)
                return new DespatchDate { Date = maxDespatchDate.AddDays(2) };
            if (maxDespatchDate.DayOfWeek == DayOfWeek.Sunday)
                return new DespatchDate { Date = maxDespatchDate.AddDays(1) };
            return new DespatchDate { Date = maxDespatchDate };
        }


    }

    [Route("api/[controller]")]
    public class DespatchDateController : Controller
    {
        private OrderService _orderService;

        public DespatchDateController()
        {
            _orderService = new OrderService(new DbContext());
        }

        [HttpGet]
        public DespatchDate Get(List<int> productIds, DateTime orderDate)
        {
            return _orderService.GetDespatchDate(productIds, orderDate);
        }
    }
}

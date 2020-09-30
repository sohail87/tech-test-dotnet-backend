using System;
using System.Collections.Generic;
using Moonpig.PostOffice.Api.Model;

namespace Moonpig.PostOffice.Core
{
    public class OrderService
    {
        private readonly IProductRepository _productRepository;

        public OrderService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public DespatchDate GetDespatchDate(List<int> productIds, DateTime orderDate)
        {
            var maxDespatchDate = orderDate;

            foreach (var productId in productIds)
            {
                var leadTimeInDays = _productRepository.GetProductLeadTime(productId);
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
}

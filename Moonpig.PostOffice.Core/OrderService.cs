using System;
using System.Collections.Generic;
using System.Linq;
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
            var maxLeadTimeDays = productIds
                .Select(productId => _productRepository.GetProductLeadTime(productId))
                .Max();

            return new DespatchDate { Date = orderDate.AddWorkingDays(maxLeadTimeDays)};
        }
    }
}

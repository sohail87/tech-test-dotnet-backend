﻿using System;
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

        public DespatchDate GetDespatchDate(DespatchDateRequest request)
        {
            var maxLeadTimeDays = request.ProductIds
                .Select(productId => _productRepository.GetProductLeadTime(productId))
                .Max();

            return new DespatchDate { Date = request.OrderDate.AddWorkingDays(maxLeadTimeDays)};
        }
    }

    public class DespatchDateRequest
    {
        public List<int> ProductIds { get; set; }
        public DateTime OrderDate { get; set; }

        public DespatchDateRequest(List<int> productIds, DateTime orderDate)
        {
            ProductIds = productIds;
            OrderDate = orderDate;
        }

        public DespatchDateRequest() { }
    }
}

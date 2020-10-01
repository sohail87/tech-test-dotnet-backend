using System;
using System.Collections.Generic;

namespace Moonpig.PostOffice.Core
{
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

        public static DespatchDateRequest SingleProduct(int productId, DateTime orderDate)
        {
            return new DespatchDateRequest(new List<int>{ productId }, orderDate);
        }
    }
}
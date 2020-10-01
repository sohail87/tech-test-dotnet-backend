using System.Linq;
using Moonpig.PostOffice.Core.Extensions;
using Moonpig.PostOffice.Core.Model;

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
}

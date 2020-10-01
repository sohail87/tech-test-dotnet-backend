namespace Moonpig.PostOffice.Core
{
    public interface IProductRepository
    {
        int GetProductLeadTime(int productId);
    }
}
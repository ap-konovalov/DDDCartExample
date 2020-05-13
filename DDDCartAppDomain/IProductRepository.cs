using System.Threading.Tasks;

namespace DDDCartAppDomain
{
	internal interface IProductRepository
	{
		Task<Product> GetProduct(ProductId commandProductId);
	}
}
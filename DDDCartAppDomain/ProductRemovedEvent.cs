using EventFlow.Aggregates;

namespace DDDCartAppDomain
{
	public class ProductRemovedEvent: IAggregateEvent<Cart, CartId>
	{
		public Product Product { get; }

		public ProductRemovedEvent(Product product)
		{
			Product = product;
		}
	}
}
using System.Collections.Generic;
using DDDCartAppDomain;

namespace DDDCArtAppTests.DSL
{
	public class CartBuilder
	{
		List<Product> _products = new List<Product>();
		
		public CartBuilder WithProduct(Product product)
		{
			_products.Add(product);
			return this;
		}
		
		public Cart Please()
		{
			Cart cart = new Cart(CartId.NewCartId());
			
			foreach (var product in _products)
			{
				cart.Apply(new ProductAddedEvent(product));
			}

			return cart;
		}
	}
}
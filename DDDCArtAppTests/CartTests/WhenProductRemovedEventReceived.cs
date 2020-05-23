using DDDCartAppDomain;
using NUnit.Framework;

namespace DDDCArtAppTests
{
	public class WhenProductRemovedEventReceived
	{

		[Test]
		public void AndCartContainsOneProduct_ThenProductShouldBeRemovedFromCart()
		{
			Cart cart = new Cart(CartId.NewCartId());
			var product = new Product(ProductId.NewProductId(), "Chees", 180.00);
			ProductAddedEvent productAddedEvent = new ProductAddedEvent(product);
			cart.Apply(productAddedEvent);
			Assert.True(cart.Products.Count == 1);

			ProductRemovedEvent productRemovedEvent = new ProductRemovedEvent(product);
			cart.Apply(productRemovedEvent);
			
			Assert.True(cart.Products.Count == 0);
		}
	}
}
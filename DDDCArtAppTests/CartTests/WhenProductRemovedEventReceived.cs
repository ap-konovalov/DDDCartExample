using DDDCartAppDomain;
using DDDCArtAppTests.DSL;
using EventFlow.Core.RetryStrategies;
using NUnit.Framework;

namespace DDDCArtAppTests
{
	public class WhenProductRemovedEventReceived
	{

		[Test]
		public void AndCartContainsOneProduct_ThenProductShouldBeRemovedFromCart()
		{
			Product milk = Create
				.Product()
				.Milk();
			Cart cart = Create
				.Cart()
				.WithProduct(milk)
				.Please();
			Assert.True(cart.Products.Count == 1);

			ProductRemovedEvent productRemovedEvent = new ProductRemovedEvent(milk);
			cart.Apply(productRemovedEvent);
			
			Assert.True(cart.Products.Count == 0);
		}

		[Test]
		public void AndCartContainsAnotherProduct_ThenProductShouldNotBeRemoved()
		{
			Product milk = Create.Product().Milk();
			Product meatBalls = Create.Product().MeatBalls();
			Cart cart = Create.Cart().WithProduct(milk).Please();
			
			ProductRemovedEvent productRemovedEvent = new ProductRemovedEvent(meatBalls);
			cart.Apply(productRemovedEvent);

			Assert.AreEqual(1, cart.Products.Count);
			Assert.AreEqual(milk, cart.Products[0]);
		}
	}
}
using System;
using DDDCartAppDomain;
using NUnit.Framework;

namespace DDDCArtAppTests
{
	public class Tests
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void WhenAddProductToCartEventReceived_ThenProductShouldBeAdded()
		{
			CartId id = new CartId($"cart-{Guid.NewGuid()}");
			Cart cart = new Cart(id);
			AddProductEvent addProductEvent = new AddProductEvent(); 
			
			cart.Apply(addProductEvent);
			
			Assert.NotNull(cart.Products);
			Assert.True(cart.Products.Count > 0);
		}
	}
}
using System;
using System.Linq;
using DDDCartAppDomain;
using NUnit.Framework;

namespace DDDCArtAppTests
{
	public class Tests
	{
		private Cart _cart;
		private CartId _cartId;

		[SetUp]
		public void Setup()
		{
			_cartId = new CartId($"cart-{Guid.NewGuid()}");
			_cart = new Cart(_cartId);
		}

		[Test]
		public void WhenAddProductToCartEventReceived_ThenCartShouldContainOneProduct()
		{
			//ARANGE
			AddProductEvent addProductEvent = new AddProductEvent();

			//ACT
			_cart.Apply(addProductEvent);

			//ASSERT
			Assert.NotNull(_cart.Products);
			Assert.True(_cart.Products.Count == 1);
		}

		// [Test]
		// public void WhenAddProductToCartEventReceived_ThenCartProductIsShoulBeEqualProductIdFromEvent()
		// {
		// 	var firstProduct = cart.Products.First();
		// 	Product product = new Product(productId);
		//
		// 	
		// 	Assert.NotNull(firstProduct.Id);
		// 	Assert.True(firstProduct.Id == productId);
		//
	}
}


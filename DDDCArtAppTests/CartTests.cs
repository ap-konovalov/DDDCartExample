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
		private Product _product;
		private ProductId _productId;

		[SetUp]
		public void Setup()
		{
			_cartId = new CartId($"cart-{Guid.NewGuid()}");
			_cart = new Cart(_cartId);

			_productId = new ProductId(Guid.NewGuid());
			_product = new Product(_productId);
		}

		[Test]
		public void WhenAddProductToCartEventReceived_ThenCartShouldContainOneProduct()
		{
			ProductAddedEvent productAddedEvent = new ProductAddedEvent(_product);

			_cart.Apply(productAddedEvent);

 			Assert.NotNull(_cart.Products);
			Assert.True(_cart.Products.Count == 1);
		}

		[Test]
		public void WhenAddProductToCartEventReceived_ThenCartProductIsShouldBeEqualProductIdFromEvent()
		{
			ProductAddedEvent productAddedEvent = new ProductAddedEvent(_product);
			
			_cart.Apply(productAddedEvent);

			Assert.NotNull(_cart.Products.First().Id);
			Assert.True(_cart.Products.First().Id == _product.Id);
		}
	}
}
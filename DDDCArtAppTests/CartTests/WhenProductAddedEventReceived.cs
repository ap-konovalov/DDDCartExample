using System;
using System.Linq;
using DDDCartAppDomain;
using NUnit.Framework;

namespace DDDCArtAppTests
{
	public class WhenProductAddedEventReceived
	{
		private Cart _cart;
		private CartId _cartId;
		private Product _product;

		[SetUp]
		public void Setup()
		{
			_cartId = new CartId($"cart-{Guid.NewGuid()}");
			_cart = new Cart(_cartId);

			ProductId productId = new ProductId(Guid.NewGuid());
			_product = new Product(productId, "TestProduct", 50);
		}

		[Test]
		public void ThenCartShouldContainOneProduct()
		{
			ProductAddedEvent productAddedEvent = new ProductAddedEvent(_product);

			_cart.Apply(productAddedEvent);

 			Assert.NotNull(_cart.Products);
			Assert.True(_cart.Products.Count == 1);
		}

		[Test]
		public void ThenCartProductShouldBeEqualProductIdFromEvent()
		{
			ProductAddedEvent productAddedEvent = new ProductAddedEvent(_product);
			
			_cart.Apply(productAddedEvent);

			Assert.NotNull(_cart.Products.First().Id);
			Assert.AreEqual(_product , _cart.Products.First());
		}

		[Test]
		public void ThenCartShouldBeContainProductNameFromEvent()
		{
			ProductAddedEvent productAddedEvent = new ProductAddedEvent(_product);
			
			_cart.Apply(productAddedEvent);
			
			Assert.NotNull(_product.Name);
			Assert.AreEqual(_product.Name,_cart.Products.First().Name);
		}

		[Test]
		public void ThenCartProductPriceShouldBeEqualProductPriceFromEvent()
		{
			ProductAddedEvent productAddedEvent = new ProductAddedEvent(_product);
			
			_cart.Apply(productAddedEvent);

			Assert.AreEqual(_product.Price, _cart.Products.First().Price);
		}

		[Test]
		public void Twice_ThenCArtShouldContainTwoProducts()
		{
			ProductAddedEvent firstProductAddedEvent = new ProductAddedEvent(_product);
			ProductAddedEvent secondProductAddedEvent = new ProductAddedEvent(_product);
			
			_cart.Apply(firstProductAddedEvent);
			_cart.Apply(secondProductAddedEvent);
			
			Assert.AreEqual(2, _cart.Products.Count);
		}
	}
}
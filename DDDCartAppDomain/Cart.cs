﻿using System.Collections.Generic;
using System.Linq;
using EventFlow.Aggregates;

namespace DDDCartAppDomain
{
	public class Cart : AggregateRoot<Cart,CartId>,IEmit<ProductAddedEvent>, IEmit<ProductRemovedEvent>
	{
		public List<Product> Products => _products;
		private readonly List<Product> _products;

		public Cart(CartId id) : base(id)
		{
			_products = new List<Product>();
		}

		public void Apply(ProductAddedEvent productAddedEvent)
		{
			_products.Add(productAddedEvent.Product);
		}
		
		public void Apply(ProductRemovedEvent productRemovedEvent)
		{
			var productFromCart = _products.FirstOrDefault(product => product.Id == productRemovedEvent.Product.Id);
			_products.Remove(productFromCart);
		}

		public void AddProduct(Product product)
		{
			Emit(new ProductAddedEvent(product));
		}		
		
		public void RemoveProduct(Product product)
		{
			Emit(new ProductRemovedEvent(product));
		}
	}
}
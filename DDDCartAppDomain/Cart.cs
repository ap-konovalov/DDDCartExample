using System;
using System.Collections.Generic;
using EventFlow.Aggregates;

namespace DDDCartAppDomain
{
	public class Cart : AggregateRoot<Cart,CartId>
	{
		public List<Product> Products => _products;
		private readonly List<Product> _products;

		public Cart(CartId id) : base(id)
		{
			_products = new List<Product>();
		}

		public void Apply(AddProductEvent addProductEvent)
		{
			_products.Add(new Product());
		}
	}
}
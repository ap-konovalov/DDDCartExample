﻿using System;

namespace DDDCartAppDomain
{
	public class Product
	{
		public ProductId Id { get; private set; }

		public Product(ProductId id)
		{
			Id = id;
		}
	}
}
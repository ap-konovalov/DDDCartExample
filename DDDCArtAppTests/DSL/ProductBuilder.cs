using DDDCartAppDomain;

namespace DDDCArtAppTests.DSL
{
	public class ProductBuilder
	{
		public Product Milk()
		{
			return new Product(ProductId.NewProductId(), "milk", 80.00);
		}

		public Product MeatBalls()
		{
			return new Product(ProductId.NewProductId(), "Meat Balls", 325.00);
		}
	}
}
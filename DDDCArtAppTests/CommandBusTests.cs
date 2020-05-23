﻿using System;
using System.Threading;
using System.Threading.Tasks;
using DDDCartAppDomain;
using EventFlow;
using EventFlow.Aggregates;
using EventFlow.DependencyInjection.Extensions;
using EventFlow.Extensions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Data.Common;
using EventFlow.EntityFramework.Extensions;
using EventFlow.EntityFramework;

namespace DDDCArtAppTests
{
	public class CommandBusTests
	{
		[Test]
		public async Task AddProductToCart()
		{
			var services = new ServiceCollection();
			services.AddTransient<IProductRepository, FakeProductRepository>();
			using var resolver = EventFlowOptions.New
				.UseServiceCollection(services)
				.AddDefaults(typeof(CartContext).Assembly)
				.UseEntityFrameworkEventStore<CartContext>()
				.ConfigureEntityFramework(EntityFrameworkConfiguration.New) 
				.AddDbContextProvider<CartContext, MySqlCartContextProvider>()
				.AddEvents(typeof(ProductAddedEvent))
				.AddCommands(typeof(AddProductCommand))
				.AddCommandHandlers(typeof(AddProductCommandHandler))
				.CreateResolver();
			
			var commandBus = resolver.Resolve<ICommandBus>();
			var aggregateStore = resolver.Resolve<IAggregateStore>();
			CartId cartId = CartId.NewCartId();
			await commandBus.PublishAsync(
				new AddProductCommand(cartId, new ProductId(Guid.Empty)), 
				CancellationToken.None);
			Cart cart = await aggregateStore.LoadAsync<Cart, CartId>(cartId, CancellationToken.None);
			Assert.AreEqual(1, cart.Products.Count);
		}

		[Test]
		public void DeleteProductFromCart()
		{
			var services = new ServiceCollection();
			services.AddTransient<IProductRepository, FakeProductRepository>();
			
			using var resolver = EventFlowOptions.New
				.UseServiceCollection(services)
				.AddDefaults(typeof(CartContext).Assembly)
				.UseEntityFrameworkEventStore<CartContext>()
				.ConfigureEntityFramework(EntityFrameworkConfiguration.New) 
				.AddDbContextProvider<CartContext, MySqlCartContextProvider>()
				.AddEvents(typeof(ProductAddedEvent),typeof(ProductRemovedEvent))
				.AddCommands(typeof(AddProductCommand), typeof(RemoveProductCommand))
				.AddCommandHandlers(typeof(AddProductCommandHandler), typeof(RemoveProductCommandHandler))
				.CreateResolver();
		}
		
		private static Uri GetUriFromConnectionString(string connectionString)
		{
			DbConnectionStringBuilder builder = new DbConnectionStringBuilder { ConnectionString = connectionString };
			string connectTo = (string)builder["ConnectTo"];
			return connectTo == null ? null : new Uri(connectTo);
		}
	}
}
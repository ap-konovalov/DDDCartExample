using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DDDCartApp.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CartController : ControllerBase
	{
		private readonly ILogger<CartController> _logger;

		public CartController(ILogger<CartController> logger)
		{
			_logger = logger;
		}
	}
}
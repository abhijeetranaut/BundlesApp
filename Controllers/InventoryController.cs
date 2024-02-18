using BundlesApp.Models;
using BundlesApp.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BundlesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly InventoryService _inventoryService;

        public InventoryController(InventoryService inventoryService)
        {
            _inventoryService = inventoryService ?? throw new ArgumentNullException(nameof(inventoryService));
        }

        [HttpPost("CalculateMaxBundles")]
        public ActionResult<int> CalculateMaxBundles([FromBody] InventoryCalculationRequest request)
        {
            var maxBundles = _inventoryService.CalculateMaxBundles(request);
            return Ok(maxBundles);
        }
    }
}

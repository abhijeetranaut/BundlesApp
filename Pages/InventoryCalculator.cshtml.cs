using BundlesApp.Models;
using BundlesApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace BundlesApp.Pages
{
    public class InventoryCalculatorModel : PageModel
    {
        private readonly InventoryService _inventoryService;

        public InventoryCalculatorModel(InventoryService inventoryService)
        {
            _inventoryService = inventoryService ?? throw new ArgumentNullException(nameof(inventoryService));
        }

        [BindProperty]
        public string BundleName { get; set; }

        [BindProperty]
        public string PartsInventoryJson { get; set; }

        [BindProperty]
        public string BundleInventoryJson { get; set; }

        public int? MaxBundles { get; private set; }

        public void OnPost()
        {
            try
            {
                var partsInventory = JsonSerializer.Deserialize<Dictionary<string, int>>(PartsInventoryJson);
                var bundleInventory = JsonSerializer.Deserialize<Dictionary<string, Bundle>>(BundleInventoryJson);

                var request = new InventoryCalculationRequest
                {
                    BundleName = BundleName,
                    PartsInventory = partsInventory,
                    BundleInventory = bundleInventory
                };

                MaxBundles = _inventoryService.CalculateMaxBundles(request);
            }
            catch (Exception ex)
            {
                // Handle any exceptions (e.g., invalid JSON, calculation errors)
                ModelState.AddModelError(string.Empty, $"Error: {ex.Message}");
            }
        }
    }
}

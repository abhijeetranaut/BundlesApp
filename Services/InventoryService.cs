using BundlesApp.Models;
using System;
using System.Collections.Generic;

namespace BundlesApp.Services
{
    public class InventoryService
    {
        private readonly Dictionary<string, int> _partsInventory;
        private readonly Dictionary<string, Bundle> _bundleInventory;


        public int CalculateMaxBundles(InventoryCalculationRequest request)
        {
            string bundleName = request.BundleName;
            Dictionary<string, int> partsInventory = request.PartsInventory;
            Dictionary<string, Bundle> bundleInventory = request.BundleInventory;

            HashSet<string> visitedBundles = new HashSet<string>();

            return CalculateMaxBundlesRecursive(bundleName, partsInventory, bundleInventory, visitedBundles);
        }

        private int CalculateMaxBundlesRecursive(string bundleName, Dictionary<string, int> partsInventory, Dictionary<string, Bundle> bundleInventory, HashSet<string> visitedBundles)
        {
            if (visitedBundles.Contains(bundleName))
            {
                throw new InvalidOperationException("Circular dependency detected in bundle structure.");
            }

            visitedBundles.Add(bundleName);
            if (bundleInventory == null)
            {
                throw new InvalidOperationException("Bundle inventory is not initialized.");
            }
            if (!bundleInventory.ContainsKey(bundleName))
            {
                throw new ArgumentException($"Bundle '{bundleName}' does not exist in the inventory.");
            }

            var bundle = bundleInventory[bundleName];
            int maxBundles = int.MaxValue;

            foreach (var part in bundle.Parts)
            {
                if (part.Name != null && partsInventory.ContainsKey(part.Name))
                {
                    maxBundles = Math.Min(maxBundles, partsInventory[part.Name] / part.Quantity);
                }
                else
                {
                    throw new ArgumentException($"Part '{part.Name}' does not exist in the inventory.");
                }
            }

            foreach (var subBundlePart in bundle.Bundles)
            {
                maxBundles = Math.Min(maxBundles, CalculateMaxBundlesRecursive(subBundlePart.Bundle.Name, partsInventory, new Dictionary<string, Bundle> { { subBundlePart.Bundle.Name, subBundlePart.Bundle } }, visitedBundles) / subBundlePart.Quantity);
            }

            visitedBundles.Remove(bundleName);


            return maxBundles;
        }
    }
}

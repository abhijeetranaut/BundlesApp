using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BundlesApp.Models
{
    public class InventoryCalculationRequest
    {
        public string BundleName { get; set; }
        public Dictionary<string, int> PartsInventory { get; set; }
        public Dictionary<string, Bundle> BundleInventory { get; set; }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BundlesApp.Models
{
    public class BundlePart
    {
        public Bundle Bundle { get; set; }
        public int Quantity { get; set; }
    }
}

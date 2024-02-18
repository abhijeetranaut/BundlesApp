using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BundlesApp.Models
{
    public class Bundle
    {
        public string Name { get; set; }
        public List<Part> Parts { get; set; }
        public List<BundlePart> Bundles { get; set; }

        public Bundle()
        {
            Parts = new List<Part>();
            Bundles = new List<BundlePart>();
        }
    }
}

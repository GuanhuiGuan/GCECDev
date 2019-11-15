using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCECDev.MasterDetailViews
{
    public class RootMenuItem
    {
        public RootMenuItem()
        {
            TargetType = typeof(RootDetail);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}

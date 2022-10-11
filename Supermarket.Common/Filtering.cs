using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Common
{
    public class Filtering
    {
        public string Query { get; set; }
        public DateTime BornBefore { get; set; }
        public DateTime BornAfter { get; set; }
        public bool HasAddress { get; set; }
        public Filtering(DateTime bornBefore, DateTime bornAfter, string query, bool hasAddress)
        {
            BornBefore = bornBefore;
            BornAfter = bornAfter;
            Query = query;
            HasAddress = hasAddress;
        }
    }
}

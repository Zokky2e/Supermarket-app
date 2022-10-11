using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Common
{
    public class Paging
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public Paging(int pageSize=4, int pageNumber=1)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
        }
    }
}

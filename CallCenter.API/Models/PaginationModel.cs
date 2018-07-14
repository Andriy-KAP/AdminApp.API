using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CallCenter.API.Models
{
    public class PaginationModel
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int DataCount { get; set; }
        public string Search { get; set; }
    }
}
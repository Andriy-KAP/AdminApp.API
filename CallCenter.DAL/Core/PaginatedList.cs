using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenter.DAL.Core
{
    public class PaginatedList<T> //: List<T>
    {
        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public int TotalPageCount { get; private set; }
        public List<T> Items { get; private set; }

        public PaginatedList()
        {

        }

        public PaginatedList(int pageIndex, int pageSize, int totalCount, List<T> source)
        {
            //AddRange(source);
            Items = source;
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = totalCount;
            TotalPageCount = (int)Math.Ceiling(totalCount / (double)pageSize);
        }

        public bool HasPreviousPage
        {
            get { return PageIndex > 1; }
        }

        public bool HasNextPage
        {
            get { return PageIndex < TotalPageCount; }
        }
        public async Task<List<T>> ToListAsync()
        {
            return  await this.ToListAsync();
        }
    }
}

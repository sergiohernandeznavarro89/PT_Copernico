using PagedList;
using System;

namespace Service.Helpers
{
    public class PaginationInfo
    {
        public int? TotalItems { get; set; }
        public int? PageSize { get; set; }
        public int? CurrentPage { get; set; }
        public int? TotalPages { get; set; }
        public bool? HasPreviousPage { get; set; }
        public bool? HasNextPage { get; set; }
        public int? NextPageNumber { get; set; }
        public int? PreviousPageNumber { get; set; }

        public static PaginationInfo formatInfo<T>(IPagedList<T> datos)
        {
            return new PaginationInfo
            {
                TotalItems = datos.TotalItemCount,
                CurrentPage = datos.PageNumber,
                HasNextPage = datos.HasNextPage,
                HasPreviousPage = datos.HasPreviousPage,
                PageSize = datos.PageSize,
                TotalPages = datos.PageCount,
                NextPageNumber = datos.HasNextPage ? datos.PageNumber + 1 : null,
                PreviousPageNumber = datos.HasPreviousPage ? datos.PageNumber - 1 : null,
            };
        }
    }
}

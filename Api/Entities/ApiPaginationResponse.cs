using Service.Helpers;

namespace Api.Entities
{
    public class ApiPaginationResponse<T>
    {
        public List<T> Items { get; set; }
        public PaginationInfo PaginationInfo { get; set; }

        public static ApiPaginationResponse<T> formatResponse(PagedList.IPagedList<T> data)
        {
            return new ApiPaginationResponse<T>
            {
                Items = data.ToList(),
                PaginationInfo = PaginationInfo.formatInfo(data)
            };
        }        
    }
}

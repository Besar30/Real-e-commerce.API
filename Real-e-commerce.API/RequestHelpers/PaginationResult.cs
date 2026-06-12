namespace Real_e_commerce.API.RequestHelpers
{
    public class PaginationResult<T>
    {
        public IReadOnlyList<T> Items { get; set; } = [];

        public int pageIndex { get; set;}
        public int pageSize { get; set;}
        public int Count { get; set;}
        public int PageCount => (int)Math.Ceiling((double)Count / pageSize);
        public bool hasNext => pageIndex < PageCount;
        public bool hasPrevious => pageIndex > 0;
        public static PaginationResult<T> CreatePagination(IReadOnlyList<T> Data,int totalCount, int pageIndex, int pageSize)
        {
            return new PaginationResult<T>
            {
                Items = Data,
                pageIndex = pageIndex,
                pageSize = pageSize,
                Count = totalCount,
            };
        }
    }
}

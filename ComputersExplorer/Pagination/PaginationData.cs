namespace ComputersExplorer.Pagination
{
    public class PaginationData
    {
        private int maxPageSize = 100;
        private int pageSize;

        public int PageNumber { get; set; }

        public int PageSize
        {
            get => pageSize;
            set
            {
                pageSize = (value > maxPageSize) ? maxPageSize : value;         
            }
        }

    }
}

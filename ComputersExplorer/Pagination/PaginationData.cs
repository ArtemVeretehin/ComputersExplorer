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
                if (value > maxPageSize) { pageSize = maxPageSize; }
                else if (value <= 0) { pageSize = 1; }
                else { pageSize = value; }         
            }
        }

    }
}

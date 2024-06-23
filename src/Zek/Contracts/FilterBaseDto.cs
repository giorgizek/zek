namespace Zek.Contracts
{
    /// <summary>
    /// Filter Base DTO
    /// </summary>
    public class FilterBaseDto
    {
        /// <summary>
        /// Constructor FilterDTO
        /// </summary>
        public FilterBaseDto()
        {
            Page = 1;
            PageSize = 10;
        }

        private int _page;
        /// <summary>
        /// Page number
        /// </summary>
        public int Page
        {
            get => _page;
            set
            {
                if (value < 1)
                    value = 1;
                _page = value;
            }
        }

        private int _pageSize;
        /// <summary>
        /// Page size
        /// </summary>
        public int PageSize
        {
            get => _pageSize;
            set
            {
                if (value < 1)
                    value = 1;
                else if (value > MaxPageSize)
                    value = MaxPageSize;

                _pageSize = value;
            }
        }

        /// <summary>
        /// Sort column name
        /// </summary>
        public string Sort { get; set; }

        /// <summary>
        /// Asc = null or true, Desc = false
        /// </summary>
        public bool? Asc { get; set; }

        /// <summary>
        /// Max page size
        /// </summary>
        protected virtual int MaxPageSize => 10000;

        /// <summary>
        /// Quick Search Field
        /// </summary>
        public string QuickSearch { get; set; }
    }
}

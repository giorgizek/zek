namespace Zek.Model.DTO
{
    public class FilterDTO
    {
        public FilterDTO()
        {
            Page = 1;
            PageSize = 10;
        }


        private int _page;

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


        public string Sort { get; set; }
        public bool? Asc { get; set; }
            
        //public int PageCount => PagedList?.PageCount ?? 0;

        //public int FirstItemOnPage => PagedList?.FirstItemOnPage ?? 0;

        //public int LastItemOnPage => PagedList?.LastItemOnPage ?? 0;

        //public int ItemsCount => PagedList?.TotalItemCount ?? 0;

        protected virtual int MaxPageSize => 10000;
    }
}
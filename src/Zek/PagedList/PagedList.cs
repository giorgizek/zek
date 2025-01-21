namespace Zek.PagedList
{
    public class PagedList<T> : IPagedList<T>
    {
        /// <summary>
        /// Initializes a new instance of a type <see cref = "PagedList{T}" /> and sets properties needed to calculate position and size data on the subset and superset.
        /// </summary>
        /// <param name="subset">The single subset this collection should represent.</param>
        /// <param name = "pageNumber">The one-based index of the subset of objects contained by this instance.</param>
        /// <param name = "pageSize">The maximum size of any individual subset.</param>
        /// <param name = "totalItemCount">The size of the superset.</param>
        protected internal PagedList(IEnumerable<T> subset, int pageNumber, int pageSize, int totalItemCount)
        {
            if (pageNumber < 1)
            {
                throw new ArgumentOutOfRangeException($"pageNumber = {pageNumber}. PageNumber cannot be below 1.");
            }

            if (pageSize < 1)
            {
                throw new ArgumentOutOfRangeException($"pageSize = {pageSize}. PageSize cannot be less than 1.");
            }

            // set source to blank list if superset is null to prevent exceptions
            TotalItemCount = totalItemCount;

            PageNumber = pageNumber;
            PageSize = pageSize;
            PageCount = TotalItemCount > 0
                ? (int)Math.Ceiling(TotalItemCount / (double)PageSize)
                : 0;
            HasPreviousPage = PageNumber > 1;
            HasNextPage = PageNumber < PageCount;
            IsFirstPage = PageNumber == 1;
            IsLastPage = PageNumber == PageCount;
            FirstItemOnPage = (PageNumber - 1) * PageSize + 1;

            var numberOfLastItemOnPage = FirstItemOnPage + PageSize - 1;

            LastItemOnPage = numberOfLastItemOnPage > TotalItemCount
                ? TotalItemCount
                : numberOfLastItemOnPage;

            _items.AddRange(subset);
            Count = _items.Count;
        }

        /// <summary>
        /// 	Total number of objects contained within the superset.
        /// </summary>
        /// <value>
        /// 	Total number of objects contained within the superset.
        /// </value>
        public int TotalItemCount { get; protected set; }

        /// <summary>
        /// 	One-based index of this subset within the superset.
        /// </summary>
        /// <value>
        /// 	One-based index of this subset within the superset.
        /// </value>
        public int PageNumber { get; protected set; }

        /// <summary>
        /// 	Maximum size any individual subset.
        /// </summary>
        /// <value>
        /// 	Maximum size any individual subset.
        /// </value>
        public int PageSize { get; protected set; }


        /// <summary>
        /// 	Total number of subsets within the superset.
        /// </summary>
        /// <value>
        /// 	Total number of subsets within the superset.
        /// </value>
        public int PageCount { get; protected set; }

        /// <summary>
        /// 	Returns true if this is NOT the first subset within the superset.
        /// </summary>
        /// <value>
        /// 	Returns true if this is NOT the first subset within the superset.
        /// </value>
        public bool HasPreviousPage { get; protected set; }

        /// <summary>
        /// 	Returns true if this is NOT the last subset within the superset.
        /// </summary>
        /// <value>
        /// 	Returns true if this is NOT the last subset within the superset.
        /// </value>
        public bool HasNextPage { get; protected set; }

        /// <summary>
        /// 	Returns true if this is the first subset within the superset.
        /// </summary>
        /// <value>
        /// 	Returns true if this is the first subset within the superset.
        /// </value>
        public bool IsFirstPage { get; protected set; }

        /// <summary>
        /// 	Returns true if this is the last subset within the superset.
        /// </summary>
        /// <value>
        /// 	Returns true if this is the last subset within the superset.
        /// </value>
        public bool IsLastPage { get; protected set; }

        /// <summary>
        /// 	One-based index of the first item in the paged subset.
        /// </summary>
        /// <value>
        /// 	One-based index of the first item in the paged subset.
        /// </value>
        public int FirstItemOnPage { get; protected set; }

        /// <summary>
        /// 	One-based index of the last item in the paged subset.
        /// </summary>
        /// <value>
        /// 	One-based index of the last item in the paged subset.
        /// </value>
        public int LastItemOnPage { get; protected set; }


        /// <summary>
		/// 	Gets the number of elements contained on this page.
		/// </summary>
		public int Count { get; protected set; }


        private List<T> _items = [];
        public IEnumerable<T> Items { get => _items; }
    }
}
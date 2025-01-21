namespace Zek.PagedList
{
    [Obsolete("Please use PagerList")]
    public class PagerListDTO<T>
    {
        public PagerListDTO(Pager pager, List<T> data)
        {
            Pager = pager;
            Data = data;
        }

        public Pager Pager { get; }

        public List<T> Data { get; }
    }

    [Obsolete("Please use PagerList")]
    public class Pager
    {
        public int TotalItemCount { get; set; }
    }

    /*public class PagedListModel<T>
    {
        public PagedListModel(IPagedList<T> data)
        {
            Data = data;
        }

        public IPagedList<T> Data { get; }

        public PagedListMetaData Pager => Data.GetMetaData();
        //public int Count => Data.Count;
        //public int PageCount => Data.PageCount;
        //public int TotalItemCount => Data.TotalItemCount;
        //public int PageNumber => Data.PageNumber;
        //public int PageSize => Data.PageSize;
        //public bool HasPreviousPage => Data.HasPreviousPage;
        //public bool HasNextPage => Data.HasNextPage;
        //public bool IsFirstPage => Data.IsFirstPage;
        //public bool IsLastPage => Data.IsLastPage;
        //public int FirstItemOnPage => Data.FirstItemOnPage;
        //public int LastItemOnPage => Data.LastItemOnPage;
    }*/
}

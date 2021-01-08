using System;
using System.Collections.Generic;

namespace Zek.Model.ViewModel
{
    public interface IPagedIndexViewModel
    {
        Pager Pager { get; set; }
    }

    public class PagedIndexViewModel<T> : IPagedIndexViewModel
    {
        public PagedIndexViewModel()
        {
            Default = default;
        }
        public PagedIndexViewModel(int totalItems, int? page, int pageSize = 10) : this()
        {
            Pager = new Pager(totalItems, page, pageSize);
        }


        public readonly T Default;
        public IEnumerable<T> Items { get; set; }
        public Pager Pager { get; set; }
    }

    public class Pager
    {
        public Pager(int totalItems, int? page = null, int pageSize = 10)
        {
            // calculate total, start and end pages
            var totalPages = (int)Math.Ceiling(totalItems / (decimal)pageSize);
            var currentPage = page ?? 1;
            var startPage = currentPage - 5;
            var endPage = currentPage + 4;
            if (startPage <= 0)
            {
                endPage -= (startPage - 1);
                startPage = 1;
            }
            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }

            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;
        }

        public int TotalItems { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }
    }
}

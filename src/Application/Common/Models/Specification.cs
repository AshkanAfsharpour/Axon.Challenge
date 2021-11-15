using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace Axon.Application.Common.Models
{
     
    public class Specification<T>
        where T : class
    {
        public List<Expression<Func<T, bool>>> Conditions { get; set; } = new List<Expression<Func<T, bool>>>();

        public Func<IQueryable<T>, IIncludableQueryable<T, object>> Includes { get; set; }

        public List<string> IncludeStrings { get; } = new List<string>();

        public Func<IQueryable<T>, IOrderedQueryable<T>> OrderBy { get; set; }

        public (string ColumnName, string SortDirection) OrderByDynamic { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public Specification()
        {
        }
        public Specification(List<Expression<Func<T, bool>>> conditions)
        {
            this.Conditions = conditions;
        }
        public Specification(List<Expression<Func<T, bool>>> conditions, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes)
        {
            this.Conditions = conditions;
            this.Includes = includes;
        }

        public Specification( Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, int pageIndex, int pageSize)
        {
            this.OrderBy = orderBy;
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
        }

        public Specification(List<Expression<Func<T, bool>>> conditions,  Func<IQueryable<T>, IOrderedQueryable<T>> orderBy , int pageIndex, int pageSize)
        {
            this.Conditions = conditions;
            this.OrderBy = orderBy;
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
        }

        public Specification(List<Expression<Func<T, bool>>> conditions, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, int pageIndex, int pageSize)
        {
            this.Conditions = conditions;
            this.Includes = includes;
            this.OrderBy = orderBy;
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
        }

    }
}
﻿using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using Axon.Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Axon.Presistence
{
    internal static class SpecificationEvaluator
    {
        public static IQueryable<T> GetSpecifiedQuery<T>(this IQueryable<T> inputQuery, Specification<T> specification)
            where T : class
        {
            if (inputQuery == null)
            {
                throw new ArgumentNullException(nameof(inputQuery));
            }

            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            IQueryable<T> query = inputQuery;

            // modify the IQueryable using the specification's criteria expression
            if (specification.Conditions != null && specification.Conditions.Any())
            {
                foreach (Expression<Func<T, bool>> specificationCondition in specification.Conditions)
                {
                    query = query.Where(specificationCondition);
                }
            }

            // Includes all expression-based includes
            if (specification.Includes != null)
            {
                query = specification.Includes(query);
            }

            // Include any string-based include statements
            if (specification.IncludeStrings != null && specification.IncludeStrings.Any())
            {
                query = specification.IncludeStrings.Aggregate(query, (current, include) => current.Include(include));
            }

            // Apply ordering if expressions are set
            if (specification.OrderBy != null)
            {
                query = specification.OrderBy(query);
            }
            else if (!(string.IsNullOrWhiteSpace(specification.OrderByDynamic.ColumnName) || string.IsNullOrWhiteSpace(specification.OrderByDynamic.ColumnName)))
            {
                query = query.OrderBy(specification.OrderByDynamic.ColumnName + " " + specification.OrderByDynamic.SortDirection);
            }

            // Apply paging if enabled
            if (specification.PageIndex != 0 && specification.PageSize != 0)
            {
                query = query.Skip((specification.PageIndex - 1) * specification.PageSize).Take(specification.PageSize);
            }
           

            return query;
        }
    }
}
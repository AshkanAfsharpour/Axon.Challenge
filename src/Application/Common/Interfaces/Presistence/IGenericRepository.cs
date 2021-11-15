using Axon.Application.Common.Models;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Axon.Application.Common.Interfaces
{
    public interface IGenericRepository
    {
        IQueryable<TEntity> GetQueryable<TEntity>()
            where TEntity : class;
        // Get Single

        
        Task<TEntity> GetAsync<TEntity>(Expression<Func<TEntity, bool>> condition)
            where TEntity : class;

        Task<TEntity> GetAsync<TEntity>(Expression<Func<TEntity, bool>> condition, bool asNoTracking)
            where TEntity : class;
         
        Task<TEntity> GetAsync<TEntity>(
            Expression<Func<TEntity, bool>> condition,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes)
            where TEntity : class;

        Task<TEntity> GetAsync<TEntity>(
            Expression<Func<TEntity, bool>> condition,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes,
            bool asNoTracking)
            where TEntity : class;

         
        Task<TEntity> GetAsync<TEntity>(Specification<TEntity> specification)
            where TEntity : class;
        
        Task<TEntity> GetAsync<TEntity>(Specification<TEntity> specification, bool asNoTracking)
            where TEntity : class;


        Task<TProjectedType> GetProjectedAsync<TEntity, TProjectedType>(
           Expression<Func<TEntity, bool>> condition)
           where TEntity : class;

        Task<TProjectedType> GetProjectedAsync<TEntity, TProjectedType>(
            Expression<Func<TEntity, bool>> condition,
            Expression<Func<TEntity, TProjectedType>> selectExpression)
            where TEntity : class;


        Task<TProjectedType> GetProjectedAsync<TEntity, TProjectedType>(
            Specification<TEntity> specification)
            where TEntity : class;

        Task<TProjectedType> GetProjectedAsync<TEntity, TProjectedType>(
            Specification<TEntity> specification,
            Expression<Func<TEntity, TProjectedType>> selectExpression)
            where TEntity : class;

        // Get By Id

 
        Task<TEntity> GetByIdAsync<TEntity>(object id)
            where TEntity : class;

       
        Task<TEntity> GetByIdAsync<TEntity>(object id, bool asNoTracking)
            where TEntity : class;

         
        Task<TEntity> GetByIdAsync<TEntity>(object id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes)
            where TEntity : class;

        
        Task<TEntity> GetByIdAsync<TEntity>(object id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes, bool asNoTracking)
            where TEntity : class;

        Task<TProjectedType> GetProjectedByIdAsync<TEntity, TProjectedType>(
           object id)
           where TEntity : class;

        Task<TProjectedType> GetProjectedByIdAsync<TEntity, TProjectedType>(
            object id,
            Expression<Func<TEntity, TProjectedType>> selectExpression)
            where TEntity : class;
        // Get List
         
        Task<List<TEntity>> GetListAsync<TEntity>()
            where TEntity : class;

        
        Task<List<TEntity>> GetListAsync<TEntity>(bool asNoTracking)
            where TEntity : class;

        
        Task<List<TEntity>> GetListAsync<TEntity>(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes)
            where TEntity : class;

      
        Task<List<TEntity>> GetListAsync<TEntity>(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes, bool asNoTracking)
            where TEntity : class;
   
        Task<List<TEntity>> GetListAsync<TEntity>(Expression<Func<TEntity, bool>> condition)
            where TEntity : class;

       
        Task<List<TEntity>> GetListAsync<TEntity>(Expression<Func<TEntity, bool>> condition, bool asNoTracking)
            where TEntity : class;
 
        Task<List<TEntity>> GetListAsync<TEntity>(
            Expression<Func<TEntity, bool>> condition,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes,
            bool asNoTracking = false)
            where TEntity : class;

     
        Task<List<TEntity>> GetListAsync<TEntity>(Specification<TEntity> specification)
            where TEntity : class;
        
        Task<List<TEntity>> GetListAsync<TEntity>(Specification<TEntity> specification, bool asNoTracking)
            where TEntity : class;

        Task<PaginatedList<TEntity>> GetPaginatedListAsync<TEntity>(Specification<TEntity> specification)
            where TEntity : class;

        Task<PaginatedList<TEntity>> GetPaginatedListAsync<TEntity>(Specification<TEntity> specification, bool asNoTracking)
            where TEntity : class;


        Task<List<TProjectedType>> GetProjectedListAsync<TEntity, TProjectedType>()
           where TEntity : class;

        Task<List<TProjectedType>> GetProjectedListAsync<TEntity, TProjectedType>(
           Expression<Func<TEntity, TProjectedType>> selectExpression)
           where TEntity : class;

        Task<List<TProjectedType>> GetProjectedListAsync<TEntity, TProjectedType>(
          Expression<Func<TEntity, bool>> condition)
          where TEntity : class;

        Task<List<TProjectedType>> GetProjectedListAsync<TEntity, TProjectedType>(
            Expression<Func<TEntity, bool>> condition,
            Expression<Func<TEntity, TProjectedType>> selectExpression)
            where TEntity : class;

        
        
        Task<List<TProjectedType>> GetProjectedListAsync<TEntity, TProjectedType>(
            Specification<TEntity> specification)
            where TEntity : class;
        Task<List<TProjectedType>> GetProjectedListAsync<TEntity, TProjectedType>(
           Specification<TEntity> specification,
           Expression<Func<TEntity, TProjectedType>> selectExpression)
           where TEntity : class;

        Task<PaginatedList<TProjectedType>> GetPaginatedProjectedListAsync<TEntity, TProjectedType>(
          Specification<TEntity> specification)
          where TEntity : class;
        Task<PaginatedList<TProjectedType>> GetPaginatedProjectedListAsync<TEntity, TProjectedType>(
           Specification<TEntity> specification,
           Expression<Func<TEntity, TProjectedType>> selectExpression)
           where TEntity : class;


        // Exists
        Task<bool> ExistsAsync<TEntity>()
            where TEntity : class;
 
        Task<bool> ExistsAsync<TEntity>(Expression<Func<TEntity, bool>> condition)
            where TEntity : class;


        // Insert
        Task<object[]> InsertAsync<TEntity>(TEntity entity)
            where TEntity : class;
        Task InsertAsync<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class;

        // Update
        void Update<TEntity>(TEntity entity)
            where TEntity : class;
        void Update<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class;

        // Delete
        void Delete<TEntity>(TEntity entity)
            where TEntity : class;

        void Delete<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class;

        // Count
        Task<int> GetCountAsync<TEntity>()
            where TEntity : class;
         
        Task<int> GetCountAsync<TEntity>(params Expression<Func<TEntity, bool>>[] conditions)
            where TEntity : class;
         
        Task<long> GetLongCountAsync<TEntity>()
            where TEntity : class;

         
        Task<long> GetLongCountAsync<TEntity>(params Expression<Func<TEntity, bool>>[] conditions)
            where TEntity : class;

        // Raw Sql

        int ExecuteSqlCommand(string sql, params object[] parameters);

        Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters);

        // Context Actions
        void ResetContextState();

        Task SaveChangesAsync(CancellationToken cancellationToken = default);

        Task RollBackAsync(CancellationToken cancellationToken = default);

    }
}

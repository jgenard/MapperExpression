﻿
using MapperExemple.Entity.EF;
using MapperExemple.Entity.Interface;
using MapperExpression.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace MapperExemple.Entity
{
    public class ExempleProduct : IExempleProduct,IDisposable   

    {

        private ExempleDbContext _context;

        public ExempleProduct()
        {
            _context = new ExempleDbContext();
            _context.Database.Log = x => Debug.WriteLine(x);
        }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int? SupplierId { get; set; }
        public int? CategoryId { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        public short? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }

        public IExempleProduct GetFirstProduct()
        {
            IExempleProduct result = null;

            result = _context.Products.Select<Product, IExempleProduct>().FirstOrDefault();
            Dispose();
            return result;
        }

        public IList<IExempleProduct> GetProductsList()
        {
            List<IExempleProduct> result = null;

            result = _context.Products.Select<Product, IExempleProduct>().ToList();

            Dispose();
            return result;
        }
        public List<TResult> GetProductsListWithCriterias<TResult>(Expression<Func<IExempleProduct, bool>> criterias, Expression<Func<Product, TResult>> selectQuery)
        {
            List<TResult> result = null;
            //using (ExempleDbContext context = new ExempleDbContext())
            //{
            //    context.Database.Log = x => Debug.WriteLine(x);
            //    result = context.Products
            //                .Where(criterias)
            //             .Select(selectQuery).ToList();
            //}
            //Or

            result = GetEntities(criterias, selectQuery).Take(10).ToList();
            Dispose();
            return result;
        }

        public IQueryable<IExempleProduct> GetProducts()
        {
            ExempleDbContext context = new ExempleDbContext();

            context.Database.Log = x => Debug.WriteLine(x);

            var result = context.Products;

            return result.Select<Product, IExempleProduct>();
        }

        public IList<TResult> GetProducts2<TResult>(Expression<Func<Product, TResult>> selectQuery)
        {
            var result = GetEntities(selectQuery).ToList();
            return result;
        }

        /// <summary>
        /// Exemple to make a generic method
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selectQuery"></param>
        /// <returns></returns>
        private IQueryable<TResult> GetEntities<TEntity, TResult>(Expression<Func<TEntity, TResult>> selectQuery)
            where TEntity : class
        {
            IQueryable<TResult> result = null;

            result = _context.Set<TEntity>().Select(selectQuery);

            return result;
        }
        /// <summary>
        /// Exemple to make a generic method with criterias
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TCriterias"></typeparam>
        /// <param name="criterias"></param>
        /// <param name="selectQuery"></param>
        /// <returns></returns>
        private IQueryable<TResult> GetEntities<TEntity, TCriterias, TResult>(Expression<Func<TCriterias, bool>> criterias, Expression<Func<TEntity, TResult>> selectQuery)
            where TEntity : class
        {
            IQueryable<TResult> result = null;
            result = _context.Set<TEntity>()
                .Where(criterias)
                .Select(selectQuery);
            return result;
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
            
        }
        /// <summary>
        ///Performs application-defined tasks associated with the release or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

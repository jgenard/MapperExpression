﻿
using System;
using System.Collections.Generic;
using System.Linq;
using MapperExemple.Entity.Interface;
using System.Diagnostics;
using MapperExpression;
using MapperExpression.Extensions;
using System.Linq.Expressions;

namespace MapperExemple.Entity
{
    public class ExempleProduct : IExempleProduct

    {
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
            Product result = null;
            using (ExempleDbContext context = new ExempleDbContext())
            {
                context.Database.Log = x => Debug.WriteLine(x);
                result = (from c in context.Products
                          select c).FirstOrDefault();
            }
            return Mapper.Map<Product, IExempleProduct>(result);
        }

        public IList<IExempleProduct> GetProductsList()
        {
            List<IExempleProduct> result = null;
            using (ExempleDbContext context = new ExempleDbContext())
            {
                context.Database.Log = x => Debug.WriteLine(x);
                result = (from c in context.Products
                          select c).Select<Product, IExempleProduct>().ToList();
            }
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
            IList<TResult> result = null;
            using (ExempleDbContext context = new ExempleDbContext())
            {
                context.Database.Log = x => Debug.WriteLine(x);
                result = context.Products.Select(selectQuery).ToList();
            }
            return result;
        }
    }
}

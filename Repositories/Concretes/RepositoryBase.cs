﻿using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Concretes
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly RepositoryContext _context;
        public int Count => _context.Set<T>().Count();
            

        public RepositoryBase(RepositoryContext context) =>
            _context = context;


        public void Create(T entity) =>
            _context
            .Set<T>()
            .Add(entity);


        public IQueryable<T> FindAll(bool trackChanges) =>
            trackChanges ?
            _context.Set<T>()
            : _context.Set<T>().AsNoTracking();


        public IQueryable<T> FindWithCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
            trackChanges ?
            _context.Set<T>().Where(expression)
            : _context.Set<T>().Where(expression).AsNoTracking();


        public void Delete(T entity) =>
            _context
            .Set<T>()
            .Remove(entity);


        public void Update(T entity) =>
            _context
            .Set<T>()
            .Update(entity);
    }
}

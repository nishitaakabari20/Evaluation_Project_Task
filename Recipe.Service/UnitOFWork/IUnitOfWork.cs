﻿using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Recipe.Service.RepositoryFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.Service.UnitOFWork
{
    public interface IUnitOfWork : IDisposable
    {

        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        int Commit(bool autoHistory = false);
        Task<int> CommitAsync(bool autoHistory = false);
        Task<int> CommitAsyncWithTransaction();
        void ClearContext();
        IDbContextTransaction dbContextTransaction { get; set; }
    }

    public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        TContext Context { get; }
    }
}

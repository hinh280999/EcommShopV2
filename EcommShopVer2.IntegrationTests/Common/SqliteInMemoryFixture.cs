﻿using EcommShop.DataAccessor.DBContext;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EcommShopVer2.IntegrationTests.Common
{
    public class SqliteInMemoryFixture : IDisposable
    {
        private IServiceScope _serviceScope;
        private SqliteConnection _connection;

        public virtual IServiceProvider ServiceProvider
        {
            get
            {
                if (_serviceScope == null)
                {
                    _serviceScope = ConfigureServices(new ServiceCollection()).BuildServiceProvider().CreateScope();
                }

                return _serviceScope.ServiceProvider;
            }
        }

        public virtual EcommShopDBContext Context
            => ServiceProvider.GetRequiredService<EcommShopDBContext>();

        public virtual void CreateDatabase()
        {
            Dispose();
            _connection = new SqliteConnection("Data Source=:memory:");
            _connection.Open();
            Context.Database.EnsureCreated();
        }

        public virtual void Dispose()
        {
            _connection?.Close();
            _connection?.Dispose();
            _serviceScope?.Dispose();
            _serviceScope = null;
        }

        public virtual IServiceCollection ConfigureServices(IServiceCollection services)
            => services
                .AddDbContext<EcommShopDBContext>(b => b.UseSqlite(_connection));
    }
}

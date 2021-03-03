﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MiniORM
{
    public abstract class DbContext
    {
        private readonly DatabaseConnection connection;
        private readonly Dictionary<Type, PropertyInfo> dbSetProperties;

        internal static readonly Type[] AllowedSqlTypes =
        {
            typeof(string),
            typeof(int),
            typeof(uint),
            typeof(long),
            typeof(ulong),
            typeof(decimal),
            typeof(bool),
            typeof(DateTime)
        };
        protected DbContext(string connectionString)
        {
            this.connection= new DatabaseConnection(connectionString);
            this.dbSetProperties = this.DiscoverDbSets();
            using (new ConnectionManager(connection))
            {
                this.InitializeDbSet();
            }
            this.MapAllRelations();
        }
        public void SaveChanges()
        {
            var dbSets = this.dbSetProperties.Select(pi => pi.Value.GetValue(this))
                .ToArray();

            foreach (IEnumerable<object>  dbSet in dbSets)
            {
                var invalidEntities = dbSet.Where(entity => !IsObjectValid(entity))
                    .ToArray();
                if (invalidEntities.Any())
                {
                    throw new InvalidOperationException($"{invalidEntities.Length} Invalid entites found in {dbSet.GetType().Name}");
                }
            }
        }
    }
}
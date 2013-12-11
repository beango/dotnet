using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using MongoDB;
using MongoDB.Configuration;

namespace beango.dal
{
    public class DaoMongo<T> : IDao<T> where T : class
    {
        string connectionString = string.Empty;

        string databaseName = string.Empty;
        
        #region 初始化操作
        /// <summary>
        /// 初始化操作
        /// </summary>
        public DaoMongo()
        {
            connectionString = "Server=192.168.1.169:27017";
            databaseName = "Northwind";
        }
        #endregion

        public MongoConfiguration configuration
        {
            get
            {
                var config = new MongoConfigurationBuilder();

                config.Mapping(mapping =>
                {
                    mapping.DefaultProfile(profile => profile.SubClassesAre(t => t.IsSubclassOf(typeof(T))));
                    mapping.Map<T>();
                });

                config.ConnectionString(connectionString);

                return config.BuildConfiguration();
            }
        }

        public T AddObject(T obj)
        {
            using (Mongo mongo = new Mongo(configuration))
            {
                try
                {
                    mongo.Connect();

                    var db = mongo.GetDatabase(databaseName);

                    var collection = db.GetCollection<T>();
                    collection.Insert(obj);

                    //mongo.Disconnect();
                }
                catch (Exception)
                {
                    //mongo.Disconnect();
                    throw;
                }
            }
            return null;
        }

        public void AddObject(List<T> list)
        {
            foreach (var item in list)
            {
                AddObject(item);
            }
        }

        public void DeleteObject(T obj)
        {
            using (Mongo mongo = new Mongo(configuration))
            {
                try
                {
                    mongo.Connect();

                    var db = mongo.GetDatabase(databaseName);

                    var collection = db.GetCollection<T>(typeof(T).Name);

                    collection.Remove(obj, true);

                    mongo.Disconnect();
                }
                catch (Exception)
                {
                    mongo.Disconnect();
                    throw;
                }
            }
        }

        public void DeleteObject(List<T> list)
        {
        }

        public T GetEntityByKey(object keyValue)
        {
            return null;
        }

        public T GetEntity(Expression<Func<T, bool>> @where)
        {
            using (Mongo mongo = new Mongo(configuration))
            {
                try
                {
                    mongo.Connect();

                    var db = mongo.GetDatabase(databaseName);

                    var collection = db.GetCollection<T>(typeof (T).Name).FindAll().Documents.Where(@where.Compile());

                    mongo.Disconnect();
                    return collection.FirstOrDefault();
                }
                catch (Exception)
                {
                    mongo.Disconnect();
                    throw;
                }
            }
        }

        public T UpdateObject(T obj, params string[] propertys)
        {
            return null;
        }

        public ICollection<T> FindList(Func<T, bool> @where)
        {
            return null;
        }

        public ICollection<T> FindList(params Func<T, bool>[] @where)
        {
            using (Mongo mongo = new Mongo(configuration))
            {
                try
                {
                    mongo.Connect();

                    var db = mongo.GetDatabase(databaseName);

                    var collection = db.GetCollection<T>();

                    return collection.FindAll().Documents.ToList();
                }
                catch (Exception)
                {
                    
                    throw;
                }
                finally
                {
                    mongo.Disconnect();
                }
            }
        }

        public ICollection<TResult> FindList<TKey, TResult>(Expression<Func<T, bool>> @where, Func<T, TKey> orderSelector, Func<T, TResult> selector)
        {
            return null;
        }

        public ICollection<TResult> FindList<TKey, TResult>(Expression<Func<T, bool>> @where, string include, Func<T, TKey> orderSelector, Func<T, TResult> selector)
        {
            return null;
        }

        public int ExecuteSqlNonQuery(string commandText, params DbParameter[] parameters)
        {
            return 0;
        }

        public void Pager<Tkey, TResult>(PageInfo<T, Tkey, TResult> pi)
        {
        }

        public void InitDB()
        {
            using (Mongo mongo = new Mongo(configuration))
            {
                try
                {
                    mongo.Connect();

                    var db = mongo.GetDatabase(databaseName);

                    mongo.Disconnect();
                }
                catch (Exception)
                {
                    mongo.Disconnect();
                    throw;
                }

            }
        }
    }
}

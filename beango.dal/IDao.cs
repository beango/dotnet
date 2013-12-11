using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace beango.dal
{
    public interface IDao<T> where T : class
    {
        /// <summary>
        /// 添加一个对象
        /// </summary>
        /// <param name="obj">待添加的对象</param>
        T AddObject(T obj);

        /// <summary>
        /// 添加一个对象
        /// </summary>
        /// <param name="list">待添加的对象</param>
        void AddObject(List<T> list);

        /// <summary>
        /// 删除一个对象
        /// </summary>
        /// <param name="obj">待删除的对象</param>
        void DeleteObject(T obj);

        /// <summary>
        /// 删除一个对象
        /// </summary>
        /// <param name="list">待删除的对象</param>
        void DeleteObject(List<T> list);

        /// <summary>
        /// 获得一个实体
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="keyValue">主键值</param>
        /// <returns>查询到的对象</returns>
        T GetEntityByKey(object keyValue);

        /// <summary>
        /// 根据条件获取某个对象
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="where">查询条件</param>
        /// <returns>实体</returns>
        T GetEntity(Expression<Func<T, bool>> where);

        /// <summary>
        /// 根据实体的实体键修改实体
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertys"></param>
        T UpdateObject(T obj, params string[] propertys);

        /// <summary>
        /// 根据条件进行查询
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="where">查询条件</param>
        /// <returns>符号条件的实体的集合</returns>
        ICollection<T> FindList(Func<T, bool> where);

        /// <summary>
        /// 根据多个条件进行查询
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="where">查询条件</param>
        /// <returns>符合条件的实体的集合</returns>
        ICollection<T> FindList(params Func<T, bool>[] where);

        /// <summary>
        /// 根据条件、排序、投影进行查找
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <typeparam name="TKey">排序属性类型</typeparam>
        /// <typeparam name="TResult">返回结果类型</typeparam>
        /// <param name="where">查询条件树</param>
        /// <param name="orderSelector">排序</param>
        /// <param name="selector">投影</param>
        /// <returns>符合条件的对象的集合</returns>
        ICollection<TResult> FindList<TKey, TResult>(Expression<Func<T, bool>> where, Func<T, TKey> orderSelector,
                                                            Func<T, TResult> selector);

        /// <summary>
        /// 根据条件和排序进行查找,带抓取功能
        /// </summary>
        /// <typeparam name="T">被查询的实体类型</typeparam>
        /// <typeparam name="TKey">排序属性的类型</typeparam>
        /// <typeparam name="TResult">查询结果类型</typeparam>
        /// <param name="where">查询条件</param>
        /// <param name="include">抓取属性</param>
        /// <param name="orderSelector">排序选择器</param>
        /// <param name="selector">投影选择器</param>
        /// <returns></returns>
        ICollection<TResult> FindList<TKey, TResult>(Expression<Func<T, bool>> where, string include,
                                                            Func<T, TKey> orderSelector, Func<T, TResult> selector);

        /// <summary>
        /// 执行原始SQL命令
        /// </summary>
        /// <param name="commandText">SQL命令</param>
        /// <param name="parameters">参数</param>
        /// <returns>影响的记录数</returns>
        int ExecuteSqlNonQuery(string commandText, params DbParameter[] parameters);

        /// <summary>
        /// 分页泛型方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Tkey"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="pi"></param>
        void Pager<Tkey, TResult>(PageInfo<T, Tkey, TResult> pi);

        /// <summary>
        /// 初始化数据
        /// </summary>
        void InitDB();
    }
}

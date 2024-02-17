using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Music_Store.Repository
{
    /// <summary>
    /// Repository interface 
    /// </summary>
    /// <typeparam name="T"> Model type </typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// Get all data
        /// </summary>
        /// <returns> Enumberable data set </returns>
        IEnumerable<T> Reads();

        /// <summary>
        /// Get data by predicate
        /// </summary>
        /// <param name="predicate"> Where expression </param>
        /// <returns> Enumerable data set </returns>
        IEnumerable<T> Reads(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Get first data by predicate
        /// </summary>
        /// <param name="predicate"> Where expression </param>
        /// <returns> data </returns>
        T Read(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Create new data
        /// </summary>
        /// <param name="entity"> Data model </param>
        void Create(T entity);

        /// <summary>
        /// Update data
        /// </summary>
        /// <param name="entity"> Data model </param>
        void Update(T entity);

        /// <summary>
        /// Delete data
        /// </summary>
        /// <param name="entity"> Data model </param>
        void Delete(T entity);

        /// <summary>
        /// Save changes
        /// </summary>
        void SaveChanges();
    }
}

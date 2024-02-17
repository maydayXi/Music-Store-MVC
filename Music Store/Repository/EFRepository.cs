using Music_Store.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Music_Store.Repository
{
    /// <summary>
    /// Entity framework repository class
    /// </summary>
    /// <typeparam name="TEntity"> Data Model type (Data Table) </typeparam>
    public class EFRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private MusicShopEntities _Context { get; set; }

        /// <summary>
        /// Constructor：Initialize entity context
        /// </summary>
        /// <param name="Context"> entity context </param>
        public EFRepository(MusicShopEntities Context)
        {
            _Context = Context;
        }

        /// <summary>
        /// Get first data by predicate 
        /// </summary>
        /// <param name="predicate"> Where expression </param>
        /// <returns> Data model </returns>
        public TEntity Read(Expression<Func<TEntity, bool>> predicate)
            => _Context.Set<TEntity>().Where(predicate).FirstOrDefault();

        /// <summary>
        /// Get all data
        /// </summary>
        /// <returns> IEnumberable data set </returns>
        public IEnumerable<TEntity> Reads() => _Context.Set<TEntity>().AsEnumerable();

        /// <summary>
        /// Get data by predicate
        /// </summary>
        /// <param name="predicate"> Where expression </param>
        /// <returns> IEnumerable data set </returns>
        public IEnumerable<TEntity> Reads(Expression<Func<TEntity, bool>> predicate)
            => _Context.Set<TEntity>().Where(predicate).AsEnumerable();

        /// <summary>
        /// Create new data
        /// </summary>
        /// <param name="entity"> Data model </param>
        public void Create(TEntity entity)
        {
            _Context.Set<TEntity>().Add(entity);
        }

        /// <summary>
        /// Update data
        /// </summary>
        /// <param name="entity"> Data model </param>
        public void Update(TEntity entity)
        {
            _Context.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Delete data
        /// </summary>
        /// <param name="entity"> Data model </param>
        public void Delete(TEntity entity)
        {
            _Context.Entry(entity).State = EntityState.Deleted;
        }

        /// <summary>
        /// Save all change
        /// </summary>
        public void SaveChanges()
        {
            _Context.SaveChanges();

            if(!_Context.Configuration.ValidateOnSaveEnabled)
                _Context.Configuration.ValidateOnSaveEnabled = true;
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using BrightstarDB.EntityFramework;

namespace NuzzGraph.Entities
{
    public class DirectEntitySet<T> : BrightstarEntitySet<T> where T : class
    {
        /// <summary>
        /// Creates a new entity set attached to the specified context
        /// </summary>
        /// <param name="context">The parent context for the entity set. Must be an instance of <see cref="BrightstarEntityContext"/>.</param>
        public DirectEntitySet(EntityContext context) : base(context)
        {
          
        }

        ///<summary>
        /// Creates a new entity set connected to a LINQ expression and query provider
        ///</summary>
        ///<param name="provider">The LINQ query provider</param>
        ///<param name="expression">The LINQ expression to evaluate</param>
        public DirectEntitySet(IQueryProvider provider, Expression expression)
            : base(provider, expression)
        {
        }

        /// <summary>
        /// Creates a new instance and adds it to this set
        /// </summary>
        /// <returns></returns>
        public new T Create()
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Adds a new entity to the store collection
        /// </summary>
        /// <param name="item">The new entity to be added</param>
        /// <exception cref="EntityFrameworkException">Throw in <paramref name="item"/> is attached to a context different to the one that this entity set is attached to</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="item"/> is not an instance of a class derived from <see cref="BrightstarEntityObject"/></exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="item"/> is NULL</exception>
        public new void Add(T item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds a collection of entities to the store collection
        /// </summary>
        /// <param name="items">The entities to be added</param>
        /// <exception cref="EntityFrameworkException">Throw if one of the <paramref name="items"/> is attached to a context different to the one that this entity set is attached to</exception>
        /// <exception cref="ArgumentException">Thrown if one of the <paramref name="items"/> is not an instance of a class derived from <see cref="BrightstarEntityObject"/></exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="items"/> is null or one of its members is NULL</exception>
        public new void AddRange(IEnumerable<T> items)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds a new item to the entity set, attaching it to the specified resource address
        /// </summary>
        /// <param name="item">The item to be added</param>
        /// <param name="resourceAddress">The resource address that the item is to be attached to</param>
        public new void Add(T item, string resourceAddress)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace DalLayout.Entity
{
    public abstract class BaseEntity<TKey> : IEntity<TKey> where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; }
    }
}

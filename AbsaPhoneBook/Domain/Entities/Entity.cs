using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    [Serializable]
    public abstract class Entity : IDisposable
    {
        protected Entity() {  }
        protected virtual void DisposeEntity() { this.Dispose(); }

        public Guid Id { get; private set; }
        public void SetId(Guid id)
        {
            Id = id;
        }
        public void Dispose()
        {
            DisposeEntity();
            GC.SuppressFinalize(this);
        }
        
    }
}

using System;

namespace TReX.Discovery.Kernel.Shared
{
    public abstract class Entity
    {
        protected Entity()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; protected set; }
    }
}
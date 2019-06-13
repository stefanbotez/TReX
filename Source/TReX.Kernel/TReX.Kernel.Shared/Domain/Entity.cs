using System;

namespace TReX.Kernel.Shared.Domain
{
    public abstract class Entity
    {
        protected Entity()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; protected set; }
    }
}
using System;

namespace EHVAG.DemoInfo.Edicts
{
    public class BaseEntity
    {
        /// <summary>
        /// Gets the entity-id. This ID is raw-data and should not be used for most things
        /// To identify players over time, please use their steamid!
        /// </summary>
        public int EntityID { get; internal set; }

        internal EntityInformation EntityInfo { get; set; }

        protected BaseEntity()
        {
        }

        /// <summary>
        /// This is called after the first update (and the instance baseline) is called
        /// </summary>
        internal virtual void FullyInitialized()
        {
        }
    }
}


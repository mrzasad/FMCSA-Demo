namespace Core.Entities.Common
{
    using System;

    /// <summary>
    ///     Parent class of Data Model Class
    /// </summary>
    public class BaseEntity : IBaseEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseEntity"/> class.
        /// </summary>
        public BaseEntity()
        {
            this.CreatedOn = DateTime.UtcNow;
            this.IsActive = true;
        }

        /// <summary>
        ///     Gets or sets the created by.
        /// </summary>
        public long CreatedBy { get; set; }

        /// <summary>
        ///     Gets or sets the created on.
        /// </summary>
        public DateTime CreatedOn { get; set; }

        ///// <summary>
        /////     Gets or sets the id.
        ///// </summary>
        //public int UserId { get; set; }

        /// <summary>
        ///     Gets or sets the last modified by.
        /// </summary>
        public long? LastModifiedBy { get; set; }

        /// <summary>
        ///     Gets or sets the last modified on.
        /// </summary>
        public DateTime? LastModifiedOn { get; set; }

        /// <summary>
        ///     Gets or sets the is Active.
        /// </summary>
        public bool? IsActive { get; set; }

    }
}

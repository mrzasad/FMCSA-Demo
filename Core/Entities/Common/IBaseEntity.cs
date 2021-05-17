// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IBaseEntity.cs" company="Techno Batch Ltd.">
//   Copyright (c) 2020 by Techno Batch Ltd.  All rights reserved.
// </copyright>
// <summary>
//   Defines the IBaseEntity type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------


namespace Core.Entities.Common
{
    using System;
    /// <summary>
    /// The BaseEntity interface.
    /// </summary>
    public interface IBaseEntity
    {
        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        long CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the created on.
        /// </summary>
        DateTime CreatedOn { get; set; }

        ///// <summary>
        ///// Gets or sets the id.
        ///// </summary>
        //int UserId { get; set; }

        /// <summary>
        /// Gets or sets the last modified by.
        /// </summary>
        long? LastModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the last modified on.
        /// </summary>
        DateTime? LastModifiedOn { get; set; }
    }
}

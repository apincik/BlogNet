using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Blognet.Cms.Domain.Entities
{
    /// <summary>
    /// Base entity for DataContext entities.
    /// @TODO Move createdAt, updatedAt to own entity and inherit in selected entities.
    /// </summary>
    public class BaseEntity
    {
        [Column(Order = 0)]
        public int Id { get; set; }
        [Column(Order = 100)]
        public DateTime CreatedAt { get; set; }
        [Column(Order = 101)]
        public DateTime UpdatedAt { get; set; }
    }
}

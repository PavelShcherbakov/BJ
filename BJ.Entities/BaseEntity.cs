using Dapper.Contrib.Extensions;
using System;

namespace BJ.Entities
{
    public class BaseEntity
    {
        [ExplicitKey]
        [System.ComponentModel.DataAnnotations.Key]
        public Guid Id { get; set; }

        public DateTime CreationDate { get; set; }

        public BaseEntity()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.Now;
        }
    }
}

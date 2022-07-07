using System;
using eCommerceApi.Domain.Entities.Common;

namespace eCommerceApi.Domain.Entities
{
	public class Customer : BaseEntity
	{
        public string Name { get; set; }
        public ICollection <Order> Orders { get; set; }
    }
}


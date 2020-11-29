using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FixTheThing.Database.Entities
{
	public class Item
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public string Name { get; set; }
		public string SerialNumber { get; set; }
		public string Description { get; set; }
		public DateTime? InstalledOn { get; set; }

		public virtual ICollection<WorkOrder> WorkOrders { get; set; }
	}
}

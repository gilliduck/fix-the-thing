using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FixTheThing.Database.Entities
{
	public class WorkOrder
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public virtual Item Item { get; set; }

		public string Activity { get; set; }
		public DateTime? CompletedOn { get; set; }
		public DateTime ScheduledFor { get; set; }
	}
}

using System;
using FixTheThing.Database.Contexts;
using FixTheThing.Database.Entities;

namespace FixTheThing.UI.ViewModels.Inventory
{
	public class ItemVM
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string SerialNumber { get; set; }
		public string Description { get; set; }
		public DateTime? InstalledOn { get; set; }

		public ItemVM()
		{
			//empty for serialization
		}

		public ItemVM(Item entity)
		{
			Name = entity.Name;
			SerialNumber = entity.SerialNumber;
			Description = entity.Description;
			InstalledOn = entity.InstalledOn;
			Id = entity.Id;
		}
	}
}

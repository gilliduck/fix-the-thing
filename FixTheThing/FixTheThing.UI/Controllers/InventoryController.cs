using FixTheThing.Database.Contexts;
using FixTheThing.Database.Entities;
using FixTheThing.UI.ViewModels.Inventory;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FixTheThing.UI.Controllers
{
	public class InventoryController : Controller
	{
		private readonly FttDbContext _dbContext;

		public InventoryController(FttDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> GetInventory([DataSourceRequest] DataSourceRequest request)
		{
			var inventory = _dbContext.Items;
			var results = await inventory.ToDataSourceResultAsync(request, i => new ItemVM(i));
			return Json(results);
		}

		[HttpPost]
		public async Task<IActionResult> AddItem([DataSourceRequest] DataSourceRequest request, ItemVM item)
		{
			try
			{
				if (item is not null && ModelState.IsValid)
				{
					var entity = new Item
					{
						SerialNumber = item.SerialNumber,
						InstalledOn = item.InstalledOn,
						Name = item.Name,
						Description = item.Description
					};
					await _dbContext.Items.AddAsync(entity);
					await _dbContext.SaveChangesAsync();
					item.Id = entity.Id;
				}
			}
			catch
			{
				ModelState.AddModelError("GenericError", "Unable to add item");
			}
			return Json(await new[] { item }.ToDataSourceResultAsync(request, ModelState));
		}

		[HttpPost]
		public async Task<IActionResult> DeleteItem([DataSourceRequest] DataSourceRequest request, ItemVM item)
		{
			try
			{
				var entity = await _dbContext.Items.FindAsync(item.Id);
				_dbContext.Items.Remove(entity);
				await _dbContext.SaveChangesAsync();
			}
			catch
			{
				ModelState.AddModelError("GenericError", "Unable to delete item");
			}
			return Json(await new[] { item }.ToDataSourceResultAsync(request, ModelState));
		}

		[HttpPost]
		public async Task<IActionResult> UpdateItem([DataSourceRequest] DataSourceRequest request, ItemVM item)
		{
			try
			{
				if (item is not null && ModelState.IsValid)
				{
					var entity = await _dbContext.Items.FindAsync(item.Id);
					foreach (var itemProperty in typeof(ItemVM).GetProperties())
					{
						var itemPropertyValue = itemProperty.GetValue(item);
						var entityProperty = typeof(Item).GetProperty(itemProperty.Name);
						if (entityProperty?.GetValue(entity) == itemPropertyValue)
						{
							entityProperty?.SetValue(entity, itemPropertyValue);
						}
					}

					await _dbContext.SaveChangesAsync();
				}
			}
			catch
			{
				ModelState.AddModelError("GenericError", "Unable to update item");
			}
			return Json(await new[] { item }.ToDataSourceResultAsync(request, ModelState));
		}
	}
}

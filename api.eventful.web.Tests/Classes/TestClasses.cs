using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.eventful.web.Tests.Classes
{
	class TestClasses
	{
	}

	public class Category
	{
		public string id { get; set; }
		public string name { get; set; }
	}

	public class Categories
	{
		public List<Category> category { get; set; }
	}

	public class CategoriesRootObject
	{
		public Categories categories { get; set; }
	}
}

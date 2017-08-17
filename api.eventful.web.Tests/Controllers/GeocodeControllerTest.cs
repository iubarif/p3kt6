//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http;
//using System.Text;
//using System.Web.Http;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using api.eventful.web;
//using api.eventful.web.Controllers;
//using System.Threading.Tasks;
//using System.Web.Http.Results;
//using api.eventful.classes.Geocode;

//namespace api.eventful.web.Tests.Controllers
//{
//	[TestClass]
//	public class GeocodeControllerTest
//	{
//		private string noAddress = "";
//		private string InvalidAddress = "40 CourtXYZ St Boston, TX 92108";
//		private string InvalidPostalCode = "92108";

//		private string ValidPostalCode = "02108";

//		//[TestMethod]
//		//public void GetValidPostalCode()
//		//{
//		//	// Arrange
//		//	GeocodeController controller = new GeocodeController();

//		//	// Act  //
//		//	Task<IHttpActionResult> result =  controller.Get(ValidPostalCode);// as OkNegotiatedContentResult<Location>;

//		//	// Assert
//		//	//Assert.AreEqual(result, TaskStatus.Faulted);
//		//	Assert.IsNotNull(result);
//		//}


//		[TestMethod]
//		public async Task GetValidPostalCode()
//		{
//			//var testProducts = GetTestProducts();
//			//var controller = new SimpleProductController(testProducts);
//			GeocodeController controller = new GeocodeController();

//			var result = await controller.Get(ValidPostalCode) as OkNegotiatedContentResult<Location>;
//			Assert.IsNotNull(result);
//			//Assert.AreEqual(testProducts[3].Name, result.Content.Name);
//		}




//		//[TestMethod]
//		//public void GetWithoutParam()
//		//{
//		//	// Arrange
//		//	GeocodeController controller = new GeocodeController();

//		//	// Act
//		//	Task<IHttpActionResult> result = controller.Get(noAddress);

//		//	// Assert
//		//	Assert.AreEqual(result.Status, TaskStatus.Faulted);
//		//	Assert.IsNull(result.Result);			
//		//}

//		//[TestMethod]
//		//public void GetInvalidAddress()
//		//{
//		//	// Arrange
//		//	GeocodeController controller = new GeocodeController();

//		//	// Act
//		//	Task<IHttpActionResult> result = controller.Get(InvalidAddress);

//		//	// Assert
//		//	Assert.AreEqual(result.Status, TaskStatus.Faulted);			
//		//	Assert.IsNull(result.Result);
//		//}

//		//[TestMethod]
//		//public void GetInvalidPostalCode()
//		//{
//		//	// Arrange
//		//	GeocodeController controller = new GeocodeController();

//		//	// Act
//		//	Task<IHttpActionResult> result = controller.Get(InvalidPostalCode);

//		//	// Assert
//		//	Assert.AreEqual(result.Status, TaskStatus.Faulted);			
//		//}


//	}
//}

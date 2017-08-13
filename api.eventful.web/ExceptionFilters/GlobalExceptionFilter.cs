using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace api.eventful.web.ExceptionFilters
{
	public class GlobalExceptionFilter : ExceptionFilterAttribute
	{
		public override void OnException(HttpActionExecutedContext actionExecutedContext)
		{
			base.OnException(actionExecutedContext);

			string exceptionMessage = actionExecutedContext.Exception.InnerException == null ? 
				actionExecutedContext.Exception.Message : actionExecutedContext.Exception.InnerException.Message;

			var controllerName = actionExecutedContext.ActionContext.ActionDescriptor.ControllerDescriptor.ControllerName;
			var actionName = actionExecutedContext.ActionContext.ActionDescriptor.ActionName;
			var errorMessage = string.Format("{0}. Source of the Error -> Controller :  {1} and Action : {2}", 
				"Internal Server Error.Please Contact your Administrator", controllerName, actionName);

			actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
			{
				Content = new StringContent(exceptionMessage),
				ReasonPhrase = errorMessage
			};
		}

	}
}
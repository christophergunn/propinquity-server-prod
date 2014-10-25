using System.Configuration;
using System.Dynamic;
using TeamWin.Propinquity.Web.OpenTok;

namespace TeamWin.Propinquity.Web
{
	using Nancy;

	public class IndexModule : NancyModule
	{
        public IndexModule(OpenTokService opentokService)
		{
            Get["/"] = _ =>
            {
                dynamic locals = new ExpandoObject();

                locals.ApiKey = opentokService.OpenTok.ApiKey.ToString();
                locals.ServerUrl = ConfigurationManager.AppSettings["serverUrl"];

                return View["index", locals];
            };
		}
	}
}
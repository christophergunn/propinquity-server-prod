using System.Dynamic;
using HelloWorld;

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
                locals.SessionId = opentokService.Session.Id;
                locals.Token = opentokService.Session.GenerateToken();

                return View["index", locals];
            };
		}
	}
}
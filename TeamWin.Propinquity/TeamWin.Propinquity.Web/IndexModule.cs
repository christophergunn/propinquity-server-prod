namespace TeamWin.Propinquity.Web
{
	using Nancy;

	public class IndexModule : NancyModule
	{
		public IndexModule()
		{
			Get["/"] = parameters =>
			{
				return View["index"];
			};
		}
	}
}
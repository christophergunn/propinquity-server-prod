using HelloWorld;

namespace TeamWin.Propinquity.Web
{
	using Nancy;

	public class Bootstrapper : DefaultNancyBootstrapper
	{
        protected override void ConfigureApplicationContainer(Nancy.TinyIoc.TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            container.Register<OpenTokService>().AsSingleton();
        }
	}
}
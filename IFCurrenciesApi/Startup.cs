using System.Web.Http;
using Owin;

namespace IFCurrenciesApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            WebApiConfig.Register(config);

            AutofacConfig.Configure(config);

            app.UseWebApi(config);
        }
    }
}
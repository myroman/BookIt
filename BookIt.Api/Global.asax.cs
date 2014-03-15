using System.Web;
using System.Web.Http;

using BookIt.Api.App_Start;

namespace BookIt.Api
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
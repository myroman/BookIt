using System.Web;
using System.Web.Http;
using System.Web.Optimization;

namespace BookIt.Api2
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configure(WebApiConfig.RegisterAutofacStuff);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}

using System;
using System.Web;
using System.Web.Http;

namespace DDD_Sample
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            Bootstrapper.Init();
        }
    }
}
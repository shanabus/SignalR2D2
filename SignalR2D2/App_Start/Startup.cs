using Microsoft.Owin;
using Owin;
using SignalR2D2;

[assembly: OwinStartup(typeof(Startup))]

namespace SignalR2D2
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            //GlobalHost.DependencyResolver.Register(typeof(EstimateHub),
            //    () => new EstimateHub(DependencyResolver.Current.GetService<InMemoryCache>() as ICacheService)
            //);

            app.MapSignalR();
        }
    }
}

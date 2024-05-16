using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ChainOfResp_IoC
{
    internal class Container
    {
        public static IHost CreateHostBuilder()
        {
            return Host
                .CreateDefaultBuilder()
                .ConfigureServices((_, service) =>
                {
                    service.AddSingleton<SimpleGreeting>();
                    service.AddSingleton<NullGreeting>();
                    service.AddSingleton<ShoutingGreeting>();
                    service.AddSingleton<TwoNamesGreeting>();
                    service.AddSingleton<MultipleNamesGreeting>();
                    service.AddSingleton<MixedNormalAndShoutingGreeting>();
                    
                   

                    });
                }).Build();
        }

    }
}

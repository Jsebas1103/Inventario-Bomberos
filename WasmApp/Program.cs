using Blazored.Toast;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using WasmApp;
using Services;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient<IBomberoService, BomberoService>
        (client => client.BaseAddress = new Uri("https://localhost:7238"));
        
builder.Services.AddHttpClient<ICategoriaService, CategoriaService>
        (client => client.BaseAddress = new Uri("https://localhost:7238"));

builder.Services.AddHttpClient<IElementoService, ElementoService>
        (client => client.BaseAddress = new Uri("https://localhost:7238"));

builder.Services.AddHttpClient<IPrestamoService, PrestamoService>
        (client => client.BaseAddress = new Uri("https://localhost:7238"));

builder.Services.AddHttpClient<IDetallePrestamoService, DetallePrestamoService>
        (client => client.BaseAddress = new Uri("https://localhost:7238"));



builder.Services.AddBlazoredToast();

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();

using Application;
using Infrastructure;
using Microsoft.Extensions.FileProviders;
using Persistance;
using Presentation;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddPersistance(builder.Configuration)
        .AddInfrastructure(builder.Configuration)
        .AddApplication()
        .AddPresentation(builder.Configuration);
}

var app = builder.Build();

{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
  
    var skinDirectory = Path.Combine(builder.Configuration
            .GetValue<string>(WebHostDefaults.ContentRootKey),
            "wwwroot",
            "skins");
    
  app.UseStaticFiles(new StaticFileOptions
  {
      FileProvider = new PhysicalFileProvider(skinDirectory),
      RequestPath = "/link/to/skin"
  });
  

    app.UseHttpsRedirection();
    app.UseAuthentication();

    app.UseAuthorization();
    
    app.MapControllers();
}


app.Run();

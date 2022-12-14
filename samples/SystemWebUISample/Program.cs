// MIT License.

using System.Runtime.Loader;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataProtection();

builder.Services.AddSession();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSystemWebAdapters()
    .AddJsonSessionSerializer()
    .WrapAspNetCoreSession()
    .AddHttpHandlers(options =>
    {
    })
    .AddDynamicPages(options =>
    {
        options.UseFrameworkParser = true;
        options.Files = builder.Environment.ContentRootFileProvider;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseWebFormsScripts();

app.UseRouting();

app.UseSession();
app.UseSystemWebAdapters();

app.MapGet("/acls", () => AssemblyLoadContext.All.Select(acl => new
{
    Name = acl.Name,
    Assemblies = acl.Assemblies.Select(a => a.FullName)
}));

app.MapHttpHandlers();

app.Run();

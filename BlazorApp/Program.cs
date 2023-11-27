using Radzen;
using SharedClass;
using SharedClass.Components;
using SharedClass.Components.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddScoped<Login>();
builder.Services.AddScoped<Connection>();
builder.Services.AddScoped<Insert>();
builder.Services.AddScoped<Select>();
builder.Services.AddScoped<Delete>();
builder.Services.AddScoped<Update>();
builder.Services.AddScoped<ExampleJsInterop>();
builder.Services.AddScoped<ContextMenuService>();
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddBlazorBootstrap();
builder.Services.AddRadzenComponents();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

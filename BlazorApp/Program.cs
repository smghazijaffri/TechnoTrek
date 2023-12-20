using Blazored.LocalStorage;
using MudBlazor;
using MudBlazor.Services;
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
builder.Services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; });
builder.Services.AddMudServices();
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;

    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 5000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Text;
});
builder.Services.AddBlazoredLocalStorage();   // local storage
builder.Services.AddBlazoredLocalStorage(config =>
    config.JsonSerializerOptions.WriteIndented = true);  // local storage
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

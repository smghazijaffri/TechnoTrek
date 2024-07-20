using Email = SharedClass.Components.Data.Email;
using SharedClass.Components.Model;
using SharedClass.Components.State;
using SharedClass.Components.Data;
using SharedClass.Components;
using ProtectedLocalStore;
using MudBlazor.Services;
using SharedClass;
using MudBlazor;
using Syncfusion.Blazor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSingleton<AppState>();

builder.Services.AddScoped<BOM>();
builder.Services.AddScoped<CRUD>();
builder.Services.AddScoped<Stock>();
builder.Services.AddScoped<Login>();
builder.Services.AddScoped<Users>();
builder.Services.AddScoped<Email>();
builder.Services.AddScoped<Select>();
builder.Services.AddScoped<Vendor>();
builder.Services.AddScoped<Report>();
builder.Services.AddScoped<SO_Item>();
builder.Services.AddScoped<SI_Item>();
builder.Services.AddScoped<ItemUOM>();
builder.Services.AddScoped<SI_Item>();
builder.Services.AddScoped<UserAuth>();
builder.Services.AddScoped<GI_Items>();
builder.Services.AddScoped<QA_Items>();
builder.Services.AddScoped<BOM_Item>();
builder.Services.AddScoped<PR_Items>();
builder.Services.AddScoped<Customer>();
builder.Services.AddScoped<PO_Items>();
builder.Services.AddScoped<DropDown>();
builder.Services.AddScoped<GR_Items>();
builder.Services.AddScoped<QU_Items>();
builder.Services.AddScoped<PI_Items>();
builder.Services.AddScoped<BO_Items>();
builder.Services.AddScoped<UserRoles>();
builder.Services.AddScoped<ItemClass>();
builder.Services.AddScoped<SaleOrder>();
builder.Services.AddScoped<BulkOrder>();
builder.Services.AddScoped<RFQVendor>();
builder.Services.AddScoped<RFQ_Items>();
builder.Services.AddScoped<Quotation>();
builder.Services.AddScoped<BaseRecord>();
builder.Services.AddScoped<Connection>();
builder.Services.AddScoped<GoodsIssue>();
builder.Services.AddScoped<PCComponent>();
builder.Services.AddScoped<GoodReceipt>();
builder.Services.AddScoped<Stock_Entry>();
builder.Services.AddScoped<SalesInvoice>();
builder.Services.AddScoped<BindDropdown>();
builder.Services.AddScoped<Compatibility>();
builder.Services.AddScoped<AlternateItem>();
builder.Services.AddScoped<UnitofMeasure>();
builder.Services.AddScoped<SingleDropDown>();
builder.Services.AddScoped<PurchaseOrders>();
builder.Services.AddScoped<RolePermissions>();
builder.Services.AddScoped<UserPermissions>();
builder.Services.AddScoped<ProductionOrder>();
builder.Services.AddScoped<PurchaseInvoice>();
builder.Services.AddScoped<StockEntry_Items>();
builder.Services.AddScoped<ExampleJsInterop>();
builder.Services.AddScoped<ReportParameters>();
builder.Services.AddScoped<QualityAssurance>();
builder.Services.AddScoped<RequestForQuotation>();
builder.Services.AddScoped<PurchaseRequisition>();
builder.Services.AddScoped<PurchaseRequisition>();
builder.Services.AddScoped<PurchaseOrderAnalysis>();

builder.Services.AddSyncfusionBlazor();

builder.Services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; });

builder.Services.AddMudServices();
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomEnd;

    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 5000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Text;
});

builder.Services.AddProtectedLocalStore(new EncryptionService(
                new KeyInfo("45BLO2yoJkvBwz99kBEMlNkxvL40vUSGaqr/WBu3+Vg=", "Ou3fn+I9SVicGWMLkFEgZQ==")));

Bold.Licensing.BoldLicenseProvider.RegisterLicense("ulYGC1wHCO/8VYJG0pb0PJe4kr8N6TWzMHAbhJkJfPM=");

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NCaF5cXmZCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdnWXhfd3RdRGVfUUN1VkA=");

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
using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Microsoft.Extensions.Logging;
using MudBlazor;
using MudBlazor.Services;
using ProtectedLocalStore;
using SharedClass;
using SharedClass.Components.Data;
using SharedClass.Components.Model;
using System.Text.Json;

namespace MauiMobileApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddScoped<CRUD>();
            builder.Services.AddScoped<Login>();
            builder.Services.AddScoped<Insert>();
            builder.Services.AddScoped<Select>();
            builder.Services.AddScoped<Delete>();
            builder.Services.AddScoped<Update>();
            builder.Services.AddScoped<Vendor>();
            builder.Services.AddScoped<SO_Item>();
            builder.Services.AddScoped<ItemUOM>();
            builder.Services.AddScoped<SI_Item>();
            builder.Services.AddScoped<GR_Items>();
            builder.Services.AddScoped<PI_Items>();
            builder.Services.AddScoped<PR_Items>();
            builder.Services.AddScoped<PO_Items>();
            builder.Services.AddScoped<DropDown>();
            builder.Services.AddScoped<QU_Items>();
            builder.Services.AddScoped<Customer>();
            builder.Services.AddScoped<ItemClass>();
            builder.Services.AddScoped<SaleOrder>();
            builder.Services.AddScoped<RFQVendor>();
            builder.Services.AddScoped<RFQ_Items>();
            builder.Services.AddScoped<Quotation>();
            builder.Services.AddScoped<Connection>();
            builder.Services.AddScoped<BaseRecord>();
            builder.Services.AddScoped<GoodReceipt>();
            builder.Services.AddScoped<GoodReceipt>();
            builder.Services.AddScoped<SalesInvoice>();
            builder.Services.AddScoped<BindDropdown>();
            builder.Services.AddScoped<UnitofMeasure>();
            builder.Services.AddScoped<Compatibility>();
            builder.Services.AddScoped<AlternateItem>();
            builder.Services.AddScoped<PurchaseOrders>();
            builder.Services.AddScoped<SingleDropDown>();
            builder.Services.AddScoped<PurchaseInvoice>();
            builder.Services.AddScoped<ExampleJsInterop>();
            builder.Services.AddScoped<RequestForQuotation>();
            builder.Services.AddScoped<PurchaseRequisition>();
            builder.Services.AddScoped<PurchaseRequisition>();

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

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
using Microsoft.Extensions.Caching.Memory;
using BoldReports.Web.ReportViewer;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp.Api;

[Route("api/{controller}/{action}/{id?}")]
public class ReportController : ControllerBase, IReportController
{
    private readonly IMemoryCache _cache;
    private IWebHostEnvironment _hostingEnvironment;

    public ReportController(IWebHostEnvironment webHostEnvironment, IMemoryCache cache)
    {
        _hostingEnvironment = webHostEnvironment;
        _cache = cache;
    }

    [ActionName("GetResource")]
    [AcceptVerbs("GET")]
    public object GetResource(ReportResource resource)
    {
        return ReportHelper.GetResource(resource, this, _cache);
    }

    [NonAction]
    public void OnInitReportOptions(ReportViewerOptions reportOption)
    {
        //string basePath = _hostingEnvironment.WebRootPath;
        FileStream inputStream = new("D:/FYP2/TechnoTrek/SharedClass/wwwroot/reports/Sample Inventory.rdl",
            FileMode.Open, FileAccess.Read);
        MemoryStream reportStream = new MemoryStream();
        inputStream.CopyTo(reportStream);
        reportStream.Position = 0;
        inputStream.Close();
        reportOption.ReportModel.Stream = reportStream;
    }

    [NonAction]
    public void OnReportLoaded(ReportViewerOptions reportOption)
    {

    }

    [HttpPost]
    public object PostFormReportAction()
    {
        return ReportHelper.ProcessReport(null, this, _cache);
    }

    [HttpPost]
    public object PostReportAction([FromBody] Dictionary<string, object> jsonResult)
    {
        return ReportHelper.ProcessReport(jsonResult, this, _cache);
    }
}

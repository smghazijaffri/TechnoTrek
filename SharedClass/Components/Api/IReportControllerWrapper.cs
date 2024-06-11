using BoldReports.Web.ReportViewer;

namespace SharedClass.Components.Api
{
    public interface IReportControllerWrapper
    {
        object GetResource(ReportResource resource);
        void OnInitReportOptions(ReportViewerOptions reportOption);
        void OnReportLoaded(ReportViewerOptions reportOption);
        object PostFormReportAction();
        object PostReportAction(Dictionary<string, object> jsonResult);
    }
}

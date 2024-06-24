using BoldReports.Web.ReportViewer;
using SharedClass.Components.Api;

namespace BlazorApp.Api
{
    public class ReportControllerWrapper : IReportControllerWrapper
    {
        private readonly ReportController _reportController;

        public ReportControllerWrapper(ReportController reportController)
        {
            _reportController = reportController;
        }

        public object GetResource(ReportResource resource)
        {
            return _reportController.GetResource(resource);
        }

        public void OnInitReportOptions(ReportViewerOptions reportOption)
        {
            _reportController.OnInitReportOptions(reportOption);
        }

        public void OnReportLoaded(ReportViewerOptions reportOption)
        {
            _reportController.OnReportLoaded(reportOption);
        }

        public object PostFormReportAction()
        {
            return _reportController.PostFormReportAction();
        }

        public object PostReportAction(Dictionary<string, object> jsonResult)
        {
            return _reportController.PostReportAction(jsonResult);
        }
    }
}

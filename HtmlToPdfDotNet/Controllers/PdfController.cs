using HtmlToPdfDotNet.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace HtmlToPdfDotNet.Controllers
{
    public class PDFController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ConvertNow()
        {
            var converter = new HtmlToPdfConverter();
            string outputPath = Path.Combine(Path.GetTempPath(), "Output_.pdf");

            try
            {
                var html = "<html><body><h1>Hello, world!</h1></body></html>";
                await converter.ConvertHtmlToPdf(GetHTML(), outputPath);
                var pdfBytes = await System.IO.File.ReadAllBytesAsync(outputPath);
                return File(pdfBytes, "application/pdf", "outputppt.pdf");
            }
            catch (Exception ex)
            {
                // Log the exception (not shown here for brevity)
                return StatusCode(500, "An error occurred while generating the PDF.");
            }
        }


        public static string GetHTML()
        {
            var reportType = "Daily";
            var html = $"<!DOCTYPE html><html lang=\"en\"><head>" +
                        "<meta charset=\"UTF-8\">" +
                        "<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">" +
                        $"<title>{reportType} Report</title>" +
                        "<style>" +
                        "body { font-family: sans-serif; }" +

                        ".table-heading { text-align: start; font-size: 12px; color: #01070A; font-weight: 600; padding: 11px 20px 11px 20px; }" +
                        ".table-data { font-size: 12px; padding: 15px 0px 15px 20px; }" +
                        "@media print { .pdf-container { padding: 30px 0px; } .start-new-page { page-break-before: always; } }" +
                        ".avoid-page-break { page-break-inside: avoid; }" +
                        "</style>" +
                        "</head><body>" +
                        "<div class=\"pdf-container\">" +
                        $"<div><img src='https://storage.googleapis.com/ez-goal-tracker-public-bucket/logo.png' alt='Logo'/></div>" +
                        "<div style=\"margin-top:20px; display:inline-block;width:100%\">" +
                        "<div style=\"float:left\">" +
                        $"<div style=\"font-size: 24px;color:#01070A; font-weight:600;\">EZGoal Tracker {reportType} Report</div>" +
                        $"<div style=\"font-size:12px;color:#676A6C\">Here is your {reportType} report, based on your activities</div>" +
                        "</div></div>" +
                        "<div style='font-size:16px; color:#FFF; background:#007B83; padding:10px 30px; margin-top:46px; margin-left:auto; font-weight:600; width:140px'>Achieved Tasks</div>" +
                        "<div>";

            var i = 0;
            var j = 0;
            while (i < 10)
            {
                i++;
                html += $"<div style=\"font-size: 12px;margin-bottom: 4px; color:#01070A; background: #C9CEDA; padding:12px 40px; width:100px;font-weight: 600;\">October {i + "-" + j} </div>" +
                        "<div>" +
                        "<table>" +
                        "<tr style=\"border-top:1px solid #C9CEDA; background:#E1E5F0; width:100vw\">" +
                        "<th class=\"table-heading\" style=\"width:22vw\">TASK NAME</th>" +
                        "<th class=\"table-heading\" style=\"width:22vw\">GOAL NAME</th>" +
                        "<th style=\"width:10vw\" class=\"table-heading\">TYPE</th>" +
                        "<th class=\"table-heading\" style=\"width:18vw\">START DATE & TIME</th>" +
                        "<th class=\"table-heading\" style=\"width:18vw\">END DATE & TIME</th>" +
                        "<th class=\"table-heading\" style=\"width:10vw\">PRIORITY</th>" +
                        "</tr>";

                j = 0;
                while (j < 15)
                {
                    j++;
                    html += "<tr class='avoid-page-break' style='border-bottom: 1px solid #C9CEDA; width:100vw'>" +
                            $"<td class='table-data' style='width:18vw'>Task {i + j}</td>" +
                            $"<td class='table-data' style='width:18vw'>Task Goal {i + j}</td>" +
                            $"<td class='table-data' style='width:10vw'>" +
                            $"<span style='font-size: 12px; background: rgba(108, 99, 255, 0.1); padding: 5px 15px; color: #6C63FF; text-align: center; border-radius: 5px;'>TaskType {i + j}</span>" +
                            $"</td>" +
                            $"<td class='table-data' style='width:18vw'> FormattedTaskStartDate {i + j}</td>" +
                            $"<td class='table-data' style='width:18vw'> FormattedTaskEndDate {i + j}</td>" +
                            $"<td class='table-data' style='width:18vw'> Priority {i + j}</td>" +
                            "</tr>";
                }

                html += "</table></div>";
            }

            html += "</div></body></html>";
            return html;
        }

    }
}

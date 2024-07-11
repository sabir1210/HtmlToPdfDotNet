using PuppeteerSharp;
using PuppeteerSharp.Media;

namespace HtmlToPdfDotNet.Helpers
{
    public class HtmlToPdfConverter
    {
        public async Task ConvertHtmlToPdf(string htmlContent, string outputPath)
        {
            var browserFetcher = new BrowserFetcher();
            await browserFetcher.DownloadAsync();

            using var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true,
                ExecutablePath = (await browserFetcher.DownloadAsync()).GetExecutablePath(), // You can manually download copy chrome browser data to a directory and give back chrome.exe path to ExecuteablePath
                Timeout = 300000 // 5 minutes
            });

            using var page = await browser.NewPageAsync();
            await page.SetContentAsync(htmlContent);

            var pdfOptions = new PdfOptions
            {
                // Path = outputPath,
                PrintBackground = true,
                //Format = PdfFormat.A4
                DisplayHeaderFooter = true,
                HeaderTemplate = @"<div style='font-size:10px; width:100%; text-align:right; padding-right:20px; border-bottom: 1px solid #808080;'>
                                    Page <span class='pageNumber'></span> of <span class='totalPages'></span>
                                   </div>",
                FooterTemplate = "<div></div>", // Empty footer
                MarginOptions = new MarginOptions
                {
                    Top = "20px", // Adjust as necessary to provide space for the header
                    Bottom = "20px", // Adjust as necessary to provide space for the footer
                    Left = "20px",
                    Right = "20px"
                }
            };

            await page.PdfAsync(outputPath, pdfOptions);
            await browser.CloseAsync();
        }
    }
}

//var browserFetcher = new BrowserFetcher();
//await browserFetcher.DownloadAsync();

//using var browser = await Puppeteer.LaunchAsync(new LaunchOptions
//{
//    Headless = true,
//    ExecutablePath = (await browserFetcher.DownloadAsync()).GetExecutablePath()
//});

//using var page = await browser.NewPageAsync();
//await page.SetContentAsync(htmlContent);
//await page.PdfAsync(outputPath);
//await browser.CloseAsync();

//public async Task ConvertHtmlToPdf(string htmlContent, string outputPath)
//{
//    // Initialize BrowserFetcher
//    var browserFetcher = new BrowserFetcher();

//    // Get the most recent Chromium revision
//    var revisionInfo = await browserFetcher.DownloadAsync();

//    // Launch the browser with the downloaded Chromium
//    using var browser = await Puppeteer.LaunchAsync(new LaunchOptions
//    {
//        Headless = true,
//        ExecutablePath = revisionInfo.GetExecutablePath(),
//    });

//    // Create a new page
//    using var page = await browser.NewPageAsync();

//    // Set the HTML content
//    await page.SetContentAsync(htmlContent);

//    // Convert to PDF
//    await page.PdfAsync(outputPath);

//    // Close the browser
//    await browser.CloseAsync();
//}


//public async Task ConvertHtmlToPdf(string htmlContent, string outputPath)
//{
//    // Initialize BrowserFetcher with the default options
//    var browserFetcherOptions = new BrowserFetcherOptions { Path = ".local-chromium" };
//    var browserFetcher = new BrowserFetcher(browserFetcherOptions);

//    // Get the most recent Chromium revision
//    var revision = await browserFetcher.DownloadAsync(BrowserFetcher.DefaultChromiumRevision);

//    // Launch the browser with the downloaded Chromium
//    using var browser = await Puppeteer.LaunchAsync(new LaunchOptions
//    {
//        Headless = true,
//        ExecutablePath = revision.ExecutablePath
//    });

//    // Create a new page
//    using var page = await browser.NewPageAsync();

//    // Set the HTML content
//    await page.SetContentAsync(htmlContent);

//    // Convert to PDF
//    await page.PdfAsync(outputPath);

//    // Close the browser
//    await browser.CloseAsync();
//}

//public async Task ConvertHtmlToPdf(string htmlContent, string outputPath)
//{
//    var browserFetcher = new BrowserFetcher();

//    // Get the latest revision information
//    var revisionInfo = await browserFetcher.DownloadAsync(await browserFetcher.GetLatestRevisionAsync());

//    // Launch the browser with the downloaded Chromium
//    using var browser = await Puppeteer.LaunchAsync(new LaunchOptions
//    {
//        Headless = true,
//        ExecutablePath = revisionInfo.ExecutablePath
//    });

//    // Create a new page
//    using var page = await browser.NewPageAsync();

//    // Set the HTML content
//    await page.SetContentAsync(htmlContent);

//    // Convert to PDF
//    await page.PdfAsync(outputPath);

//    // Close the browser
//    await browser.CloseAsync();
//}

//public async Task ConvertHtmlToPdf(string htmlContent, string outputPath)
//{
//    // Download Chromium if necessary
//    await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultRevision);

//    // Launch the browser
//    using var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true });
//    using var page = await browser.NewPageAsync();

//    // Set the HTML content
//    await page.SetContentAsync(htmlContent);

//    // Convert to PDF
//    await page.PdfAsync(outputPath);
//}


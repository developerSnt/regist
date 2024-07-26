
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http;
using System.Xml;
using System;
using System.IO;
using System.Drawing; // Add this
using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using static System.Net.WebRequestMethods;
using regist;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
//using PdfSharpCore;
//using PdfSharpCore.Pdf;
//using TheArtOfDev.HtmlRenderer.PdfSharp;
//using PdfSharpCore.Drawing;
using System.IO;
using System.Threading.Tasks;

using System;
//using System.Drawing;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
//using PdfSharpCore.Drawing.Layout;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using SixLabors.Fonts;
using System.Net;


using Spire.Pdf;
using Spire.Pdf.Graphics;
using System.IO;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactPolicy", builder =>
    {
        builder.WithOrigins("http://localhost:3000" , "https://newapp-b5wt.onrender.com" )
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddDbContext<AppData>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors("ReactPolicy");
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapPost("/api/Registration", async (AppData dbContext, Regis formData) =>

{
    if (formData != null)
    {
        try
        {
            await dbContext.Regist.AddAsync(formData);
            await dbContext.SaveChangesAsync();
            return Results.Created($"/api/Registration/{formData.Id}", formData);
        }
        catch (Exception ex)
        {
            // Log the exception for debugging purposes
            Console.WriteLine($"Error saving registration data: {ex.Message}");
            return Results.BadRequest("Failed to save registration data.");
        }
    }
    else
    {
        return Results.BadRequest("Invalid data format");
    }
});
//app.MapGet("/api/data", async (AppData dbContext) =>
//{
//    var dataList = await dbContext.Regist.ToListAsync();
//    return Results.Ok(dataList); // Return list of data
//});

//app.MapGet("/api/Login", async (AppData dbContext, string email, string password) =>
//{

//    var user = await dbContext.Regist.FirstOrDefaultAsync(u => u.Email == email && u.password == password);

//    if (user == null)
//    {
//        return Results.NotFound("Not valid username Or Password");
//    }

//    if (user.password == password)
//    {
//        return Results.Ok("Successfully logged in");
//    }
//    else
//    {
//        return Results.NotFound("Not valid username Or Password");
//    }
//});

app.MapGet("/api/Login", async (AppData dbContext, string email, string password) =>
{
    try
    {
        var user = await dbContext.Regist.FirstOrDefaultAsync(u => u.Email == email && u.password == password);

        if (user == null)
        {
            return Results.NotFound("Invalid username or password");
        }

        // If login is successful, return user details
        return Results.Ok(new { UserName = user.FirstName }); // Adjust properties as per your User model
    }
    catch (Exception ex)
    {
        return Results.BadRequest("Error logging in: " + ex.Message);
    }
});

//app.MapGet("/api/data", async (AppData dbContext, int page = 1, int pageSize = 5) =>
//{
//    try
//    {

//        int skipAmount = (page - 1) * pageSize;


//        var data = await dbContext.Regist
//            .Skip(skipAmount)
//            .Take(pageSize)
//            .ToListAsync();


//        return Results.Ok(data);
//    }
//    catch (Exception ex)
//    {
//      
//        return Results.BadRequest("Error fetching data: " + ex.Message);
//    }
//});


app.MapGet("/api/data", async (AppData dbContext, int page, int pageSize) =>
{
    try
    {
        int skipAmount = (page - 1) * pageSize;

        var data = await dbContext.Regist
            .Skip(skipAmount)
            .Take(pageSize)
            .ToListAsync();

        var totalDataCount = await dbContext.Regist.CountAsync(); 

        return Results.Ok(new { Data = data, TotalData = totalDataCount });
    }
    catch (Exception ex)
    {
       
        return Results.BadRequest("Error fetching data: " + ex.Message);
    }
});


//app.MapGet("/api/viewDetails", async (string urls, string name) =>
//{
//    foreach (var url in urls)
//    {
//        try
//        {
//            Console.WriteLine($"Scraping content from: {url}");
//            string content = ScrapeArticleContent(url, "article_content", "col-12 col-md-9");
//            Console.WriteLine("Content:");
//            Console.WriteLine(content);
//            Console.WriteLine("============================================================");
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Failed to scrape content from {url}: {ex.Message}");
//        }
//    }

//    static string ScrapeArticleContent(string url, string elementId, string elementClass)
//    {
//        var web = new HtmlWeb();
//        var doc = web.Load(url);

//        // Try to get the content by ID
//        var contentNode = doc.DocumentNode.SelectSingleNode($"//*[@id='{elementId}']");

//        // If not found by ID, try to get the content by Class
//        if (contentNode == null)
//        {
//            contentNode = doc.DocumentNode.SelectSingleNode($"//*[@class='{elementClass}']");
//        }

//        if (contentNode == null)
//        {
//            throw new Exception("Content not found using provided ID or Class.");
//        }

//        return contentNode.InnerText.Trim();
//    }
//});

//app.MapGet("/api/viewDetails", async (string urls) =>
//{


//    foreach (var url in urls)
//    {
//        try
//        {
//            Console.WriteLine($"Scraping content from: {url}");
//            string content = await ScrapeArticleContentAsync(url, "article_content", "col-12 col-md-9");
//            Console.WriteLine("Content:");
//            Console.WriteLine(content);
//            Console.WriteLine("============================================================");
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Failed to scrape content from {url}: {ex.Message}");
//        }
//    }
//});

//async Task<string> ScrapeArticleContentAsync(string url, string elementId, string elementClass)
//{
//    try
//    {
//        var web = new HtmlWeb();
//        var doc = await web.LoadFromWebAsync(url);

//        // Try to get the content by ID
//        var contentNode = doc.DocumentNode.SelectSingleNode($"//*[@id='{elementId}']");

//        // If not found by ID, try to get the content by Class
//        if (contentNode == null)
//        {
//            contentNode = doc.DocumentNode.SelectSingleNode($"//*[@class='{elementClass}']");
//        }

//        if (contentNode == null)
//        {
//            throw new Exception("Content not found using provided ID or Class.");
//        }

//        return contentNode.InnerText.Trim();
//    }
//    catch (Exception ex)
//    {
//        throw new Exception($"Failed to scrape content from {url}: {ex.Message}");
//    }
//}
//app.MapGet("/api/viewDetails", async (string url) =>
//{
//    try
//    {
//        if (string.IsNullOrWhiteSpace(url))
//        {
//            return Results.BadRequest("URL parameter is required.");
//        }



//        foreach (var singleUrl in url)
//        {
//            try
//            {
//                Console.WriteLine($"Scraping content from: {singleUrl}");
//                string content = await ScrapeArticleContentAsync(singleUrl, "article_content", "col-12 col-md-9");
//                Console.WriteLine("Content:");
//                Console.WriteLine(content);
//                Console.WriteLine("============================================================");
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Failed to scrape content from {singleUrl}: {ex.Message}");
//            }
//        }

//        return Results.Ok("Data : " + singleUrl);
//    }
//    catch (Exception ex)
//    {
//        return Results.BadRequest($"Error scraping content: {ex.Message}");
//    }
//});
app.MapPost("/api/insetdatainfo", async (AppData dbContext, DataInfo formData) =>
{
    if (formData != null)
    {
        try
        {
            await dbContext.DataInfos.AddAsync(formData);
            await dbContext.SaveChangesAsync();
            return Results.Created($"/api/DataInfos/{formData.Id}", formData);
        }
        catch (Exception ex)
        {
            // Log the exception for debugging purposes
            Console.WriteLine($"Error saving fetch data info: {ex.Message}");
            return Results.BadRequest("Failed to save fetch data info.");
        }
    }
    else
    {
        return Results.BadRequest("Invalid data format");
    }
});

app.MapGet("/api/Detailsview", async (AppData dbContext, string url, string name) =>
{
    try
    {
        var user = await dbContext.DataInfos.FirstOrDefaultAsync(u =>  u.source == name);
        var elementclass1 = user.elementClass;
        var elementid1 = user.elementId;
        var source = user.source;
        if (user == null || source == name)
        {
            string content = await ScrapeArticleContentAsync(url, elementid1, elementclass1);
                  return Results.Ok(content);
          
        }
        else
        {
            return Results.NotFound("Invalid url or name");
        }
     
        
    }
    catch (Exception ex)
    {
        return Results.BadRequest("Error: " + ex.Message);
    }
});


// Example of dependency injection
app.MapPut("/api/update/{id}", async (int id, string name, string elementclass, string elementid, string domion,string url, DataInfo newDataInfo, AppData dbContext) =>
{
    var existingDataInfo = await dbContext.DataInfos.FindAsync(id);
    if (existingDataInfo == null)
    {
        return Results.NotFound($"Data with ID {id} not found.");
    }

    // Update properties based on newDataInfo or route parameters
    existingDataInfo.url = url; // Update from newDataInfo if necessary
    existingDataInfo.source = name; // Use route parameter directly for name
    existingDataInfo.elementClass = elementclass; // Use route parameter directly for elementclass
    existingDataInfo.elementId = elementid; // Use route parameter directly for elementid
    existingDataInfo.Domion = domion; // Use route parameter directly for domion

    await dbContext.SaveChangesAsync();

    return Results.NoContent(); // 204 No Content on successful update
});

//app.MapDelete("/api/Delete/{id}", async (int id, AppData dbContext) =>
//{
//    var userid = await dbContext.DataInfos.FindAsync(id);
//    if (userid == null)
//    {
//        return Results.NotFound($"Data with ID {id} not found.");
//    }

//});
app.MapDelete("/api/Delete/{id}", async (int id, AppData dbContext) =>
{
    var dataInfo = await dbContext.DataInfos.FindAsync(id);
    if (dataInfo == null)
    {
        return Results.NotFound($"Data with ID {id} not found.");
    }

    dbContext.DataInfos.Remove(dataInfo);
    await dbContext.SaveChangesAsync();

    return Results.Ok($"Data with ID {id} deleted successfully.");
});

//app.MapGet("/api/Detailsview", async (AppData dbContext, string url, string name) =>
//{
//    try
//    {
//        var user = await dbContext.DataInfos.FirstOrDefaultAsync(u => u.url == url && u.source == name);


//            var elementclass1 = user.elementClass;
//            var elementid1 = user.elementId;
//            string content = await ScrapeArticleContentAsync(url, elementid1, elementclass1);
//            return Results.Ok(content);


//    }
//    catch (Exception ex)
//    {
//        return Results.BadRequest("Error: " + ex.Message);
//    }
//});


//app.MapGet("/api/Detailsview", async (string url, string name) =>
//{
//    try
//    {
//        if (string.IsNullOrWhiteSpace(url))
//        {
//            return Results.BadRequest("URL parameter is required.");
//        }

//        if (string.IsNullOrWhiteSpace(name))
//        {
//            return Results.BadRequest("Name parameter is required.");
//        }

//        if (name == "Thevalleypost.com")
//        {
//            string content = await ScrapeArticleContentAsync(url, "primary", "content-area ");
//            return Results.Ok(content);
//        }
//        else if (name == "India.com")
//        {
//            string content = await ScrapeArticleContentAsync(url, "article_content", "col-12 col-md-9");
//            return Results.Ok(content);
//        }
//        else if (name == "NDTV News")
//        {
//            string content = await ScrapeArticleContentAsync(url, "lft-content-area", "col-900 mr-60");
//            return Results.Ok(content);
//        }
//        else if (name == "Hindustan Times")
//        {
//            string content = await ScrapeArticleContentAsync(url, "row", "");
//            return Results.Ok(content);
//        }
//        else
//        {
//            return Results.BadRequest("Unsupported 'name' parameter.");
//        }
//    }
//    catch (Exception ex)
//    {
//        return Results.BadRequest($"Error scraping content: {ex.Message}");
//    }
//});
app.MapGet("/api/NewList", async (AppData dbContext) =>
{
   var NewsList = await dbContext.DataInfos.ToListAsync();
    return Results.Ok(NewsList); // Return list of data
});
app.MapGet("/api/NewList/{id}", async(AppData dbContext, int id) =>
{
    var user = await dbContext.DataInfos.FindAsync(id);
    return Results.Ok(new { url = user.url, source = user.source, elementClass = user.elementClass , elementId =user.elementId , Domion =user.Domion }); 

});

//app.MapPost("/createpdf", async (string title, string description, DateTime newsDate) =>
//{
//    try
//    {
//        // Step 1: Validate input (optional)
//        if (string.IsNullOrEmpty(title))
//        {
//            return Results.BadRequest("Title cannot be empty.");
//        }

//        // Step 2: Create a PDF document
//        var pdfFileName = $"{title}.pdf"; // Use the title in the filename
//        var pdfDirectory = Path.Combine("D:\\TestPdf"); // Directory to save PDF
//        var pdfPath = Path.Combine(pdfDirectory, pdfFileName);

//        // Ensure the directory exists; create if it doesn't
//        Directory.CreateDirectory(pdfDirectory);

//        // Step 3: Create the PDF document
//        using (var document = new PdfDocument())
//        {
//            var page = document.AddPage();
//            var gfx = XGraphics.FromPdfPage(page);
//            var font = new XFont("Verdana", 20, XFontStyle.Bold);

//            // Draw title
//            gfx.DrawString(title, font, XBrushes.Black,
//                new XRect(0, 20, page.Width, 50), // Adjust Y coordinate and height as needed
//                XStringFormats.Center);

//            // Draw description
//            font = new XFont("Arial", 14, XFontStyle.Regular);
//            var descriptionRect = new XRect(50, 80, page.Width - 100, page.Height - 200); // Adjust rectangle as needed
//            gfx.DrawString(description, font, XBrushes.Black, descriptionRect, XStringFormats.TopLeft);

//            // Draw news date
//            font = new XFont("Arial", 12, XFontStyle.Regular);
//            gfx.DrawString($"Date: {newsDate.ToShortDateString()}", font, XBrushes.Black,
//                new XRect(50, page.Height - 50, page.Width - 100, 50), // Adjust Y coordinate and height as needed
//                XStringFormats.BottomLeft);
//            var imageUrl = "D:\\Img\\Test.png";
//            // Step 4: Embed image (assuming imageUrl is a valid local file path)
//            if (!string.IsNullOrEmpty(imageUrl))
//            {
//                if (System.IO.File.Exists(imageUrl))
//                {
//                    try
//                    {
//                        XImage image = XImage.FromFile(imageUrl);
//                        gfx.DrawImage(image, 50, 150, 200, 100); // Adjust coordinates and size as needed
//                    }
//                    catch (Exception ex)
//                    {
//                        Console.WriteLine($"Error loading image: {ex.Message}");
//                    }
//                }
//                else
//                {
//                    Console.WriteLine($"Image file not found: {imageUrl}");
//                }
//            }

//            // Step 5: Save the PDF document
//            document.Save(pdfPath);
//        }

//        return Results.Ok($"PDF '{pdfFileName}' created and saved to D:\\TestPdf.");
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine($"Error creating PDF: {ex.Message}");
//        return Results.NotFound("PDF creation failed.");
//    }
//});


//app.MapPost("/createpdf", async (string title, string description, DateTime newsDate, string imageUrl) =>
//{
//    try
//    {
//        // Step 1: Validate input (optional)
//        if (string.IsNullOrEmpty(title))
//        {
//            return Results.BadRequest("Title cannot be empty.");
//        }

//        // Step 2: Create a PDF document
//        var pdfFileName = $"{title}.pdf"; // Use the title in the filename
//        var pdfDirectory = Path.Combine("D:\\TestPdf"); // Directory to save PDF
//        var pdfPath = Path.Combine(pdfDirectory, pdfFileName);

//        // Ensure the directory exists; create if it doesn't
//        Directory.CreateDirectory(pdfDirectory);

//        // Step 3: Create the PDF document
//        using (var document = new PdfDocument())
//        {
//            var page = document.AddPage();
//            var gfx = XGraphics.FromPdfPage(page);
//            var font = new XFont("Verdana", 20, XFontStyle.Bold);

//            // Step 4: Draw title (centered)
//            gfx.DrawString(title, font, XBrushes.Black,
//                new XRect(0, 20, page.Width, 50), // Adjust Y coordinate and height as needed
//                XStringFormats.Center);

//            // Step 5: Draw news date (right-aligned)
//            font = new XFont("Arial", 12, XFontStyle.Regular);
//            var dateText = $"Date: {newsDate.ToShortDateString()}";
//            var dateSize = gfx.MeasureString(dateText, font);
//            gfx.DrawString(dateText, font, XBrushes.Black,
//                new XRect(page.Width - dateSize.Width - 50, 20, page.Width, 50), // Adjust coordinates as needed
//                XStringFormats.TopLeft);

//            // Step 6: Embed image (centered)
//            if (!string.IsNullOrEmpty(imageUrl) && System.IO.File.Exists(imageUrl))
//            {
//                try
//                {
//                    XImage image = XImage.FromFile(imageUrl);
//                    double imageWidth = 200;
//                    double imageHeight = imageWidth * image.PixelHeight / image.PixelWidth;
//                    gfx.DrawImage(image, (page.Width - imageWidth) / 2, 100, imageWidth, imageHeight); // Adjust coordinates and size as needed
//                }
//                catch (Exception ex)
//                {
//                    Console.WriteLine($"Error loading image: {ex.Message}");
//                }
//            }
//            else
//            {
//                Console.WriteLine($"Image file not found: {imageUrl}");
//            }

//            // Step 7: Draw description (justified)
//            font = new XFont("Arial", 12, XFontStyle.Regular);
//            var descriptionRect = new XRect(50, 300, page.Width - 100, page.Height - 350); // Adjust rectangle as needed
//            gfx.DrawString(description, font, XBrushes.Black, descriptionRect, XStringFormats.TopLeft);

//            // Step 8: Save the PDF document
//            document.Save(pdfPath);
//        }

//        return Results.Ok($"PDF '{pdfFileName}' created and saved to D:\\TestPdf.");
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine($"Error creating PDF: {ex.Message}");
//        return Results.NotFound("PDF creation failed.");
//    }
//});

//app.MapPost("/createpdf", async (string title, string description, string newsDate, string imageUrl) =>
//{
//    try
//    {
//        // Step 1: Validate input (optional)
//        if (string.IsNullOrEmpty(title))
//        {
//            return Results.BadRequest("Title cannot be empty.");
//        }

//        // Step 2: Create a PDF document
//        var pdfFileName = $"{title}.pdf"; // Use the title in the filename
//        var pdfDirectory = Path.Combine("D:\\TestPdf"); // Directory to save PDF
//        var pdfPath = Path.Combine(pdfDirectory, pdfFileName);

//        // Ensure the directory exists; create if it doesn't
//        Directory.CreateDirectory(pdfDirectory);

//        // Step 3: Create the PDF document
//        using (var document = new PdfDocument())
//        {
//            var page = document.AddPage();
//            var gfx = XGraphics.FromPdfPage(page);
//            var font = new XFont("Verdana", 20, XFontStyle.Bold);
//            var layout = new XRect(20, 20, page.Width, page.Height);
//            // Draw the title centered horizontally



//// Draw news date (right-aligned)
//font = new XFont("Arial", 12, XFontStyle.Regular);
//            var dateText = $"Date: {newsDate}";
//            var dateSize = gfx.MeasureString(dateText, font);
//            gfx.DrawString(dateText, font, XBrushes.Black,
//                page.Width - dateSize.Width - 50, 20);

//            // Embed image (centered)
//            if (!string.IsNullOrEmpty(imageUrl) && System.IO.File.Exists(imageUrl))
//            {
//                try
//                {
//                    var webClient = new WebClient();
//                    byte[] imageBytes = webClient.DownloadData(imageUrl);

//                    XImage image = XImage.FromStream();
//                    double imageWidth = 200;
//                    double imageHeight = imageWidth * image.PixelHeight / image.PixelWidth;
//                    gfx.DrawImage(image, (page.Width - imageWidth) / 2, 100, imageWidth, imageHeight);
//                }
//                catch (Exception ex)
//                {
//                    Console.WriteLine($"Error loading image: {ex.Message}");
//                }
//            }
//            else
//            {
//                Console.WriteLine($"Image file not found: {imageUrl}");
//            }

//            // Draw description (justified with proper margin)
//            font = new XFont("Arial", 12, XFontStyle.Regular);
//            var descriptionRect = new XRect(50, 300, page.Width - 100, page.Height - 350);


//            var tital = new XRect(0, 20, page.Width, 50),
//           gfx.DrawString(title, font, XBrushes.Black,
//              // Adjust Y coordinate and height as needed
//              XStringFormats.Center);
//            // Draw the description with word wrapping
//            XTextFormatter tf = new XTextFormatter(gfx);
//            tf.DrawString(description, font, XBrushes.Black, descriptionRect, XStringFormats.TopLeft);
//            tf.DrawString(tital, font, XBrushes.Black, descriptionRect, XStringFormats.TopLeft);

//            // Save the PDF document
//            document.Save(pdfPath);
//        }

//        return Results.Ok($"PDF '{pdfFileName}' created and saved to D:\\TestPdf.");
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine($"Error creating PDF: {ex.Message}");
//        return Results.NotFound("PDF creation failed.");
//    }
//});
//app.MapPost("/createpdf", async (string title, string description, string newsDate, string imageUrl) =>
//{
//    try
//    {
//        // Step 1: Validate input (optional)
//        if (string.IsNullOrEmpty(title))
//        {
//            return Results.BadRequest("Title cannot be empty.");
//        }

//        // Step 2: Setup PDF file path and directory
//        var pdfFileName = $"{title}.pdf";
//        var pdfDirectory = @"D:\TestPdf";
//        var pdfPath = Path.Combine(pdfDirectory, pdfFileName);

//        // Ensure the directory exists; create if it doesn't
//        Directory.CreateDirectory(pdfDirectory);

//        // Step 3: Create the PDF document
//        using (PdfDocument document = new PdfDocument())
//        {
//            // Fonts setup
//            XFont titleFont = new XFont("Arial", 14, XFontStyle.BoldItalic);
//            XFont dateFont = new XFont("Arial", 12, XFontStyle.Regular);
//            XFont descriptionFont = new XFont("Arial", 10, XFontStyle.Regular);

//            // Split the description into paragraphs for easier handling
//            string[] paragraphs = description.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

//            foreach (string paragraph in paragraphs)
//            {
//                // Initialize variables for managing pages and content height
//                double contentHeight = 0;
//                bool newPageNeeded = false;

//                do
//                {
//                    // Add a new page to the document
//                    PdfPage page = document.AddPage();
//                    XGraphics gfx = XGraphics.FromPdfPage(page);

//                    // Calculate title size and position
//                    double titleX = 50;
//                    double titleY = 20;
//                    XRect titleRect = new XRect(titleX, titleY, page.Width - 100, page.Height);

//                    // Draw title centered horizontally with word wrapping
//                    XTextFormatter titleFormatter = new XTextFormatter(gfx);
//                    titleFormatter.DrawString(title, titleFont, XBrushes.Black, titleRect, XStringFormats.TopLeft);

//                    // Calculate date size and position
//                    string formattedDate = $"Date: {newsDate}";
//                    XSize dateSize = gfx.MeasureString(formattedDate, dateFont);
//                    double dateX = page.Width - dateSize.Width - 50;
//                    double dateY = titleRect.Top;

//                    // Draw news date (right-aligned next to the title)
//                    gfx.DrawString(formattedDate, dateFont, XBrushes.Black, new XPoint(dateX, dateY));

//                    // Embed image (centered)
//                    if (!string.IsNullOrEmpty(imageUrl))
//                    {
//                        try
//                        {
//                            using (WebClient webClient = new WebClient())
//                            {
//                                byte[] imageBytes = webClient.DownloadData(imageUrl);

//                                using (MemoryStream ms = new MemoryStream(imageBytes))
//                                {
//                                    // Create an XImage from the MemoryStream
//                                    XImage image = XImage.FromStream(() => ms);

//                                    double imageWidth = 200;
//                                    double imageHeight = imageWidth * image.PixelHeight / image.PixelWidth;
//                                    gfx.DrawImage(image, (page.Width - imageWidth) / 2, 100, imageWidth, imageHeight);
//                                }
//                            }
//                        }
//                        catch (Exception ex)
//                        {
//                            Console.WriteLine($"Error loading image: {ex.Message}");
//                        }
//                    }
//                    else
//                    {
//                        Console.WriteLine("Image URL is empty or invalid.");
//                    }

//                    // Draw description (justified with proper margin)
//                    XRect descriptionRect = new XRect(50, 300, page.Width - 100, page.Height - 350);
//                    XTextFormatter tf = new XTextFormatter(gfx);

//                    // Draw the paragraph
//                    tf.DrawString(paragraph, descriptionFont, XBrushes.Black, descriptionRect, XStringFormats.TopLeft);

//                    // Calculate the height of the drawn text
//                    double textHeight = descriptionFont.GetHeight();

//                    // Update content height and check if a new page is needed
//                    contentHeight += textHeight;
//                    newPageNeeded = contentHeight > (page.Height - 350); // 350 is the bottom margin

//                    // Save the page if it has content or if it's the first page
//                    if (contentHeight > 0 || document.Pages.Count == 1)
//                    {
//                        document.Save(pdfPath);
//                    }
//                }
//                while (newPageNeeded);
//            }
//        }

//        return Results.Ok($"PDF '{pdfFileName}' created and saved to D:\\TestPdf.");
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine($"Error creating PDF: {ex.Message}");
//        return Results.NotFound("PDF creation failed.");
//    }
//});

app.MapPost("/createpdf", async (string title, string date, string description, string imageUrl) =>
{
    // Validate input
    if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(date) || string.IsNullOrEmpty(description) || string.IsNullOrEmpty(imageUrl))
    {
        return Results.BadRequest("Title, date, description, and image URL are required.");
    }

    // Sanitize the title for the filename
    string sanitizedTitle = Regex.Replace(title, @"[<>:""/\\|?*\x00-\x1F]", "_");

    // Define the file path
    string filePath = Path.Combine("D:\\TestPdf", sanitizedTitle + ".pdf");

    // Create a PdfDocument instance
    PdfDocument pdf = new PdfDocument();

    // Add a page
    PdfPageBase page = pdf.Pages.Add();

    // Define page dimensions
    SizeF pageSize = page.Canvas.ClientSize;
    float pageWidth = pageSize.Width;
    float pageHeight = pageSize.Height;

    // Create a PdfFont instance for the title (bold and centered)
    PdfFont titleFont = new PdfFont(PdfFontFamily.Helvetica, 14, PdfFontStyle.Bold);
    PdfStringFormat titleFormat = new PdfStringFormat
    {
        Alignment = PdfTextAlignment.Center
    };

    // Draw the title (centered)
    RectangleF titleBounds = new RectangleF(new PointF(0, 10), new SizeF(pageWidth, 30));
    page.Canvas.DrawString(title, titleFont, PdfBrushes.Black, titleBounds, titleFormat);

    // Create a PdfFont instance for the date (right-aligned)
    PdfFont dateFont = new PdfFont(PdfFontFamily.Helvetica, 11);
    PdfStringFormat dateFormat = new PdfStringFormat
    {
        Alignment = PdfTextAlignment.Right
    };

    // Draw the date (right-aligned)
    RectangleF dateBounds = new RectangleF(new PointF(0, 50), new SizeF(pageWidth - 20, 30));
    page.Canvas.DrawString(date, dateFont, PdfBrushes.Black, dateBounds, dateFormat);

    // Download the image from the URL
    using (HttpClient httpClient = new HttpClient())
    {
        try
        {
            byte[] imageData = await httpClient.GetByteArrayAsync(imageUrl);
            using (MemoryStream imageStream = new MemoryStream(imageData))
            {
                // Load the image into a PdfImage object
                PdfImage image = PdfImage.FromStream(imageStream);

                // Define image bounds and center it on the page
                float imageWidth = 200; // Adjust width as needed
                float imageHeight = 150; // Adjust height as needed
                RectangleF imageBounds = new RectangleF(
                    (pageWidth - imageWidth) / 2,
                    100, // Position below the date
                    imageWidth,
                    imageHeight);

                // Draw the image onto the page
                page.Canvas.DrawImage(image, imageBounds);
            }
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to download or add image: {ex.Message}");
        }
    }

    // Create a PdfFont instance for the description
    PdfFont descriptionFont = new PdfFont(PdfFontFamily.Helvetica, 11);
    PdfStringFormat descriptionFormat = new PdfStringFormat
    {
        Alignment = PdfTextAlignment.Left,
        LineSpacing = 20f
    };

    // Draw the description below the image
    RectangleF descriptionBounds = new RectangleF(new PointF(10, 260), new SizeF(pageWidth - 20, pageHeight - 260));
    PdfTextWidget descriptionWidget = new PdfTextWidget(description, descriptionFont, PdfBrushes.Black)
    {
        StringFormat = descriptionFormat
    };
    descriptionWidget.Draw(page, descriptionBounds);

    // Save the PDF to the specified path
    try
    {
        pdf.SaveToFile(filePath, Spire.Pdf.FileFormat.PDF);
        return Results.Ok($"PDF successfully saved to {filePath}");
    }
    catch (Exception ex)
    {
        return Results.BadRequest($"Failed to save PDF: {ex.Message}");
    }
});
//app.MapPost("/createpdf", async (string title, string description, string newsDate, string imageUrl) =>
//{
//    try
//    {
//        // Validate input
//        if (string.IsNullOrEmpty(title))
//        {
//            return Results.BadRequest("Title cannot be empty.");
//        }

//        // Setup PDF file path and directory
//        var pdfFileName = $"{title}.pdf";
//        var pdfDirectory = @"D:\TestPdf";
//        var pdfPath = Path.Combine(pdfDirectory, pdfFileName);

//        // Ensure the directory exists; create if it doesn't
//        Directory.CreateDirectory(pdfDirectory);

//        // Create the PDF document
//        using (var document = new PdfDocument())
//        {
//            // Set up fonts
//            var titleFont = new PdfFont(PdfFontFamily.Helvetica, 14f, PdfFontStyle.Bold);
//            var dateFont = new PdfFont(PdfFontFamily.Helvetica, 12f);
//            var descriptionFont = new PdfFont(PdfFontFamily.Helvetica, 10f);

//            // Split the description into paragraphs
//            var paragraphs = description.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

//            var contentHeight = 0f;
//            var pageIndex = 0;

//            foreach (var paragraph in paragraphs)
//            {
//                var newPageNeeded = false;

//                do
//                {
//                    // Add a new page
//                    var page = document.Pages.Add();
//                    var pdfGraphics = page.Graphics;

//                    // Draw the title
//                    var titleRect = new RectangleF(50, 20, page.Size.Width - 100, 50);
//                    pdfGraphics.DrawString(title, titleFont, PdfBrushes.Black, titleRect, PdfStringFormat.Center);

//                    // Draw the date
//                    var formattedDate = $"Date: {newsDate}";
//                    var dateSize = pdfGraphics.MeasureString(formattedDate, dateFont);
//                    var dateX = page.Size.Width - dateSize.Width - 50;
//                    var dateY = titleRect.Top;
//                    pdfGraphics.DrawString(formattedDate, dateFont, PdfBrushes.Black, new PointF(dateX, dateY));

//                    // Draw the image
//                    if (!string.IsNullOrEmpty(imageUrl))
//                    {
//                        try
//                        {
//                            using var httpClient = new HttpClient();
//                            var imageBytes = await httpClient.GetByteArrayAsync(imageUrl);

//                            using var imageStream = new MemoryStream(imageBytes);
//                            var image = PdfImage.FromStream(imageStream);
//                            var imageWidth = 200f;
//                            var imageHeight = imageWidth * image.PixelHeight / image.PixelWidth;
//                            pdfGraphics.DrawImage(image, (page.Size.Width - imageWidth) / 2, 100, imageWidth, imageHeight);
//                        }
//                        catch (Exception ex)
//                        {
//                            Console.WriteLine($"Error loading image: {ex.Message}");
//                        }
//                    }

//                    // Draw the description
//                    var descriptionRect = new RectangleF(50, 300, page.Size.Width - 100, page.Size.Height - 350);
//                    var textFormatter = new PdfTextFormatter(pdfGraphics);
//                    textFormatter.DrawString(paragraph, descriptionFont, PdfBrushes.Black, descriptionRect);

//                    // Calculate content height
//                    contentHeight += descriptionFont.GetHeight();

//                    // Check if a new page is needed
//                    newPageNeeded = contentHeight > (page.Size.Height - 350); // 350 is the bottom margin

//                    if (newPageNeeded)
//                    {
//                        contentHeight = 0;
//                    }

//                    // Save the document if it has content
//                    if (document.Pages.Count > 0)
//                    {
//                        document.Save(pdfPath);
//                    }
//                }
//                while (newPageNeeded);
//            }
//        }

//        return Results.Ok($"PDF '{pdfFileName}' created and saved to D:\\TestPdf.");
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine($"Error creating PDF: {ex.Message}");
//        return Results.NotFound("PDF creation failed.");
//    }
//});

app.Run();
async Task<string> ScrapeArticleContentAsync(string url, string elementId, string elementClass)
{
    try
    {
        var web = new HtmlWeb();
        var doc = await web.LoadFromWebAsync(url);

        // Try to get the content by ID
        var contentNode = doc.DocumentNode.SelectSingleNode($"//*[@id='{elementId}']");

        // If not found by ID, try to get the content by Class
        if (contentNode == null)
        {
            contentNode = doc.DocumentNode.SelectSingleNode($"//*[@class='{elementClass}']");
        }

        if (contentNode == null)
        {
            throw new Exception("Content not found using provided ID or Class.");
        }

        // Clean up the content to remove newlines and extra whitespace
        string content = contentNode.InnerText.Trim();
        content = Regex.Replace(content, @"\s+", " "); // Replace multiple spaces with a single space

        return content;
    }
    catch (Exception ex)
    {
        throw new Exception($"Failed to scrape content from {url}: {ex.Message}");
    }
}

app.UseHttpsRedirection();





app.Run();


public record PdfRequest(string Title, string Date, string Description);
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Lexicon.Api.Controllers;

[Route("api/[controller]")]
public class FileController : Controller
{
	private readonly IWebHostEnvironment _environment;
	public FileController(IWebHostEnvironment environment)
	{
			_environment = environment;
		}
	[HttpPost("upload")]
	public async Task<IActionResult> PostFileAsync([FromForm] FileUploadWeb files)
	{
			var uploadPath = Path.Combine(_environment.ContentRootPath, "Uploads");
			if (!Directory.Exists(uploadPath))
			{
				Directory.CreateDirectory(uploadPath);
			}
			try
			{
				foreach (var file in files.Attachments)
				{
					// Save locally
					string safeFileName = WebUtility.HtmlEncode(file.FileName);
					var path = Path.Combine(uploadPath, safeFileName); //can save this info in database together with file title and description
					await using FileStream fs = new(path, FileMode.Create);
					await file.CopyToAsync(fs);
				}
				return Ok(new { Message = "Upload Successful!" });
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { Message = $"Upload failed: {ex.Message}" });
			}
		}
}
public class FileUploadWeb()
{
	public string Title { get; set; }
	public string Description { get; set; }
	public string UserId { get; set; }
	public string? ModuleId { get; set; }
	public IFormFileCollection Attachments { get; set; }
}
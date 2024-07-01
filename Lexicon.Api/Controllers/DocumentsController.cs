using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lexicon.Api.Entities;
using AutoMapper;
using Lexicon.Api.Repositories;
using Lexicon.Api.Dtos.DocumentDtos;
using System.Net;

namespace Lexicon.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DocumentsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _environment;

    public DocumentsController(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment environment)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _environment = environment;
    }

    [HttpGet]
    public async Task<IActionResult> GetDocuments()
    {
        var documents = await _unitOfWork.Documents.GetAllAsync();

        if (!documents.Any() || documents == null)
        {
            return BadRequest();
        }

        return Ok(_mapper.Map<IEnumerable<DocumentDto>>(documents));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DocumentDto>> GetDocument([FromRoute] int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        try
        {
            var document = await _unitOfWork.Documents.GetAsync(id);

            if (document == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<DocumentDto>(document));
        }

        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }

        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutDocument([FromRoute] int id, [FromBody] DocumentPostDto documentPost)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (id <= 0)
        {
            return BadRequest();
        }

        try
        {
            var existingDocument = await _unitOfWork.Documents.GetAsync(id);
            _mapper.Map(documentPost, existingDocument);

            _unitOfWork.Documents.Update(existingDocument);
            await _unitOfWork.SaveAsync();
        }

        catch (DbUpdateConcurrencyException)
        {
            if (!await DocumentExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }

        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }

        return NoContent();
    }

    [HttpPost("upload")]
    public async Task<IActionResult> PostDocument([FromForm] FileUploadWeb files)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        //var document = _mapper.Map<Document>(documentPostDto);
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
                // Construct the HTTP URL
                var fileUrl = $"{Request.Scheme}://{Request.Host}/uploads/{safeFileName}";
                var documentPostDto = new DocumentPostDto
                {
                    Name = safeFileName,
                    Description = files.Description,
                    Url = fileUrl,
                    UserId = Int32.Parse(files.UserId),
                    TimeAdded = DateTime.Now
                };
                var document = _mapper.Map<Document>(documentPostDto);
                try
                {
                    _unitOfWork.Documents.Add(document);
                    await _unitOfWork.SaveAsync();
                }
                catch (InvalidOperationException ex)
                {
                    return NotFound(ex.Message);
                }
                await using FileStream fs = new(path, FileMode.Create);
                await file.CopyToAsync(fs);
            }
            return Ok(new { Message = "Upload Successful!" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = $"Upload failed: {ex.Message}" });
        }


        //  return CreatedAtAction("GetDocument", new { id = document.DocumentId }, document);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDocument([FromRoute] int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        try
        {
            _unitOfWork.Documents.Delete(id);
            await _unitOfWork.SaveAsync();
        }

        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }

        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }

        return NoContent();
    }

    private async Task<bool> DocumentExists(int id) => await _unitOfWork.Documents.GetAsync(id) != null;


}


using AutoMapper;
using Core.POCO;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web.Requests;

namespace Web.Controllers
{
  [Produces("application/json")]
  [Route("api/[controller]")]
  [ApiController]
  public class CommentController : ControllerBase
  {
    private readonly IRepository<Comment> _repository;
    private readonly IMapper _mapper;

    public CommentController(IRepository<Comment> repository, IMapper mapper)
    {
      _repository = repository;
      _mapper = mapper;
    }

    /// <summary>
    /// Return all airports from the database.
    /// </summary>
    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> Get()
    {
      var comments = await _repository.GetAll();
      return Ok(comments);
    }

    /// <summary>
    /// Searches for airport matching the code.
    /// </summary>
    [HttpGet]
    [Route("by-id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetById(string id)
    {
      var comment = await _repository.GetById(id);
      return Ok(comment);
    }

    [HttpPost]
    [Route("add-comment")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddComment(CommentRequest request)
    {
      var resources = _mapper.Map<CommentRequest, Comment>(request);
      await _repository.Insert(resources);
      return Ok();
    }

    [HttpDelete]
    [Route("delete-comment")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteComment(string id)
    {
      await _repository.Delete(id);
      return Ok();
    }

    [HttpPut]
    [Route("update-comment")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateComment(CommentRequest request)
    {
      var resources = _mapper.Map<CommentRequest, Comment>(request);
      await _repository.Update(resources);
      return Ok();
    }
  }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ToDo.Application.Contracts;
using ToDo.Application.DTO.ViewModel;
using ToDo.Application.DTOs.InputModel;
using ToDo.Application.Notification;


namespace ToDo.API.Controllers;

[Authorize]
[Route("assignment")]
public class AssignmentController : MainController
{
    private readonly IAssignmentService _assignmentService;

    public AssignmentController(INotificator notificator, IAssignmentService assignmentService) : base(notificator)
    {
        _assignmentService = assignmentService;
    }
    
    [HttpPost]
    [SwaggerOperation("Create a task")]
    [ProducesResponseType(typeof(AssignmentViewModel), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] AddAssignmentInputModel inputModel)
    {
        var createAssignment = await _assignmentService.CreateAsync(inputModel);
        return CreatedResponse("", createAssignment);
    }
    
    [HttpPut("{id}")]
    [SwaggerOperation("Update a task")]
    [ProducesResponseType(typeof(AssignmentViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateAssignmentInputModel inputModel)
    {
        var updateAssignment = await _assignmentService.UpdateAsync(id, inputModel);
        return OkResponse(updateAssignment);
    }
    
    [HttpDelete("{id}")]
    [SwaggerOperation("Delete a task")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        await _assignmentService.DeleteAsync(id);
        return NoContentResponse();
    }
    
    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "GetById a task")]
    [ProducesResponseType(typeof(AssignmentViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var getAssignment = await _assignmentService.GetByIdAsync(id);
        return OkResponse(getAssignment);
    }

    [HttpGet]
    [SwaggerOperation("Search tasks")]
    [ProducesResponseType(typeof(PagedViewModel<AssignmentViewModel>), StatusCodes.Status200OK)]
    public async Task<PagedViewModel<AssignmentViewModel>> Search([FromQuery] AssignmentSearchInputModel inputModel)
    {
        return await _assignmentService.SearchAsync(inputModel);
    }
    
    [HttpPatch("{id}/conclude")]
    [SwaggerOperation("Conclud a task")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Conclude(int id)
    {
        await _assignmentService.MarkConcludedAsync(id);
        return NoContentResponse();
    }

    [HttpPatch("{id}/unconclude")]
    [SwaggerOperation("Desconclud a task")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Unconclude(int id)
    { 
        await _assignmentService.MarkDesconcludedAsync(id);
        return NoContentResponse();
    }
}
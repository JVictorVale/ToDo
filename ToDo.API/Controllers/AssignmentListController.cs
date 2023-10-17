using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ToDo.API.Responses;
using ToDo.Application.Contracts;
using ToDo.Application.DTO.ViewModel;
using ToDo.Application.DTOs.InputModel;
using ToDo.Application.DTOs.ViewModel;
using ToDo.Application.Notification;


namespace ToDo.API.Controllers;

[Authorize]
[Route("assignmentList")]
public class AssignmentListController : MainController
{ 
    private readonly IAssignmentListService _assignmentListService;

    public AssignmentListController(INotificator notificator, IAssignmentListService assignmentListService) : base(notificator)
    {
        _assignmentListService = assignmentListService;
    }
    
    [HttpPost]
    [SwaggerOperation(Summary = "Create a to-do list")]
    [ProducesResponseType(typeof(AssignmentListViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create([FromBody] AddAssignmentListInputModel inputModel)
    {
        var createAssignmentList = await _assignmentListService.Create(inputModel);
        return CreatedResponse("", createAssignmentList);
    }

    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Update a to-do list")]
    [ProducesResponseType(typeof(AssignmentListViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateAssignmentListInputModel inputModel)
    {
        var updateAssignmentList = await _assignmentListService.Update(id, inputModel);
        return OkResponse(updateAssignmentList);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation("Delete a to-do list")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(NotFoundResult) ,StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        await _assignmentListService.Delete(id);
        return NoContentResponse();
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "GetById a to-do list")]
    [ProducesResponseType(typeof(AssignmentListViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var getAssignmentList = await _assignmentListService.GetById(id);
        return OkResponse(getAssignmentList);
    }
    
    [HttpGet]
    [SwaggerOperation("Search to-do lists")]
    [ProducesResponseType(typeof(PagedViewModel<AssignmentListViewModel>), StatusCodes.Status200OK)]
    public async Task<PagedViewModel<AssignmentListViewModel>> Search([FromQuery] AssignmentListSearchInputModel inputModel)
    {
        return await _assignmentListService.Search(inputModel);
    }
    
    [HttpGet("{id}/assignments")]
    [SwaggerOperation("Search for tasks in a to-do list")]
    [ProducesResponseType(typeof(IEnumerable<AssignmentViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SearchAssignments(int id, [FromQuery] AssignmentSearchInputModel inputModel)
    {
        var getAssignment = await _assignmentListService.SearchAssignments(id, inputModel);
        return OkResponse(getAssignment);
    }
}
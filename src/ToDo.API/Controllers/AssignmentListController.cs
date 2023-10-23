using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ToDo.API.Responses;
using ToDo.Application.Contracts;
using ToDo.Application.DTO.ViewModel;
using ToDo.Application.DTOs.Assignment;
using ToDo.Application.DTOs.AssignmentList;
using ToDo.Application.DTOs.Paged;
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
    [ProducesResponseType(typeof(AssignmentListDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create([FromBody] AddAssignmentListDto dto)
    {
        var createAssignmentList = await _assignmentListService.CreateAsync(dto);
        return CreatedResponse("", createAssignmentList);
    }

    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Update a to-do list")]
    [ProducesResponseType(typeof(AssignmentListDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateAssignmentListDto dto)
    {
        var updateAssignmentList = await _assignmentListService.UpdateAsync(id, dto);
        return OkResponse(updateAssignmentList);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation("Delete a to-do list")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(NotFoundResult) ,StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        await _assignmentListService.DeleteAsync(id);
        return NoContentResponse();
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "GetById a to-do list")]
    [ProducesResponseType(typeof(AssignmentListDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var getAssignmentList = await _assignmentListService.GetByIdAsync(id);
        return OkResponse(getAssignmentList);
    }
    
    [HttpGet]
    [SwaggerOperation("Search to-do lists")]
    [ProducesResponseType(typeof(PagedDto<AssignmentListDto>), StatusCodes.Status200OK)]
    public async Task<PagedDto<AssignmentListDto>> Search([FromQuery] AssignmentListSearchDto dto)
    {
        return await _assignmentListService.SearchAsync(dto);
    }
    
    [HttpGet("{id}/assignments")]
    [SwaggerOperation("Search for tasks in a to-do list")]
    [ProducesResponseType(typeof(IEnumerable<AssignmentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SearchAssignments(int id, [FromQuery] AssignmentSearchDto dto)
    {
        var getAssignment = await _assignmentListService.SearchAssignmentsAsync(id, dto);
        return OkResponse(getAssignment);
    }
}
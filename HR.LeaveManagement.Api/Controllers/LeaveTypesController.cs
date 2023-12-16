using HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveTypesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/LeaveTypes
        [HttpGet]
        public async Task<ActionResult<List<LeaveTypeDto>>> Get()
        {
            var leaveTypes = await _mediator.Send(new GetLeaveTypesQuery());
            return leaveTypes;
        }

        // GET: api/LeaveTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveTypeDetailDto>> Get(int id)
        {
            var leaveType = await _mediator.Send(new GetLeaveTypesDetailsQuery(id));
            return Ok(leaveType);
        }

        // POST: api/LeaveTypes
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateLeaveTypeCommand leaveTypeDto)
        {
            var result = await _mediator.Send(leaveTypeDto);
            return CreatedAtAction(nameof(Get), new { Id = result});
        }

        // PUT: api/LeaveTypes/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(UpdateLeaveTypeCommand updateLeaveTypeCommand)
        {
            await _mediator.Send(updateLeaveTypeCommand);
            return NoContent();
        }

        // DELETE: api/LeaveTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteLeaveTypeCommand()
            {
                Id = id
            });

            return NoContent();
        }
    }
}

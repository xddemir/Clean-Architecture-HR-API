using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]
    public class LeaveAllocationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveAllocationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/LeaveAllocations
        [HttpGet]
        public async Task<ActionResult<List<LeaveAllocationDto>>> Get()
        {
            var leaveAllocations = await _mediator.Send(new GetLeaveAllocationListQuery());
            return Ok(leaveAllocations);
        }

        // GET: api/LeaveAllocations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveAllocationDto>> Get(int id)
        {
            var leaveAllocation = await _mediator.Send(new GetLeaveAllocationDetailQuery(id));
            return Ok(leaveAllocation);
        }

        // POST: api/LeaveAllocations
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] int leaveTypeId)
        {
            await _mediator.Send(new CreateLeaveAllocationCommand()
            {
                LeaveTypeId = leaveTypeId
            });
            
            return NoContent();
        }

        // PUT: api/LeaveAllocations/5
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UpdateLeaveAllocationCommand updateLeaveAllocationCommand)
        {
            await _mediator.Send(updateLeaveAllocationCommand);
            return NoContent();
        }

        // DELETE: api/LeaveAllocations/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteLeaveAllocationCommand(id));
            return NoContent();
        }
    }
}

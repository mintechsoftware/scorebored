using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scorebored.API.Models;
using Scorebored.API.Requests;
using Scorebored.API.Responses;
using Scorebored.API.Services;

namespace Scorebored.API.Controllers
{
    [Authorize]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private IGroupService _groupService;
        private IUserService _userService;

        public GroupsController(IGroupService groupService, IUserService userService)
        {
            _groupService = groupService;
            _userService = userService;
        }

        [HttpPost("/group/create")]
        public IActionResult CreateGroup([FromBody]CreateGroupRequest request)
        {
            var group = _groupService.CreateGroup(request.Name);

            if (group == null)
            {
                return Conflict("Group already exists");
            }

            long.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId);

            _groupService.CreateGroupMembership(group.GroupId, userId);
            _userService.UpdateUserCurrentGroup(userId, group.GroupId);
            return Ok(group);
        }

        [HttpGet("/user/groups/{userId}")]
        public ActionResult<GetUserGroupsResponse> GetUserGroups(long userId) => new GetUserGroupsResponse
        {
            Groups = _groupService.GetUserGroups(userId)
        };

        [HttpPost("/user/groups/current_group_id")]
        public IActionResult UpdateUserCurrentGroupId(long userId, long groupId)
        {
            _groupService.UpdateUserCurrentGroupId(userId, groupId);
            return Ok(groupId);
        }

        [HttpGet("/groups/access_code/{userId}/{groupId}")]
        public GroupInvitation GetGroupInvitation(long userId, long groupId) => _groupService.CreateNewGroupInvitation(userId, groupId);

        [HttpPost("/groups/join/{userId}")]
        public ActionResult<Group> JoinGroup(long userId, JoinGroupRequest request)
        {
            if (_groupService.InvitationHasBeenUsed(request.Code))
            {
                return Conflict("Invitation has already been used");
            }

            if (_groupService.UserIsAlreadyInGroup(userId, request.Code))
            {
                return Conflict("User is already in group");
            }

            var group = _groupService.JoinGroup(userId, request.Code);

            if (group == null)
            {
                return BadRequest();
            }

            return group;
        }
    }
}

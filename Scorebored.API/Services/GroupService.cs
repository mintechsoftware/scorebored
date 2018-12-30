using System;
using System.Collections.Generic;
using System.Linq;
using Scorebored.API.Models;

namespace Scorebored.API.Services
{
    public interface IGroupService
    {
        Group CreateGroup(string name);
        IEnumerable<Group> GetUserGroups(long userId);
        GroupMembership CreateGroupMembership(long groupId, long userId);
        void UpdateUserCurrentGroupId(long userId, long groupId);
        GroupInvitation CreateNewGroupInvitation(long userId, long groupId);
        Group JoinGroup(long userId, string code);
        bool InvitationHasBeenUsed(string code);
        bool UserIsAlreadyInGroup(long userId, string code);
    }

    public class GroupService : IGroupService
    {
        private readonly ScoreboredContext _dbContext;

        public GroupService(ScoreboredContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Group CreateGroup(string name)
        {
            var groupCount = _dbContext.Groups.Where(group => group.Name == name).Count();

            if (groupCount > 0)
            {
                return null;
            }

            var newGroup = new Group
            {
                Name = name
            };

            _dbContext.Add(newGroup);
            _dbContext.SaveChanges();

            return newGroup;
        }

        public GroupMembership CreateGroupMembership(long groupId, long userId)
        {
            var newGroupMembership = new GroupMembership
            {
                GroupId = groupId,
                UserId = userId
            };

            _dbContext.Add(newGroupMembership);
            _dbContext.SaveChanges();

            return newGroupMembership;
        }

        public IEnumerable<Group> GetUserGroups(long userId)
        {
            var groupIds = _dbContext.GroupMemberships
            .Where(gm => gm.UserId == userId)
            .Select(gm => gm.GroupId);

            return _dbContext.Groups.Where(g => groupIds.Contains(g.GroupId));
        }

        public void UpdateUserCurrentGroupId(long userId, long groupId)
        {
            var user = _dbContext.Users.Find(userId);
            user.CurrentGroupId = groupId;
            _dbContext.SaveChanges();
        }

        public GroupInvitation CreateNewGroupInvitation(long userId, long groupId)
        {
            var code = GenerateInvitationCode();

            while (_dbContext.GroupInvitations.Where(gi => gi.Code == code).FirstOrDefault() != null)
            {
                code = GenerateInvitationCode();
            }

            var newGroupInvitation = new GroupInvitation
            {
                GroupId = groupId,
                RequestedById = userId,
                Code = code,
                CreatedAt = DateTime.Now
            };

            _dbContext.Add(newGroupInvitation);
            _dbContext.SaveChanges();

            return newGroupInvitation;
        }

        private string GenerateInvitationCode()
        {
            const int length = 8;
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)])
              .ToArray());
        }

        public Group JoinGroup(long userId, string code)
        {
            var groupInvitation = _dbContext.GroupInvitations.Where(gi => gi.Code == code && gi.Accepted == false).FirstOrDefault();

            if (groupInvitation == null)
            {
                return null;
            }

            var newGroupMembership = new GroupMembership
            {
                GroupId = groupInvitation.GroupId,
                UserId = userId
            };

            groupInvitation.Accepted = true;
            _dbContext.GroupMemberships.Add(newGroupMembership);
            _dbContext.SaveChanges();

            return _dbContext.Groups.Where(g => g.GroupId == newGroupMembership.GroupId).FirstOrDefault();
        }

        public bool InvitationHasBeenUsed(string code)
        {
            return _dbContext.GroupInvitations.Where(gi => gi.Code == code && gi.Accepted).Any();
        }

        public bool UserIsAlreadyInGroup(long userId, string code)
        {
            var groupInvitation = _dbContext.GroupInvitations.Where(gi => gi.Code == code).FirstOrDefault();

            if (groupInvitation == null)
            {
                return false;
            }

            return _dbContext.GroupMemberships.Where(gm => gm.UserId == userId && gm.GroupId == groupInvitation.GroupId).Any();
        }
    }
}

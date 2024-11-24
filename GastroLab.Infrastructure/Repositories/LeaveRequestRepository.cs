using GastroLab.Application.Interfaces;
using GastroLab.Domain.DBO;
using GastroLab.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Infrastructure.Repositories
{
    public class LeaveRequestRepository : ILeaveRequestRepository
    {
        private readonly GastroLabDbContext _context;

        public LeaveRequestRepository(GastroLabDbContext context)
        {
            _context = context;
        }

        public void DeleteLeaveRequest(int id)
        {
            var leaveRequest = _context.LeaveRequests.FirstOrDefault(x => x.Id == id);

            if (leaveRequest == null)
            {
                throw new Exception("Leave request not found");
            }

            _context.LeaveRequests.Remove(leaveRequest);
            _context.SaveChanges();
            
        }

        public void UpdateLeaveRequest(LeaveRequest leaveRequest)
        {
            _context.LeaveRequests.Update(leaveRequest);
            _context.SaveChanges();
        }

        public LeaveRequest GetLeaveRequestById(int leaveRequestId)
        {
            return _context.LeaveRequests.Include(x => x.User).FirstOrDefault(x => x.Id == leaveRequestId);
        }

        public List<LeaveRequest> GetPendingLeaveRequests()
        {
            return _context.LeaveRequests.Where(x => x.Status == LeaveRequestStatus.Pending)
                .Include(x => x.User)
                .ToList();
        }
        public List<LeaveRequest> GetResolvedLeaveRequests()
        {
            return _context.LeaveRequests.Where(x => x.Status != LeaveRequestStatus.Pending)
                .Include(x => x.User)
                .ToList();
        }

        public void AddLeaveRequest(LeaveRequest leaveRequest)
        {
            _context.LeaveRequests.Add(leaveRequest);
            _context.SaveChanges();
        }

        public List<LeaveRequest> GetPendingLeaveRequestsByUserId(string? userId)
        {
            return _context.LeaveRequests.Where(x => x.UserId == userId && x.Status == LeaveRequestStatus.Pending)
                .Include(x => x.User)
                .ToList();
        }
        public List<LeaveRequest> GetResolvedLeaveRequestsByUserId(string? userId)
        {
            return _context.LeaveRequests.Where(x => x.UserId == userId && x.Status != LeaveRequestStatus.Pending)
                .Include(x => x.User)
                .ToList();
        }
    }
}

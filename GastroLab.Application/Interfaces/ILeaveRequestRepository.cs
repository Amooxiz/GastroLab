using GastroLab.Domain.DBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Application.Interfaces
{
    public interface ILeaveRequestRepository
    {
        public void AddLeaveRequest(LeaveRequest leaveRequest);
        public List<LeaveRequest> GetPendingLeaveRequests();
        public List<LeaveRequest> GetResolvedLeaveRequests();
        public List<LeaveRequest> GetPendingLeaveRequestsByUserId(string? userId);
        public List<LeaveRequest> GetResolvedLeaveRequestsByUserId(string? userId);
        public LeaveRequest GetLeaveRequestById(int leaveRequestId);
        public void UpdateLeaveRequest(LeaveRequest leaveRequest);
        public void DeleteLeaveRequest(int id);
    }
}

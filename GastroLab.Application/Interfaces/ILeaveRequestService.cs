using GastroLab.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Application.Interfaces
{
    public interface ILeaveRequestService
    {
        public void AddLeaveRequest(LeaveRequestVM leaveRequestVM);
        public LeaveRequestVM GetLeaveRequestById(int leaveRequestId);
        public List<LeaveRequestVM> GetPendingLeaveRequests();
        public List<LeaveRequestVM> GetPendingLeaveRequestsByUserId(string? userId);
        public List<LeaveRequestVM> GetResolvedLeaveRequests();
        public List<LeaveRequestVM> GetResolvedLeaveRequestsByUserId(string? userId);
        public void UpdateLeaveRequest(LeaveRequestVM leaveRequestVM);
        public void DeleteLeaveRequest(int id);
    }
}

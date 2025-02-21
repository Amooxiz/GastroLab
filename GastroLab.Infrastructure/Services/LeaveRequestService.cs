using GastroLab.Application.Interfaces;
using GastroLab.Application.ViewModels;
using GastroLab.Application.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Infrastructure.Services
{
    public class LeaveRequestService : ILeaveRequestService
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;

        public LeaveRequestService(ILeaveRequestRepository leaveRequestRepository)
        {
            _leaveRequestRepository = leaveRequestRepository;
        }

        public void DeleteLeaveRequest(int id)
        {
            _leaveRequestRepository.DeleteLeaveRequest(id);
        }

        public void UpdateLeaveRequest(LeaveRequestVM leaveRequestVM)
        {
            _leaveRequestRepository.UpdateLeaveRequest(leaveRequestVM.ToModel());
        }
        public LeaveRequestVM GetLeaveRequestById(int leaveRequestId)
        {
            return _leaveRequestRepository.GetLeaveRequestById(leaveRequestId).ToVM();
        }

        public List<LeaveRequestVM> GetPendingLeaveRequests()
        {
            return _leaveRequestRepository.GetPendingLeaveRequests().Select(x => x.ToVM()).ToList();
        }


        public List<LeaveRequestVM> GetResolvedLeaveRequests()
        {
            return _leaveRequestRepository.GetResolvedLeaveRequests().Select(x => x.ToVM()).ToList();
        }

        public void AddLeaveRequest(LeaveRequestVM leaveRequestVM)
        {
            _leaveRequestRepository.AddLeaveRequest(leaveRequestVM.ToModel());
        }

        public List<LeaveRequestVM> GetPendingLeaveRequestsByUserId(string? userId)
        {
            return _leaveRequestRepository.GetPendingLeaveRequestsByUserId(userId).Select(x => x.ToVM()).ToList();
        }
        public List<LeaveRequestVM> GetResolvedLeaveRequestsByUserId(string? userId)
        {
            return _leaveRequestRepository.GetResolvedLeaveRequestsByUserId(userId).Select(x => x.ToVM()).ToList();
        }
    }
}

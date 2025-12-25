using TaskHub.API.DTOs.Dashboard;

namespace TaskHub.API.Services.Interfaces
{
    public interface IDashboardService
    {
        DashboardDto GetAdminDashboard();
        DashboardDto GetUserDashboard(int userId);
    }
}


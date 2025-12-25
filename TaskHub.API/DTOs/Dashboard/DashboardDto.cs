namespace TaskHub.API.DTOs.Dashboard
{
    public class DashboardDto
    {
        public int TotalTasks { get; set; }
        public int CompletedTasks { get; set; }
        public int PendingTasks { get; set; }
        public int InProgressTasks { get; set; }
        public int CancelledTasks { get; set; }
        public int? TotalUsers { get; set; } // Only for Admin
    }
}


using OrchidPharmedApi.Entities;

namespace OrchidPharmedApi.Core.DTOs
{
    public class TaskEntityDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public TaskEntityStatus Status { get; set; }
    }
}

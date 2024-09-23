namespace OrchidPharmedApi.Entities
{
    public class TaskEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public TaskEntityStatus Status { get; set; }
        public int ProjectId { get; set; }
        public ProjectEntity Project { get; set; }
    }

    public enum TaskEntityStatus
    {
        ToDo,
        InProgress,
        Done
    }
}

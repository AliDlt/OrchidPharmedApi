namespace OrchidPharmedApi.Entities
{
    public class ProjectEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<TaskEntity> TaskEntities { get; set; } = new();
    }
}

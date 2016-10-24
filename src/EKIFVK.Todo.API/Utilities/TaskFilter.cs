namespace EKIFVK.Todo.API.Utilities
{
    public class TaskFilter
    {
        public int Skip { get; set; }
        public int Count { get; set; }
        public bool OnlyFinished { get; set; }
        public bool OnlyUnfinish { get; set; }
        public bool OnlyScheduled { get; set; }
        public bool OnlyNoSchedule { get; set; }

    }
}

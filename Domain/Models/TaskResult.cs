namespace Domain.Models
{
    public class TaskResult<T>
    {
        public T Model { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}

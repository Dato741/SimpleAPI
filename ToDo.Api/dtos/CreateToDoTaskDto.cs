using ToDo.Api.Entities;
using ToDo.Api.Data;

namespace ToDo.Api.dtos
{
    public class CreateToDoTaskDto
    {
        /// <summary>
        /// Name as a string
        /// </summary>
        /// <example>API Project</example>
        public required string Name { get; set; }

        /// <summary>
        /// Status as a bool - true/false
        /// </summary>
        public bool IsCompleted { get; set; }

        /// <summary>
        /// 1 - High , 2 - Medium , 3 - Low  || May be represented as string as well as their corresponding numbers
        /// </summary>
        /// <example>1</example>
        public EPriority Priority { get; set; }

        /// <summary>
        /// year/month/day
        /// </summary>
        public DateOnly DueDate { get; set; }
    }
}

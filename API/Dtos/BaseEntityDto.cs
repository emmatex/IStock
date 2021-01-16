using System;

namespace API.Dtos
{
    public class BaseEntityDto
    {
        public string Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
    }
}

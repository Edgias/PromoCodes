using System.Collections.Generic;

namespace TheRoom.PromoCodes.API.Models.Responses
{
    public class PaginatedResponse<T>
    {
        public int Total { get; set; }

        public IEnumerable<T> Data { get; set; }

        public PaginatedResponse()
        {
            Data = new List<T>();
        }
    }
}

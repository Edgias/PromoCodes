using TheRoom.PromoCodes.ApplicationCore.Entities;

namespace TheRoom.PromoCodes.ApplicationCore.Specifications
{
    public class ServiceSpecification : BaseSpecification<Service>
    {
        /// <summary>
        /// Filter services by name
        /// </summary>
        /// <param name="searchQuery">of the service(s) you want to search</param>
        public ServiceSpecification(string searchQuery)
            : base(s => string.IsNullOrEmpty(searchQuery) || s.Description.Contains(searchQuery))
        {
        }


        /// <summary>
        /// Filter services by name
        /// </summary>
        /// <param name="skip">Number of items to skip for server side pagination</param>
        /// <param name="take">Number of items to return for server side pagination</param>
        /// <param name="searchQuery">of the service(s) you want to search</param>
        public ServiceSpecification(int skip, int take, string searchQuery) 
            : base(s => string.IsNullOrEmpty(searchQuery) || s.Description.Contains(searchQuery))
        {
            ApplyPaging(skip, take);
        }
    }
}

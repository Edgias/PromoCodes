using TheRoom.PromoCodes.ApplicationCore.SharedKernel;

namespace TheRoom.PromoCodes.ApplicationCore.Entities
{
    public class Service : BaseEntity
    {
        public string Description { get; private set; }

        public Service(string description)
        {
            Guard.AgainstNullOrEmpty(description, nameof(description));

            Description = description;
        }

        public void UpdateDetails(string description)
        {
            Guard.AgainstNullOrEmpty(description, nameof(description));

            Description = description;
        }

    }
}

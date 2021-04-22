namespace TheRoom.PromoCodes.ApplicationCore.Entities
{
    public class Service : BaseEntity
    {
        public string Description { get; private set; }

        public Service(string description)
        {
            Description = description;
        }

        public void UpdateDetails(string description)
        {
            Description = description;
        }

    }
}

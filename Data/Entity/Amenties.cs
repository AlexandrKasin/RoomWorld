namespace Data.Entity
{
    public class Amenties : BaseEntity
    {
        public string Name { get; set; }
        public bool Availability { get; set; }
        public int Amount { get; set; }
        public string Icon { get; set; }
        public Flat Flat { get; set; } = new Flat();
    }
}
namespace Data.Entity
{
    public class Extras : BaseEntity
    {
        public string Name { get; set; }
        public string Cost { get; set; }
        public Flat Flat { get; set; } = new Flat();
    }
}

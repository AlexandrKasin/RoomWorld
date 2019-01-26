namespace Data.Entity
{
    public class Message : BaseEntity
    {
        public string Text { get; set; }

       public User UserFrom { get; set; }
        public string UsernameTo { get; set; }
   
        public Dialog Dialog { get; set; }
    }
}
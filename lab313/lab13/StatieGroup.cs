namespace CSlab13
{
    public class StatieGroup
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public int MagazineId { get; set; }
        public Magazine Magazine { get; set; }
        public int StatieId { get; set; }
        public Statie Statie { get; set; }
        
    }
}
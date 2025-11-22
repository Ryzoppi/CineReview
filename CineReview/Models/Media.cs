namespace CineReview.Models
{
    public abstract class Media
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Synopsis { get; set; }
        public string Director { get; set; }
        public int ReleaseYear { get; set; }

        public ICollection<Review>? Reviews { get; set; }
    }
}

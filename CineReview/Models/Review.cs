namespace CineReview.Models
{
    public class Review
    {
        public int Grade { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }
        public Media Media { get; set; }
        public int MediaId { get; set; }
    }
}

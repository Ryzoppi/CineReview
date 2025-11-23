namespace CineReview.DTOs
{
    public class SerieCreateDto
    {
        public string Name { get; set; }
        public string Synopsis { get; set; }
        public string Director { get; set; }
        public int ReleaseYear { get; set; }
        public int Seasons { get; set; }
        public int Episodes { get; set; }
    }
}

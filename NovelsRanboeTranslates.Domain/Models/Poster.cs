namespace NovelsRanboeTranslates.Domain.Models;

public class Poster
{
    public int _id { get; set; }
    public string Image { get; set; }

    public Poster(int id, string image)
    {
        _id = id;
        Image = image;
    }
}
namespace Wypozyczalnia.Models.ViewModels;

public class AuthorViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }

    public Author ConvertToModel()
    {
        return new Author
        {
            Id = Id,
            Name = Name,
            LastName = LastName
        };
    }

    public static AuthorViewModel ConvertToViewModel(Author author)
    {
        return new AuthorViewModel
        {
            Id = author.Id,
            Name = author.Name,
            LastName = author.LastName
        };
    }
}
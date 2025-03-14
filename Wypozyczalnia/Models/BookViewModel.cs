using Microsoft.AspNetCore.Mvc.Rendering;

namespace Wypozyczalnia.Models;

public class BookViewModel
{
    public string Title { get; set; }
    public int Pages { get; set; }
    public bool IsBorrowed { get; set; } = false;

    // Lista ID wybranych autorów (z formularza)
    public List<int> SelectedAuthors { get; set; } = new List<int>();

    // Lista autorów do wyświetlenia w widoku
    public List<SelectListItem> AvailableAuthors { get; set; } = new List<SelectListItem>();
}
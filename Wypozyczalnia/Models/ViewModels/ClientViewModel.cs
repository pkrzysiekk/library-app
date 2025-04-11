namespace Wypozyczalnia.Models.ViewModels;

public class ClientViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }

    public Client ConvertToModel()
    {
        return new Client
        {
            Id = Id,
            Name = Name,
            LastName = LastName
        };
    }

    public static ClientViewModel ConvertToViewModel(Client client)
    {
        return new ClientViewModel
        {
            Id = client.Id,
            Name = client.Name,
            LastName = client.LastName
        };
    }
}
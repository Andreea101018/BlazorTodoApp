using System.Text.Json;
using Domain.Models;

namespace FileData;

public class FileContext
{
    private const string filePath = "data.json";
    private DataContainer? dataContainer; //Notice the variable is nullable, marked with the "?", indicating we allow this field to be null. 

    public ICollection<Todo> Todos
    {
        get
        {
            LoadData();
            return dataContainer!.Todos;
        }
    }

    public ICollection<User> Users
    {
        get
        {
            LoadData();
            return dataContainer!.Users;
        }
    }

    private void LoadData() //The method is private, because this class should be responsible for determining when to load data. No outside class should tell this class to load data.
    {
        if (dataContainer != null) return; //First we check if the data is already loaded, and if so, we return.

        if (!File.Exists(filePath)) //Then we check if there is a file, and if not, we just create a new "empty" DataContainer.
        {
            dataContainer = new()
            {
                Todos = new List<Todo>(),
                Users = new List<User>()
            };
            
            return;
        }
        //If there is a file: We read all the content of the file, it returns a string.
        //Then that string is deserialized into a DataContainer, and assigned to the field variable.
        string content = File.ReadAllText(filePath);
        dataContainer = JsonSerializer.Deserialize<DataContainer>(content);
    }

    //Later, when we work with databases through Entity Framework Core, you will also need to call SaveChanges after interacting with the database. So, we practice the workflow here.
    //The DataContainer is serialized to JSON, then written to the file. Then the field is cleared.
    public void SaveChanges()
    {
        string serialized = JsonSerializer.Serialize(dataContainer, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        File.WriteAllText(filePath, serialized);
        dataContainer = null;
    }
}
using System;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using XiangtanPlanner.Models;

namespace XiangtanPlanner.Services;

public class DataService
{
    private const string DataFileName = "planner.json";
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    private readonly string _dataDirectory;
    private readonly string _dataFilePath;

    public DataService()
    {
        var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        _dataDirectory = Path.Combine(documents, "湘潭", "data");
        _dataFilePath = Path.Combine(_dataDirectory, DataFileName);
    }

    public async Task<PlannerState> LoadAsync()
    {
        try
        {
            Directory.CreateDirectory(_dataDirectory);
            if (!File.Exists(_dataFilePath))
            {
                var defaultState = PlannerState.CreateDefault();
                await SaveAsync(defaultState).ConfigureAwait(false);
                return defaultState;
            }

            await using var stream = File.OpenRead(_dataFilePath);
            var state = await JsonSerializer.DeserializeAsync<PlannerState>(stream, SerializerOptions).ConfigureAwait(false);
            return state ?? PlannerState.CreateDefault();
        }
        catch
        {
            return PlannerState.CreateDefault();
        }
    }

    public async Task SaveAsync(PlannerState state)
    {
        Directory.CreateDirectory(_dataDirectory);
        await using var stream = File.Create(_dataFilePath);
        await JsonSerializer.SerializeAsync(stream, state, SerializerOptions).ConfigureAwait(false);
    }
}

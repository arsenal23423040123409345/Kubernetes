using System.Text;
using System.Text.Json;
using PlatformService.Dtos;

namespace PlatformService.DataServices.Sync.Http;

public class CommandDataClient : ICommandDataClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public CommandDataClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task SendPlatformToCommand(PlatformReadDto model)
    {
        var httpContent = new StringContent(
            JsonSerializer.Serialize(model),
            Encoding.UTF8,
            "application/json");

        var response = await _httpClient
            .PostAsync(_configuration["CommandService"], httpContent);

        Console.WriteLine(response.IsSuccessStatusCode
            ? "--> Sync POST to Command Service was OK"
            : "--> Sync POST to Command Service was NOT OK");
    }
}

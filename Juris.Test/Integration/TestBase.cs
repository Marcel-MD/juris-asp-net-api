using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Juris.Common.Dtos.User;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Juris.Test.Integration;

public class TestBase
{
    protected readonly HttpClient _client;

    protected TestBase()
    {
        var appFactory = new WebApplicationFactory<Program>();
        _client = appFactory.CreateClient();
    }

    protected async Task Authenticate(string email, string password)
    {
        var response = await _client.PostAsJsonAsync("api/user/login", new CreateUserDto()
        {
            Email = email,
            Password = password
        });

        if (!response.IsSuccessStatusCode)
            throw new Exception($"Can't authenticate user: {email} {password}");

        var dto = await response.Content.ReadFromJsonAsync<UserTokenDto>();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", dto.Token);
    }
}
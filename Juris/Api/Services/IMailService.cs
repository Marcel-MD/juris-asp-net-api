﻿namespace Juris.Api.Services;

public interface IMailService
{
    public Task SendAsync(string to, string subject, string body);
}
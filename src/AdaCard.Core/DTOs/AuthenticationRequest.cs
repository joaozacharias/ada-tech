﻿namespace AdaCard.Core.DTOs;

public class AuthenticationRequest
{
    public string Login { get; set; } = string.Empty;

    public string Senha { get; set; } = string.Empty;
}

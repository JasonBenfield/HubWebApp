﻿namespace XTI_HubWebAppApi.UserList;

public sealed class AddOrUpdateUserModel
{
    public string UserName { get; set; } = "";
    public string Password { get; set; } = "";
    public string PersonName { get; set; } = "";
    public string Email { get; set; } = "";
}
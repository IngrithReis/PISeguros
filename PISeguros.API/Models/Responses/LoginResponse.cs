﻿namespace PISeguros.API.Models.Responses
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public DateTime? Expires { get; set; }
    }
}

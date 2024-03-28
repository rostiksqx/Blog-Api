﻿namespace AuthCookies.Infrastructure;

public interface IPasswordHasher
{
    string Generate(string password);
    bool Verify(string password, string passwordHash);
}
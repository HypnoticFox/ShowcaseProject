﻿
using System.Runtime.Serialization;

namespace ShowcaseProject.Products.API.Exceptions;

public class ConfigurationException : Exception
{
    public ConfigurationException() { }

    public ConfigurationException(string? message) : base(message)
    {
    }

    public ConfigurationException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}

﻿namespace MagicVilla_VillaAPI.Logging;

public class Logging : ILogging
{
    public void Log(string message, string type)
    {
        if(type == "Error")
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error: {message}");
        }
        else if (type == "Info")
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Info: {message}");
        }
        else if (type == "Warning")
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Warning: {message}");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Log: {message}");
        }
    }
}

using System;

public static class NameFactory
{
    private static Random rand = new Random();
    public static string GetName()
    {
        var names = new[] { "gorilla", "chimp", "cow", "parrot", "sloth", "monkey", "giraffe", "cat", "dog", "cock", "dolphin" };
        return names[rand.Next(0, names.Length - 1)];
    }
}
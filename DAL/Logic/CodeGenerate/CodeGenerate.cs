using System;
using System.CodeDom.Compiler;

namespace Application.Logic.CodeGenerate;

public class CodeGenerate
{
    private static Random _random = new Random();

    public static string GeneratedCode()
    {
        string code = string.Empty;
        for (int i = 0; i < 5; i++)
        {
            code += _random.Next(0, 9).ToString();
        }

        return code;

    }
    public static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[_random.Next(s.Length)]).ToArray());
    }
}
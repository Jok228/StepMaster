using System.CodeDom.Compiler;

namespace StepMaster.Models.CodeGenerate;

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
}
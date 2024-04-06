using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace SemanticKernelLatokenTask.Plugins
{
    public class MathPlugin
    {
        [KernelFunction]
        [Description("Sums two integers and returns the result as a string.")]
        public int Sum(
            // Description в параметрах не обязателен
            [Description("The first integer to be added.")] int x,
            [Description("The second integer to be added.")] int y)
        {
            int sum = x + y;
            Console.WriteLine($"Sum {x} + {y} = " + sum);
            return x + y;
        }

        [KernelFunction]
        [Description("Multiplies two integers and returns the result.")]
        public int Multiply(
            // Description в параметрах не обязателен
            [Description("The first integer to be multiplied.")] int x,
            [Description("The second integer to be multiplied.")] int y)
        {
            int result = x * y;
            Console.WriteLine($"Multiplying {x} * {y} = " + result);
            return result;
        }
    }
}
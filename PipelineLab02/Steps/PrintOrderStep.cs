using System.Text;
using Pipeline.Contexts;
using Pipeline.Functions;
using Pipeline.Interfaces;
using Pipeline.Models;


namespace Pipeline.Steps;
public sealed class PrintOrderStep : IPipelineStep<OrderContext>
{
    public void Execute(OrderContext context)
    {
        Console.WriteLine("PrintOrderStep: printing order info...");
        Console.WriteLine($"OrderId: {context.OrderId}");
        Console.WriteLine($"Amount: {context.Amount}");
    }

    public void Introspect(StringBuilder sb, int indent = 0)
    {
        // for (a; !d; c) { b; }
        for (int i = 0; !(i == indent); i += 1)
        {
            sb.Append(' ');
        }

        // int i = 0; // a
        // while (true)
        // {
        //     sb.Append(' '); // b
        //     i += 1; // c
        //     if (i == indent) //d
        //         break;
        // }

        sb.AppendLine($"PrintOrderStep");
    }
}

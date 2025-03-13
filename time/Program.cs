using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        Console.WriteLine("Contador iniciado! Pressione ESC para parar.");

        CancellationTokenSource cts = new CancellationTokenSource();
        Task contadorTask = ContadorHorasAsync(cts.Token);

        
        while (true)
        {
            if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape)
            {
                cts.Cancel();
                break;
            }
            await Task.Delay(100); 
        }

        await contadorTask;
        Console.WriteLine("\nContador encerrado.");
    }

    static async Task ContadorHorasAsync(CancellationToken cancellationToken)
    {
        DateTime inicio = DateTime.Now;

        while (!cancellationToken.IsCancellationRequested)
        {
            TimeSpan elapsed = DateTime.Now - inicio;

            Console.Clear();
            Console.WriteLine($"⏳ Tempo decorrido: {elapsed.Days}d {elapsed.Hours:D2}:{elapsed.Minutes:D2}:{elapsed.Seconds:D2}");
            Console.WriteLine("Pressione ESC para parar.");

            await Task.Delay(1000); 
        }
    }
}

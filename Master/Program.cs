using System.Diagnostics;
using System.IO.Pipes;
using System.Text;

// Connection to agent process
using var server = new NamedPipeServerStream("mypipe", PipeDirection.In);

try
{
    Console.WriteLine("Awaiting for agent connection...");
    server.WaitForConnection();
} catch
{
    Console.WriteLine("Something went wrong with connection.");
    return;
}
Console.WriteLine("Connected successfully. Awaiting for data.");

// Wait for data.
// Process the data.
// Print results.

using var reader = new StreamReader(server);
string? message = reader.ReadLine();

if (message == null)
    return;

Console.WriteLine($"Received: {message}");

Console.ReadKey();
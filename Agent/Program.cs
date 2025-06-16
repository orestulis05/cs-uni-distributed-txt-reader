using System.IO.Pipes;

// Get directory path from args

// Connection to the master process
using var client = new NamedPipeClientStream(".", "mypipe", PipeDirection.Out);
Console.WriteLine("Connecting to Master process...");
client.Connect();
Console.WriteLine("Connected!");

// Get files from a directory
// Read through every file
// Send data to the Master process

using var writer = new StreamWriter(client) { AutoFlush = true };
writer.WriteLine("Hello from the agent!");

// writer.Flush();

Console.ReadKey();
using System.Globalization;
using System.IO.Pipes;

// 1. Each agent receives a directory path containing .txt files.
if (args.Length < 2)
{
    Console.WriteLine("agent.exe <directory> <pipename>");
    return;
}

string directory = args[0];
string pipeName = args[1];

if (!Directory.Exists(directory))
{
    Console.WriteLine("Directory does not exist");
    return;
}

string[] txtFiles = Directory.GetFiles(directory, "*.txt");


// 2. It reads the content of each file, indexes words (e.g., filename; count)
// 3. It sends this information to the Master process using a named pipe (one pipe per agent).
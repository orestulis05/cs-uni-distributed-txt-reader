using System.Diagnostics;
using System.IO.Pipes;
using System.Text;


// 1. Waits for connections from both agents via named pipes.

if (args.Length < 2)
{
    Console.WriteLine("master.exe <pipename1> <pipename2>");
    return;
}

string pipename1 = args[0];
string pipename2 = args[1];

// 2. Receives and processes the indexed word data.
// 3. Aggregates the data and displays the final result (filenames, word, count of word).
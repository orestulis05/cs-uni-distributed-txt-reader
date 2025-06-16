using Agent;
using System.Diagnostics;

// 1. Each agent receives a directory path containing .txt files.
if (args.Length < 2)
{
    Console.WriteLine("agent.exe <directory> <pipename>");
    return;
}

string directory = args[0];
string pipeName = args[1];

Process.GetCurrentProcess().ProcessorAffinity = pipeName == "agent1" ? (IntPtr)0x2 : (IntPtr)0x4;

if (!Directory.Exists(directory))
{
    Console.WriteLine("Directory does not exist");
    return;
}

AgentWorker agent = new AgentWorker(directory, pipeName);
agent.ProcessFiles();
agent.SendToMaster();

Console.ReadKey();
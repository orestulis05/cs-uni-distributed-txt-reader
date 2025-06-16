using Master;
using System.Diagnostics;

if (args.Length < 2)
{
    Console.WriteLine("master.exe <pipename1> <pipename2>");
    return;
}

string pipename1 = args[0];
string pipename2 = args[1];

Process.GetCurrentProcess().ProcessorAffinity = (IntPtr)0x1;

MasterWorker master = new MasterWorker();

Task t1 = master.ListenToAgent(pipename1);
Task t2 = master.ListenToAgent(pipename2);

await Task.WhenAll(t1, t2);

master.PrintResults();

Console.ReadKey();
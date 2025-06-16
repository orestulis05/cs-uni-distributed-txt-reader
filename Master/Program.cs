using Master;
using System.Diagnostics;
using System.IO.Pipes;
using System.Text;

if (args.Length < 2)
{
    Console.WriteLine("master.exe <pipename1> <pipename2>");
    return;
}

string pipename1 = args[0];
string pipename2 = args[1];

MasterWorker master = new MasterWorker();

Task t1 = master.ListenToAgent(pipename1);
Task t2 = master.ListenToAgent(pipename2);

await Task.WhenAll(t1, t2);


Console.ReadKey();
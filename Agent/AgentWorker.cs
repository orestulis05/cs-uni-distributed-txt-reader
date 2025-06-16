using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agent
{
    public class AgentWorker
    {
        private string[] txtFilePaths;
        private Queue<string> dataQueue;

        public AgentWorker(string directory, string pipeName) {
            txtFilePaths = Directory.GetFiles(directory, "*.txt");
            dataQueue = new Queue<string>();
        }

        // 2. It reads the content of each file, indexes words (e.g., filename; count)
        public void ProcessFiles()
        {
            foreach (string filePath in txtFilePaths)
            {
                Dictionary<string, int> wordCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                string content = File.ReadAllText(filePath);
                string[] words = content.Split(new[] { ' ', '\n', '\r', ',', '.', ';', ':', '-', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string word in words)
                {
                    if (!wordCounts.ContainsKey(word))
                        wordCounts[word] = 0;

                    wordCounts[word]++;
                }

                foreach(KeyValuePair<string, int> entry in wordCounts)
                {
                    dataQueue.Enqueue($"{Path.GetFileName(filePath)};{entry.Key};{entry.Value}");
                }
            }

            dataQueue.Enqueue("EOF");

            while (dataQueue.TryDequeue(out string? data))
            {
                if (data == "EOF")
                    break;

                Console.WriteLine(data);
            }
        }

        // 3. It sends this information to the Master process using a named pipe (one pipe per agent).
        public void SendToMaster()
        {

        }

    }
}

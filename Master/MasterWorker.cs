using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Master
{
    struct WordData
    {
        public string fileName;
        public string word;
        public int wordCount;
    }

    public class MasterWorker
    {
        private List<WordData> words;

        public MasterWorker()
        {
            words = new List<WordData>();
        }

        // 1. Waits for connections from both agents via named pipes.
        // 2. Receives and processes the indexed word data.
        public async Task ListenToAgent(string pipeName)
        {
            var server = new NamedPipeServerStream(pipeName, PipeDirection.In);
            await server.WaitForConnectionAsync();
            Console.WriteLine($"{pipeName} agent connection established.");
           
            var reader = new StreamReader(server);

            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                var parts = line.Split(';');
                if (parts.Length < 3)
                {
                    continue;
                }

                WordData word = new WordData();
                word.fileName = parts[0];
                word.word = parts[1];
                word.wordCount = int.Parse(parts[2]);
                words.Add(word);
            }
        }

        // 3. Aggregates the data and displays the final result (filenames, word, count of word).
        public void PrintResults()
        {
            foreach (WordData word in words)
            {
                Console.WriteLine($"{word.fileName}:{word.wordCount}:{word.word}");
            }
        }
    }
}

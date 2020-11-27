
using EasterRaces.IO.Contracts;
using System;
using System.IO;

namespace EasterRaces.IO
{
    public class FileWriter : IWriter
    {
        private readonly string filePath;

        public FileWriter(string filePath)
        {
            this.filePath = filePath;
        }
        public void WriteLine(string text)
        {
            File.AppendAllText(this.filePath, text + Environment.NewLine);
        }

        public void Write(string text)
        {
            File.AppendAllText(this.filePath, text);
        }
    }
}

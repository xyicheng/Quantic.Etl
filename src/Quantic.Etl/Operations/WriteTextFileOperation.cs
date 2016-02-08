using System.IO;
using System.Threading.Tasks;
using Quantic.Common;
using Quantic.Etl.Abstractions;

namespace Quantic.Etl.Operations
{
    public class WriteTextFileOperation : IOperation<bool>
    {
        private readonly string _text;
        private readonly string _fileName;

        public WriteTextFileOperation(string destinationFile, string text)
        {
            Requires.NotNullOrEmpty(destinationFile, nameof(destinationFile));

            _fileName = destinationFile;
            _text = text;
        }

        public async Task<bool> Execute()
        {
            // Whatever.
            if (_text.Length == 0)
                return false;

            if (!File.Exists(_fileName))
                File.Create(_fileName);

            using (var sw = new StreamWriter(_fileName, false))
                await sw.WriteAsync(_text);

            return true;
        }
    }
}

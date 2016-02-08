using System.IO;
using System.Threading.Tasks;
using Quantic.Common;
using Quantic.Etl.Abstractions;

namespace Quantic.Etl.Operations
{
    public class ReadTextFileOperation : IOperation<string>
    {
        private readonly string _fileLocation;

        public ReadTextFileOperation(string file)
        {
            Requires.NotNullOrEmpty(file, nameof(file));

            _fileLocation = file;
        }

        public async Task<string> Execute()
        {
            if (!File.Exists(_fileLocation))
                throw new FileNotFoundException($"Couldn't find the specified file to read: {_fileLocation}");

            using (var sr = new StreamReader(File.OpenRead(_fileLocation)))
                return await sr.ReadToEndAsync();
        }
    }
}

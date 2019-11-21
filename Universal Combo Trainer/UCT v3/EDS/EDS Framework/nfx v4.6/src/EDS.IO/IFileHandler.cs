using System;
using System.IO;

namespace EDS.IO
{
    public interface IFileHandler
    {
        void CreateFile(string path);

        void CreateFile(string path, bool overwrite);

        void CreateFile(string path, bool overwrite, bool createDirectoryStructure);

        void CreateFolder(string path);

        string CreateTempFile();

        string CreateTempFile(string directory);

        string CreateTempFolder();

        void Compress(string destination, params string[] sourceFiles);

        void Decompress(string sourceFile, string destination);
    }
}

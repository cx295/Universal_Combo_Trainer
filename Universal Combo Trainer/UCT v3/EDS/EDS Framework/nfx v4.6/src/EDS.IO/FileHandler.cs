using System;
using System.IO;
using System.IO.Compression;

namespace EDS.IO
{
    public class FileHandler : IFileHandler
    {

        #region CreateFile

        /// <summary>
        /// Creates the at the specified path. 
        /// If the directories do not exist, they will not be created.
        /// If the file already exists, it will not be overwritten.
        /// </summary>
        /// <param name="path">The path to create the file.</param>
        public void CreateFile(string path)
        {
            CreateFile(path, false);
        }

        /// <summary>
        /// Creates the file at the specified path.
        ///  If the directories do not exist, they will not be created.
        /// </summary>
        /// <param name="path">The path to create the file.</param>
        /// <param name="overwrite">If true, the file will be overwritten if it already exists.</param>
        public void CreateFile(string path, bool overwrite)
        {
            CreateFile(path, overwrite, false);
        }

        /// <summary>
        /// Creates the file at the specified path.
        /// </summary>
        /// <param name="path">The path to create the file.</param>
        /// <param name="overwrite">If true, the file will be overwritten if it already exists.</param>
        /// <param name="createDirectoryStructure">If true, the directory structure will be created if it doesn't exist.</param>
        public void CreateFile(string path, bool overwrite, bool createDirectoryStructure)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }

            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("path");
            }

            if (File.Exists(path) && !overwrite)
            {
                return;
            }

            var fileRoot = Path.GetDirectoryName(path);
            var fileName = Path.GetFileName(path);

            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException("FilePath must be a full path.", "filePath");
            }

            if (!Directory.Exists(fileRoot))
            {
                if (!createDirectoryStructure)
                {
                    throw new IOException("FilePath does not exist.");
                }

                CreateFolder(fileRoot);
            }

            using (var tmp = File.Create(path))
            {
                tmp.Close();
            }
        }

        /// <summary>
        /// Creates a zero byte, uniquely-named file.
        /// </summary>
        /// <returns>The path to the file.</returns>
        public string CreateTempFile()
        {
            var directory = CreateTempFolder();

            var path = CreateTempFile(directory);

            return path;
        }

        /// <summary>
        /// Creates a zero byte, uniquely-named file in the specified directory.
        /// </summary>
        /// <param name="directory">The directory to create the file.</param>
        /// <returns>The path to the file.</returns>
        public string CreateTempFile(string directory)
        {
            var fileName = Path.GetRandomFileName();
            var path = Path.Combine(directory, fileName);

            CreateFile(path);

            return path;
        }

        #endregion CreateFile

        #region CreateFolder

        /// <summary>
        /// Creates the specified folder structure.
        /// </summary>
        /// <param name="path">The folder structure to create.</param>
        public void CreateFolder(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException("path");
            }

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        /// <summary>
        /// Creates a uniquely-named folder.
        /// </summary>
        /// <returns>The path of the folder.</returns>
        public string CreateTempFolder()
        {
            var tmpPath = Path.GetTempPath();
            var tmpFolder = Path.Combine(tmpPath, Guid.NewGuid().ToString());

            CreateFolder(tmpFolder);

            return tmpFolder;
        }

        #endregion CreateFolder

        #region Compress/Decompress

        /// <summary>
        /// Compresses the list of files into the location specified.
        /// </summary>
        /// <param name="sourceFiles">The list of files to compress.</param>
        /// <param name="destination">The location to store the compressed file.</param>
        public void Compress(string destination, params string[] sourceFiles)
        {
            var tempFolder = CreateTempFolder();

            foreach(var sourceFile in sourceFiles)
            {
                File.Copy(sourceFile, Path.Combine(tempFolder, Path.GetFileName(sourceFile)));
            }

            ZipFile.CreateFromDirectory(tempFolder, destination, CompressionLevel.Optimal, false);

        }

        /// <summary>
        /// Decompresses the given file to the given destination.
        /// </summary>
        /// <param name="sourceFile">The file to be decompressed.</param>
        /// <param name="destination">The location to store the decompressed files.</param>
        public void Decompress(string sourceFile, string destination)
        {
            ZipFile.ExtractToDirectory(sourceFile, destination);
        }

        #endregion

    }
}

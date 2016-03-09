using System;
using System.IO;
using NUnit.Framework;

namespace MiscCodeTests
{
    [TestFixture]
    public class DeleteFiles
    {
        [Test]
        public void DeleteMeta()
        {
            const string path = @"C:\_Temp\Unianio";
            var fsi = new FileSystemIterator(path);
            fsi.ProcessElement += (o, a) =>
                                  {
                                      if (a.IsFile && a.Name.EndsWith(".meta"))
                                      {
                                          a.FileInfo.Delete();
                                      }
                                  };
            fsi.Run();

        }

    }

    public class FileSystemIterator
    {
        public event EventHandler<FileSystemElementArgument> ProcessElement;

        private string _rootPath;
        private int _currentlevel = 0;
        private bool _cancel = false;

        public FileSystemIterator(string rootPath)
        {
            this.RootPath = rootPath;
        }

        public string RootPath
        {
            get { return this._rootPath; }
            private set
            {
                if (!Directory.Exists(value))
                {
                    throw new ArgumentException(String.Format("The folder '{0}' does not exist", value), "RootPath");
                }
                this._rootPath = value;
            }
        }

        public void Run()
        {
            this.ParseDirectory(this.RootPath);
        }

        private void ParseDirectory(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            this.ProcessDirectory(di);
            if (this._cancel) { return; }
            FileInfo[] fileInfo = di.GetFiles();
            for (int i = 0; i < fileInfo.Length; i++)
            {
                this.ProcessFile(fileInfo[i], di);
                if (this._cancel) { return; }
            }
            DirectoryInfo[] dirInfo = di.GetDirectories();
            for (int i = 0; i < dirInfo.Length; i++)
            {
                this._currentlevel++;
                this.ParseDirectory(Path.Combine(path, dirInfo[i].Name));
                this._currentlevel--;
            }

        }

        private void ProcessFile(FileInfo fi, DirectoryInfo di)
        {
            this.OnProcessElement(new FileSystemElementArgument(true, fi, di, this._currentlevel, this.RootPath));
        }

        private void ProcessDirectory(DirectoryInfo di)
        {
            this.OnProcessElement(new FileSystemElementArgument(false, null, di, this._currentlevel, this.RootPath));
        }

        private void OnProcessElement(FileSystemElementArgument arg)
        {
            if (this.ProcessElement != null)
            {
                this.ProcessElement(this, arg);
                if (arg.Cancel)
                {
                    this._cancel = true;
                }
            }
        }
    }
    public class FileSystemElementArgument : EventArgs
    {
        private readonly bool _isFile;
        private readonly bool _isFolder;
        private readonly FileInfo _fileInfo;
        private readonly DirectoryInfo _directoryInfo;
        private readonly int _level;
        private readonly string _rootPath;
        private bool _cancel = false;

        public FileSystemElementArgument(bool isFile, FileInfo fi, DirectoryInfo di, int level, string rootPath)
        {
            this._isFile = isFile;
            this._isFolder = !this._isFile;
            this._fileInfo = fi;
            this._directoryInfo = di;
            this._level = level;
            this._rootPath = rootPath;
        }

        public bool Cancel
        {
            get { return this._cancel; }
            set { this._cancel = value; }
        }
        public bool IsFile
        {
            get { return this._isFile; }
        }
        public bool IsFolder
        {
            get { return this._isFolder; }
        }
        public FileInfo FileInfo
        {
            get { return this._fileInfo; }
        }
        public DirectoryInfo DirectoryInfo
        {
            get { return this._directoryInfo; }
        }
        public int Level
        {
            get { return this._level; }
        }
        public string RootPath
        {
            get { return this._rootPath; }
        }
        public string Name
        {
            get
            {
                if (this.IsFile && this.FileInfo != null)
                {
                    return this.FileInfo.Name;
                }
                else if (this.IsFolder && this.DirectoryInfo != null)
                {
                    return this.DirectoryInfo.Name;
                }
                else
                {
                    return null;
                }
            }
        }

    }
}
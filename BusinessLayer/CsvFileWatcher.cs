using System.IO;
using System.Threading;
using BusinessLayer.Parsers;
using NLog;

namespace BusinessLayer
{
    public class CsvFileWatcher
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly FileHandler _handler;
        private readonly ReaderWriterLockSlim _locker;
        private readonly FileSystemWatcher _watcher;
        private bool _isEnabled;

        public CsvFileWatcher(string directoryPath)
        {
            _watcher = new FileSystemWatcher(directoryPath)
            {
                Filter = "*.csv"
            };
            _isEnabled = true;
            _locker = new ReaderWriterLockSlim();
            _handler = new FileHandler(new CsvParser());
        }

        public void StartWatch()
        {
            _watcher.Created += OnCreated;
            _watcher.EnableRaisingEvents = true;
            while (_isEnabled) Thread.Sleep(1000);
        }

        public void StopWatch()
        {
            _watcher.EnableRaisingEvents = false;
            _isEnabled = false;
            _watcher.Created -= OnCreated;
        }

        private void OnCreated(object sender, FileSystemEventArgs args)
        {
            _handler.FileName = args.FullPath;
            var thread = new Thread(_handler.StartHandle);
            thread.Start();
        }
    }
}
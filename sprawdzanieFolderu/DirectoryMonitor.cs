using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.ObjectModel;


namespace sprawdzanieFolderu
{
    class DirectoryMonitor
    {
        private string address;
        private string ending;
        private FileSystemWatcher watcher;
        public ObservableCollection<FileToMonitor> Files;
        public string whatHappened;
        

        public DirectoryMonitor(string adr, string end)
        {
            address = adr;
            ending = end;
            Files=new ObservableCollection<FileToMonitor>();
            StartWatch();

        }

        private void StartWatch()
        {
            if (ending != "")
            {
                watcher = new FileSystemWatcher(address, "*." + ending);
                DirectoryInfo s = new DirectoryInfo(address);
                FileInfo[] items = s.GetFiles("*." + ending);
                Files.Clear();
                for (int i = 0; i < items.Count(); i++)
                {
                    FileToMonitor newFile = new FileToMonitor(items[i].Name, address);
                    Files.Add(newFile);
                }
                watcher.Created += new FileSystemEventHandler(NewItem);
                watcher.Deleted += new FileSystemEventHandler(DeletedItem);
                watcher.Renamed += new RenamedEventHandler(RenamedItem);
                watcher.EnableRaisingEvents = true;
            }
        }

        public void StopWatch()
        {
            watcher.Dispose();
        }

        private void NewItem(object source, FileSystemEventArgs e)
        {   
            whatHappened = e.Name + " was created";
            DirectoryInfo s = new DirectoryInfo(address);
            FileInfo[] items = s.GetFiles("*." + ending);
            Files.Clear();
            for (int i = 0; i < items.Count(); i++)
            {
                FileToMonitor newFile = new FileToMonitor(items[i].Name, address);
                Files.Add(newFile);
            }

        }

        private void DeletedItem(object source, FileSystemEventArgs e)
        {
            whatHappened = e.Name + " was deleted";
            DirectoryInfo s = new DirectoryInfo(address);
            FileInfo[] items = s.GetFiles("*." + ending);
            Files.Clear();
            for (int i = 0; i < items.Count(); i++)
            {
                FileToMonitor newFile = new FileToMonitor(items[i].Name, address);
                Files.Add(newFile);
            }

        }

        private void RenamedItem(object source, RenamedEventArgs e)
        {
            whatHappened =e.OldName + " was renamed to " + e.Name;
            DirectoryInfo s = new DirectoryInfo(address);
            FileInfo[] items = s.GetFiles("*." + ending);
            Files.Clear();
            for (int i = 0; i < items.Count(); i++)
            {
                FileToMonitor newFile = new FileToMonitor(items[i].Name, address);
                Files.Add(newFile);
            }

        }

        public string WhatHappened
        {
            get { return whatHappened; }
            set { whatHappened = value; }
        }

    }
}

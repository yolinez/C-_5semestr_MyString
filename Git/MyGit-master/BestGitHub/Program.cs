using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Resources;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Serialization;

namespace BestGitHub
{
    public enum FileStatus
    {    
        Null,
        Added,
        Removed,
        Deleted,
        New
    };

    static class StringExpansion
    {
        public static string[] GetInput(this string input) => input.Split(' ');
    }
    
    [Serializable]
    public class Node
    {
        public Node()
        {
            
        }
        public Node(string fileName,string size,string created,string modified,string path)
        {
            FileName = fileName;
            Size = size;
            Created = created;
            Modified = modified;
            Path = path;
        }
        public Node(string fileName,string created,string modified,string path)
        {
            FileName = fileName;
            Size = "-1";
            Created = created;
            Modified = modified;
            Path = path;
        }

        public FileStatus @FileStatus = FileStatus.New;
        
        public string FileName;
        public string Size;
        public string Created;
        public string Modified;
        public string Path;

        public bool Applyed;
    }

    [Serializable]
    public class Dictionary
    {
        public string path;
        public bool Applyed = false;

        public Dictionary(){}
        
        public Dictionary(string path)
        {
            this.path = new DirectoryInfo(path).FullName;
            BuildDictionary(path);
        }
        
        public List<Node> GitFiles = new List<Node>();

        private void BuildDictionary(string path)
        {
            //this.path = new DirectoryInfo(path).FullName;
            if (Directory.Exists(path))
            {
                string[] files = Directory.GetFiles(path);
                foreach (var file in files)
                {
                    var fileInfo = new FileInfo(file);
                    var node = new Node(fileInfo.FullName,fileInfo.Length.ToString(),fileInfo.CreationTime.ToString(),fileInfo.LastWriteTime.ToString(),fileInfo.FullName);
                    GitFiles.Add(node);
                }

                string[] directories = Directory.GetDirectories(path);
                foreach (var directory in directories)
                {
                    var directoryInfo = new DirectoryInfo(directory);
                    var node = new Node(directoryInfo.Name,directoryInfo.CreationTime.ToString(),directoryInfo.LastWriteTime.ToString(),directoryInfo.FullName);
                    GitFiles.Add(node);
                    BuildDictionary(directory);
                }
            }
        }
    }
    
    [Serializable]
    public class Git
    {
        public List<Dictionary> _dictionary = new List<Dictionary>();
        public Dictionary _choosenDictionary;

        public Git(){}

        public Git(string[] pathes)
        {
            foreach (var path in pathes)
            {
                var newDictionary = new Dictionary(path);
                _dictionary.Add(newDictionary);
            }
        }
        
        public Git(List<Dictionary> dictionarys)
        {
            _dictionary = dictionarys;
        }
        public Git(Dictionary dictionary)
        {
            Init(dictionary);
        }

        public void Init(Dictionary dictionary)
        {
            _dictionary.Add(dictionary);
        }
        
        public void Init(string path)
        {
            if (CheckDirrectoryLike(path))
            {
                _dictionary.Add(new Dictionary(path));
            }
        }

        private bool CheckDirrectoryLike(string path)
        {
            foreach (var dirrectory in _dictionary)
            {
                if (dirrectory.path == path)
                {
                    Console.WriteLine("Дерриктория уже находится под версионным контролем");
                    return false;
                }
            }

            return true;
        }
        
        public void Add(string fullPath)
        {
                for (int i = 0; i < _choosenDictionary.GitFiles.Count; i++)
                {
                    if (_choosenDictionary.GitFiles[i].FileName == fullPath || fullPath == "-A")
                    {
                        _choosenDictionary.GitFiles[i].FileStatus = FileStatus.Added;
                        if(fullPath!="-A")return;
                    }
                }
            if(fullPath!="-A")Console.WriteLine("Файла не обнаружено");
        }
        
        public void Remove(string fullPath)
        {
                for (int i = 0; i < _choosenDictionary.GitFiles.Count; i++)
                {
                    if (_choosenDictionary.GitFiles[i].FileName == fullPath)
                    {
                        _choosenDictionary.GitFiles[i].FileStatus = FileStatus.Removed;
                        return;
                    }
                }
            Console.WriteLine("Файла не обнаружено");
        }

        public void Apply(string dirPath)
        {
            foreach (var dictionary in _dictionary)
            {
                if (dictionary.path == dirPath)
                {
                    foreach (var file in dictionary.GitFiles)
                    {
                        if (file.FileStatus != FileStatus.New)
                        {
                            //file.FileStatus = FileStatus.Null;
                            file.Applyed = true;
                        }
                    }
                    Console.WriteLine("Данные успешно сохранены");
                    return;
                }
            }
            Console.WriteLine("Файла не обнаружено");
        }

        public void CheckOut(string path)
        {
            foreach (var dictionary in _dictionary)
            {
                if (dictionary.path == path)
                {
                    Console.WriteLine($"Выбран путь:{path}");
                    _choosenDictionary = dictionary;
                    return;
                }
            }
            Console.WriteLine("Директория не найдена");
            _choosenDictionary = null;
        }
        
        public void CheckOut(int num)
        {
            if (num < _dictionary.Count)
            {
                _choosenDictionary = _dictionary[num];
                Console.WriteLine($"Выбран путь:{_dictionary[num].path}");
                return;
            }
            Console.WriteLine("Директория не найдена");
            _choosenDictionary = null;
        }
        
        public void Status()
        {
            if (_choosenDictionary == null)
            {
                Console.WriteLine("Сначала выберите дирректорию!");
                return;
            }
                if (!_choosenDictionary.Applyed)
                {
                    Console.WriteLine($"\nДерриктория: {_choosenDictionary.path}");
                    var dictionaryCopy = new Dictionary(_choosenDictionary.path);
                    for (int i = 0; i < _choosenDictionary.GitFiles.Count; i++)
                    {
                        var dictionaryOwner = _choosenDictionary;
                        CheckForDeleted(ref dictionaryOwner, ref dictionaryCopy, i);
                    }

                    foreach (var file in _choosenDictionary.GitFiles)
                    {
                        if(!file.Applyed)Console.WriteLine(
                            $"Название:{file.FileName},Размер {file.Size},Дата создания{file.Created},Дата модификации{file.Modified},Статус:{file.FileStatus}");
                    }
            }
        }

        private void CheckForDeleted(ref Dictionary dictionaryOwner,ref Dictionary dictionary,int index)
        {
            bool isDeleted = true;
            for (int j = 0; j < dictionary.GitFiles.Count; j++)
            {
                if (dictionaryOwner.GitFiles[index].FileName == dictionary.GitFiles[j].FileName)
                {
                    isDeleted = false;
                    break;
                }
            }
            if(isDeleted)dictionaryOwner.GitFiles[index].FileStatus = FileStatus.Deleted;
        }
    }
    
    internal class Program
    {
        public static void Save(Git git)
        {
            var xmlSerializer = new XmlSerializer(typeof(Git));
            using (FileStream fs = new FileStream("save.xml", FileMode.OpenOrCreate))
            {
                xmlSerializer.Serialize(fs,git);
                Console.WriteLine("Сохранение произашло успешно");
            }
        }

        public static Git Load()
        {
            var xmlSerializer = new XmlSerializer(typeof(Git));
            if (File.Exists("save.xml"))
            {
                using (FileStream fs = new FileStream("save.xml", FileMode.Open))
                {
                    return (Git) xmlSerializer.Deserialize(fs);
                }
            }
            return new Git();
        }
        
        public static void Main(string[] args)
        {            
            
            Git git = Load();
            string[] command;
            int dirNum;
            command = Console.ReadLine().GetInput();
            while (command[0] != "exit")
            {
                switch (command[0])
                {
                    case "":
                        Console.WriteLine("Поле команды не должно быть пустым");
                        break;
                    case "init":
                        if(command[1]!=null)git.Init(command[1]);
                        break;
                    case "status":
                        git.Status();
                        break;
                    case "add":
                        if (git._choosenDictionary != null && command[1] != null)
                        {
                            if (command[1] != "-A")
                            {
                                git.Add($"{git._choosenDictionary.path}\u005c{command[1]}");
                            }
                            else
                            {
                                git.Add("-A");
                            }
                        }
                        break;
                    case "remove":
                        if(git._choosenDictionary!=null && command[1]!=null)git.Remove($"{git._choosenDictionary.path}\u005c{command[1]}");
                        break;
                    case "apply":
                        if(command[1]!=null)git.Apply(command[1]);
                        break;
                    case "checkout":
                        if (Int32.TryParse(command[1], out dirNum))
                        {
                            git.CheckOut(dirNum);
                        }
                        else
                        {
                            git.CheckOut(command[1]);
                        }
                        break;
                }
                command = Console.ReadLine().GetInput();
            }

            Save(git);
        }
    }
}
using System;
using System.IO;
using System.Text.Json;

using AirConsole.Solution.Models;

namespace AirConsole.Solution.Data
{
    public class JsonFileRepository<TItem, TKey> : InMemoryRepository<TItem, TKey>
        where TItem : Model<TKey>
    {
        public string Filename { get; private set; }
        public JsonFileRepository(string filename)
        {
            Filename = filename;
            Load();
        }

        public override void Add(TItem item)
        {
            base.Add(item);
            Save();
        }

        public override void Edit(TItem item)
        {
            base.Edit(item);
            Save();
        }

        public override TItem Delete(TKey key)
        {
            return base.Delete(key);
            Save();
        }

        private void Load()
        {
            if (File.Exists(Filename))
            {
                var fileContents = File.ReadAllText(Filename);
                Console.WriteLine(fileContents);
                var items = JsonSerializer.Deserialize<TItem[]>(fileContents);
                foreach (var item in items)
                {
                    base.Add(item);
                }
            }
        }
        
        private void Save()
        {
            var jsonString = JsonSerializer.Serialize(base.GetAll());
            File.WriteAllText(Filename, jsonString);
        }
    }
}
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace CharacterSheet.Data
{
    public class KeyValueContext : DbContext
    {
        public DbSet<KeyValue> KeyValues { get; set; }

        public KeyValueContext()
        {
            SQLitePCL.Batteries_V2.Init();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "keyvalues.db3");
            options.UseSqlite($"Filename={dbPath}");
        }
    }

    public class KeyValue
    {
        [Key, Required]
        public string Key { get; set; }

        [Required]
        public string Value { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodTypeC.ConsoleUI
{
    internal class Beer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brewery { get; set; }
        public string Style { get; set; }
        public List<string> Flavors { get; set; }
        public double AlcoholByVolume { get; set; }
        public double Score { get; set; }
        public DateTime? Added { get; set; }
        public DateTime LastModified { get; set; }

        public void Add(int id, string? name, string? brewery, string? style, string flavor, double alcoholByVolume, double score)
        {
            this.Id = id;
            this.Name = name;
            this.Brewery = brewery;
            this.Style = style;
            this.Flavors.Add(flavor);
            this.AlcoholByVolume = alcoholByVolume;
            this.Score = score;
            DateTime dateTimeNow = new DateTime().Date;
            this.Added = dateTimeNow;
        }
    }
}
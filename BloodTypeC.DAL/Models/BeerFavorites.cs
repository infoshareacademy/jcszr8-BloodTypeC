using BloodTypeC.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodTypeC.DAL
{
    public class BeerFavorites
    {
        public int Id { get; set; }
        public List<Beer> Beers { get; set; }
        //public int UserId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Books.Domain.Entities
{
    public class CountryEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        [JsonIgnore]
        public ICollection<CityEntity> Cities { get; set; }
    = new List<CityEntity>();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Domain.Entities
{
    public class CityEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<UserEntity>? UserEntities { get; set; }
    }
}

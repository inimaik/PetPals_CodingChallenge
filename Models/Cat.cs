using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Cat : Pet
    {
        public string CatColor { get; set; }

        public Cat(string name, int age, string breed, string catColor) : base(name, age, breed)
        {
            CatColor = catColor;
        }
    }
}

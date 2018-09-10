using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Phase
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public ObservableCollection<Color> Colors { get; set; }
    }
}

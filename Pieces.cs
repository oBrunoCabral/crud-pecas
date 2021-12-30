using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CRUDPecas
{
    public class Piece
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public Dimension Dimension { get; set; }
    }

    public class Dimension
    {
        public string Width { get; set; }
        public string Height { get; set; }
    }
}



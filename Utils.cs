using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDPecas
{
    public class Utils
    {
        public static List<Piece> LoadJson()
        {
            var systemPath = System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            var filePath = Path.Combine(systemPath, "files");
            
            var json = filePath + @"Piece.json";
            if (!File.Exists(json))
            {
                File.WriteAllText(json, "[]");

            }
            List<Piece> piecesList = JsonConvert.DeserializeObject<List<Piece>>(File.ReadAllText(json));
            return piecesList;
        }
    }
}

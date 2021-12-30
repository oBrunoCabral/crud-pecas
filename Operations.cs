using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDPecas
{
    public class Operations
    {
        public static void AddOrEditPiece(Piece Piece)
        {
            var piecesList = Utils.LoadJson();

            if(piecesList.Exists( x=>x.Code == Piece.Code))
            {
                int index = piecesList.FindIndex(x => x.Code == Piece.Code);
                piecesList[index] = Piece;
                WriteOnFile(piecesList);                
            }
            else
            {
                piecesList.Add(Piece);
                WriteOnFile(piecesList);
            }
        }

        public static void RemovePiece(Piece Piece)
        {
            var piecesList = Utils.LoadJson();
            int index = piecesList.FindIndex(x => x.Code == Piece.Code);
            piecesList.RemoveAt(index);
            WriteOnFile(piecesList);
        }

        public static List<Piece> FindByCode(string searchTerm)
        {
            List<Piece> piecesList = Utils.LoadJson().FindAll(x => x.Code == searchTerm);
            return piecesList;
        }
        public static List<Piece> FindByDescription(string searchTerm)
        {
            List<Piece> piecesList = Utils.LoadJson().FindAll(x => x.Description.Contains(searchTerm));
            return piecesList;
        }

        public static List<Piece> FindByDimension(string searchTerm)
        {
            List<Piece> aux1 = Utils.LoadJson().FindAll(x => x.Dimension.Height == searchTerm);
            List<Piece> aux2 = Utils.LoadJson().FindAll(x => x.Dimension.Width == searchTerm);

            List<Piece> piecesList = aux1.Concat(aux2).ToList();

            return piecesList;
        }

        public static void WriteOnFile(List<Piece> piecesList)
        {
            var systemPath = System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            var filePath = Path.Combine(systemPath, "files");

            File.WriteAllText(filePath + "Piece.json", JsonConvert.SerializeObject(piecesList));
        }
    }
}

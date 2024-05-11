//Written for games in the eSofnet engine.
//Legend of Edda: Pegasus https://store.steampowered.com/app/2241570/
//Luna Online: Reborn https://store.steampowered.com/app/457590/
//Super Mecha Champions https://store.steampowered.com/app/1368910/

using System.IO;

namespace eSofnet_Extractor
{
    class Program
    {
        static void Main(string[] args)
        {
            BinaryReader br = new(File.OpenRead(args[0]));

            br.ReadInt32();
            int filecount = br.ReadInt32();
            br.BaseStream.Position = 0x5c;
            string path = Path.GetDirectoryName(args[0]) + "//" + Path.GetFileNameWithoutExtension(args[0]);
            Directory.CreateDirectory(path);
            for (int i = 0; i < filecount; i++)
            {
                int size = br.ReadInt32();
                int fileSize = br.ReadInt32();
                int nameSize = br.ReadInt32();
                int unknown = br.ReadInt32();
                br.ReadBytes(16);
                string name = new(System.Text.Encoding.GetEncoding("ISO-8859-1").GetChars(br.ReadBytes(nameSize)));
                br.ReadByte();
                BinaryWriter bw = new(File.Create(path + "//" + name));
                bw.Write(br.ReadBytes(fileSize));
                bw.Close();
            }
        }
    }
}

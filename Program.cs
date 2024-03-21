﻿//Written for games in the eSofnet engine.
//Legend of Edda: Pegasus https://store.steampowered.com/app/2241570/
//Luna Online: Reborn https://store.steampowered.com/app/457590/
using System.IO;

namespace Legend_of_Edda_Pegasus_Extractor
{
    class Program
    {
        static void Main(string[] args)
        {
            BinaryReader br = new(File.OpenRead(args[0]));

            br.ReadInt32();
            int filecount = br.ReadInt32();
            br.BaseStream.Position = 0x5c;
            Directory.CreateDirectory(Path.GetDirectoryName(args[0]) + "//" + Path.GetFileNameWithoutExtension(args[0]));
            for (int i = 0; i < filecount; i++)
            {
                int size = br.ReadInt32();
                int fileSize = br.ReadInt32();
                int nameSize = br.ReadInt32();
                int unknown = br.ReadInt32();
                br.ReadBytes(16);
                string name = new(br.ReadChars(nameSize));
                br.ReadByte();
                using FileStream FS = File.Create(Path.GetDirectoryName(args[0]) + "//" + Path.GetFileNameWithoutExtension(args[0]) + "//" + name);
                BinaryWriter bw = new(FS);
                bw.Write(br.ReadBytes(fileSize));
                bw.Close();
            }
        }
    }
}

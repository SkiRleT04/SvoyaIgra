using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Objects.Utils
{
    static class NameRoom
    {
        static int position = 0;
        static string[] names;

        static NameRoom()
        {
            var filePath = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())) + @"\Files\NamesRoom.txt";
            names = File.ReadAllLines(filePath);
        }

        //выдает имя  для комнаты
        public static string Get()
        {
            if (position == names.Length - 1)
                position = 0;
            return names[position++];
        }
    }
}

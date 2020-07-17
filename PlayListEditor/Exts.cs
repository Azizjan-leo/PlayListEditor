using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayListEditor
{
    public static class Exts
    {
        public static string ToString(this MediaItem i)
        {
            return i.Name + "," + i.Length.ToString(@"hh\:mm\:ss");
        }

    }
}

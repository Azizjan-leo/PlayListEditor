using System;
using System.Collections.Generic;

namespace PlayListEditor
{
    public struct MediaItem
    {
        public string Name { get; set; }
        public string Length { get; set; }

        public MediaItem(string name, TimeSpan length) : this()
        {
            Name = name;
            Length = length.ToString(@"hh\:mm\:ss");
        }

        public MediaItem(string name, string length) : this()
        {
            Name = name;
            Length = length;
        }

        public string ToCSVLine()
        {
            return Name + "," + Length;
        }

    }

    public class PlayList
    {
        public string Name { get; set; }
        public List<MediaItem> Items { get; set; }
        public TimeSpan Duration 
        {
            get
            {
                TimeSpan tmp = default;
                foreach (var item in Items)
                {
                    tmp += TimeSpan.Parse(item.Length);
                }
                return tmp;
            } 
        }
        public PlayList(string name)
        {
            Name = name;
            Items = new List<MediaItem>();
        }
    }
    public static class Catalog
    {
        public static TimeSpan Duration { 
            get
            {
                TimeSpan tmp = default;
                foreach (var list in Lists)
                {
                    tmp += list.Duration;
                }
                return tmp;
            }
        }
        public static List<PlayList> Lists = new List<PlayList>();
    }
}

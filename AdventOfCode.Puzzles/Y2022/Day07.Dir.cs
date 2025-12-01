using System.Runtime.InteropServices;

namespace AdventOfCode.Puzzles.Y2022;

public partial class Day07
{
    class Dir
    {
        public Dir(string name, Dir? parent = null)
        {
            Name = name;
            Parent = parent;
        }

        public string Name;
        public Dir? Parent;
        public List<Dir> SubDirs = new();
        public int TotalFileSize = 0;

        public int TotalSize
        {
            get
            {
                var totalSize = 0;
                foreach (var item in CollectionsMarshal.AsSpan(SubDirs))
                {
                    totalSize += item.TotalSize;
                }
                return totalSize + TotalFileSize;
            }
        }

        public override string ToString() => Name;
    }
}

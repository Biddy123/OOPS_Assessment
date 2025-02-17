using System;
using System.Collections.Generic;

namespace OOPS_Assessment
{
    internal class LevelManager
    {
        private List<int[,]> levels;

        public LevelManager()
        {
            levels = new List<int[,]>
    {
        new int[10, 10]
        {
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 3, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 2, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
        },
        new int[10, 10]
        {
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 2, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 2, 0, 3, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 3, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
        }
    };
        }


        public int[,] GetLevel(int index)
        {
            if (index < levels.Count)
                return levels[index];
            else
                throw new IndexOutOfRangeException("No more levels available.");
        }

        public int LevelCount => levels.Count;
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPS_Assessment
{
    public static class Levels
    {
        public static int[][,] LevelMaps = new int[][,]
{
    // Level 1
    new int[,]
    {
        {1,1,1,1,1,1,1,1,1,1},
        {1,0,0,0,0,0,0,0,0,1},
        {1,0,0,0,0,0,0,0,0,1},
        {1,0,0,2,3,4,0,0,0,1}, 
        {1,0,0,0,0,0,0,0,0,1},
        {1,0,0,0,0,0,0,0,0,1}, 
        {1,0,0,0,0,0,0,0,0,1},
        {1,0,0,0,0,0,0,0,0,1},
        {1,0,0,0,0,0,0,0,0,1},
        {1,1,1,1,1,1,1,1,1,1}
    },
    // Level 2
    new int[,]
    {
        {1,1,1,1,1,1,1,1,1,1},
        {1,2,0,0,0,0,0,0,4,1},
        {1,0,0,0,0,0,0,0,0,1},
        {1,0,0,0,0,0,0,0,0,1},
        {1,0,0,0,0,0,0,0,0,1},
        {1,0,0,0,0,0,0,0,0,1},
        {1,0,3,0,0,0,0,0,0,1},
        {1,0,0,0,0,0,0,0,0,1},
        {1,0,0,0,0,0,0,0,0,1},
        {1,1,1,1,1,1,1,1,1,1}
    },
    // Level 3
    new int[,]
    {
        {1,1,1,1,1,1,1,1,1,1},
        {1,2,0,0,1,0,0,0,0,1},
        {1,0,0,0,1,0,0,0,0,1},  
        {1,0,0,0,1,0,0,4,0,1},
        {1,0,3,0,1,0,0,0,0,1},
        {1,0,0,0,0,0,0,0,0,1},  
        {1,0,3,0,0,0,0,4,0,1},
        {1,0,0,0,1,0,0,0,0,1},
        {1,0,0,0,1,0,0,0,0,1},
        {1,1,1,1,1,1,1,1,1,1}
    },
    // Level 4
    new int[,]
    {
        {1,1,1,1,1,1,1,1,1,1},
        {1,2,0,0,1,0,0,0,0,1},
        {1,0,3,0,1,0,4,0,0,1},  
        {1,0,0,0,1,0,3,1,0,1},
        {1,0,1,0,0,0,0,1,0,1},
        {1,0,1,0,3,0,0,4,0,1},  
        {1,0,0,0,1,0,3,0,0,1},
        {1,4,0,0,1,0,4,0,0,1},  
        {1,0,0,0,1,0,0,0,0,1},
        {1,1,1,1,1,1,1,1,1,1}
    },
    // Level 5
    new int[,]
    {
        {1,1,1,1,1,1,1,1,1,1},
        {1,2,0,0,1,0,0,0,0,1},
        {1,0,3,0,1,0,4,0,4,1},  
        {1,0,0,0,1,0,3,1,0,1},
        {1,0,1,0,0,0,0,1,3,1},
        {1,0,1,0,3,0,3,4,0,1},  
        {1,0,0,0,1,0,3,0,0,1},
        {1,4,0,0,1,0,4,0,0,1},
        {1,0,0,0,1,0,0,0,4,1},
        {1,1,1,1,1,1,1,1,1,1}
    }

};
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Completely_Irrelevant
{
    public enum InvokingMode { Single, Multiple };

    public static class Constants
    {
        public readonly static float G_CONST = .2f; //.14
        public readonly static int CHAR_SPD_CONST = 2;
        public readonly static float CHAR_JMP_CONST = 4.6f;
        public readonly static float ENCUMB_SPD_CONST = .75f;
        public readonly static float ENCUMB_JMP_CONST = .85f;
        public readonly static float CRUSHING_SPD_CONST = .7f;

        public readonly static int CHARACTER_TYPE_1 = 1;
        public readonly static int CHARACTER_TYPE_2 = 2;
        /**
         * TODO: INSERT CHAR_TYPES HERE
         * */

        public readonly static int MAX_NUM_OF_LEVELS = 7;
        public readonly static int HUB_LEVEL_INDEX = 7;
        public readonly static string SUB_LEVEL_STRING = "sublevel";
        public readonly static string NORM_LEVEL_STRING = "level";
        public readonly static string NO_LEVEL_STRING = "none";

        public readonly static int COUNT_PTS_CONST = 50;
        public readonly static int CRUSHING_PTS_CONST = 100;
        /**
         * TODO: INSERT POINT VALUES HERE
         * */

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_10113
{
    internal class GameRule
    {
        internal const int B_WIDTH = 30; // 벽돌 하나의 너비가 몇 픽셀을 차지할 것인지 (벽돌 좌표 한 칸의 폭)
        internal const int B_HEIGHT = 30; // 벽돌 한 칸 높이
        internal const int BX = 12; // 벽돌이 움직일 수 있는 너비.
        internal const int BY = 20; //벽돌이 움직일 수 있는 높이.
        internal const int SX = 4; //시작 x 좌표
        internal const int SY = 0; //시작 y 좌표

        internal const int NextBlockBoxSize = 4;
        internal const int NextBlockBoxStart_X = 15;
        internal const int NextBlockBoxEnd_X = 19;

        internal const int HoldBlockBoxSize = 4;
        internal const int HoldBlockBoxStart_X = 15;
        internal const int HoldBlockBoxEnd_X = 19;
        internal const int HoldBlockBoxStart_Y = 6;
        internal const int HoldBlockBoxEnd_Y = 10;
    }
}

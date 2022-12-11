using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing; // 추가

namespace Tetris_10113
{
    internal class Game
    {
        Diagram now; // null
        Queue<Diagram> diagrams = new Queue<Diagram>();
        Board gboard = Board.GameBoard; // 보드 공간

        internal int this[int x, int y]
        {
            get
            {
                return gboard[x, y];
            }
        }

        internal Queue<Diagram> Diagrams => diagrams;

        internal Color BlockColor => now.BlockColor;

        internal Point NowPosition
        {
            get
            {
                if(now == null) // 도형이 없는경우
                {
                    return new Point(0, 0); //좌표 0,0 반환
                }
                return new Point(now.X, now.Y); //도형이 있는경우 그 도형의 좌표 반환
            }
        }

        internal int BlockNum
        {
            get
            {
                return now.BlockNum; // now - 현재 내려오고 있는 도형을 나타냄.
            }
        }

        internal int Turn
        {
            get { return now.Turn; }
        }

        internal Color[,] AlreadyStoreBlockColor
        {
            get { return gboard.AlreadySroteBlockColor; }
        }

        internal int[,] NextBlockBoard => gboard.NextBlockBoard;

        internal static Game Singleton // static 이므로 유일성이 보장? 클래스에 소속되니까 ? 프로젝트 전반에 걸쳐 해당 클래스는 하나뿐
        {
            get;
            private set;
        }

        static Game() // 정적 생성자 (정적 멤버인 Singleton을 초기화하기 위함)  - 자동호출 (CLR 가상 머신에 의해)
        {
            Singleton = new Game();// 내부에선 인스턴스 생성자 호출
        }
        Game() // 인스턴스 생성자
        {
            for(int i = 0; i < 10; i++)
                diagrams.Enqueue(new Diagram());

            now = diagrams.Dequeue(); // 게임 시작하면서 (게임 인스턴스 만들면서) 도형을 만들고 출발

            SetNextBlock(diagrams.First().BlockNum, diagrams.First().Turn);
        }

        private void SetNextBlock(int bn, int turn)
        {
            for (int xx = 0; xx < 4; xx++)
            {
                for (int yy = 0; yy < 4; yy++)
                {
                    gboard.NextBlockBoard[xx, yy] += BlockValue.bvals[bn, turn, xx, yy];
                }
            }
        }

        internal bool MoveLeft()
        {
            for(int xx = 0; xx<4; xx++)
            {
                for(int yy = 0; yy< 4; yy++)
                {
                    if(BlockValue.bvals[now.BlockNum, Turn, xx,yy] != 0)
                    {
                        if(now.X + xx <= 0) //현재 1이 채워진 블록이 왼쪽벽에 붙어있으면
                        {
                            return false; // 더 이상 이동 못하니 false 반환
                        }
                    }
                }
            }
            if(gboard.MoveEnable(now.BlockNum, Turn, now.X-1, now.Y))
            {
                now.MoveLeft();
                return true;
            }
            return false; //이미 바닥에 고정된 다른 도형에 의해 옆으로 못 가는 상황.
        }

        internal bool MoveRight()
        {
            for(int xx = 0; xx< 4; xx++)
            {
                for(int yy = 0; yy<4; yy++)
                {
                    if(BlockValue.bvals[now.BlockNum, Turn, xx, yy] != 0)
                    {
                        if(now.X + xx +1 >= GameRule.BX)
                        {
                            return false;
                        }
                    }
                }
            }
            if (gboard.MoveEnable(now.BlockNum, Turn, now.X + 1, now.Y))
            {
                now.MoveRight();
                return true;
            }
            return false; //이미 바닥에 고정된 다른 도형에 의해 옆으로 못 가는 상황.
        }

        internal bool MoveDown() //아래로 더 갈 수 있는지 확인 후 가능하면 이동
        {
            for(int xx = 0; xx<4; xx++)
            {
                for(int yy = 0; yy<4; yy++)
                {
                    if(BlockValue.bvals[now.BlockNum, Turn, xx,yy] != 0)
                    {
                        if(now.Y+yy+1 >= GameRule.BY)
                        {
                            gboard.Store(now.BlockNum, Turn, now.X, now.Y); // 벽돌을 밑에 쌓는 작업
                            return false;
                        }
                    }
                }
            }
            if (gboard.MoveEnable(now.BlockNum, Turn, now.X, now.Y+1))
            {
                now.MoveDown();
                return true;
            }
            gboard.Store(now.BlockNum, Turn, now.X, now.Y); 
            return false; //이미 바닥에 고정된 다른 도형에 의해 밑으로 못 가는 상황.
        }

        internal bool MoveTurn()
        {
            for(int xx = 0; xx<4; xx++)
            {
                for(int yy = 0; yy<4; yy++)
                {
                    if(BlockValue.bvals[now.BlockNum, (Turn+1)%4, xx,yy] != 0)
                    {
                        if((now.X +xx<0)||(now.X+xx >= GameRule.BX) || (now.Y + yy >= GameRule.BY))
                        {
                            return false; // 좌우 아래 회전 공간 확인 후 안되면 false반환
                        }
                    }
                }
            }
            if (gboard.MoveEnable(now.BlockNum, (Turn+1) % 4, now.X, now.Y))
            {
                now.MoveTurn();
                return true;
            }
            return false; //이미 바닥에 고정된 다른 도형에 의해 턴하지 못 가는 상황.
        }

        internal bool Next() // 벽돌이 맨밑에 오면 다음 벽돌을 선택
        {
            if (diagrams.Count == 0) diagrams.Enqueue(new Diagram());

            now = diagrams.Dequeue();

            return gboard.MoveEnable(now.BlockNum, Turn, now.X, now.Y); //벽돌이 위까지 차서 새 도형이 나올 공간 없는 경우
        }

        internal void Restart()
        {
            gboard.ClearBoard();
        }
    }
}

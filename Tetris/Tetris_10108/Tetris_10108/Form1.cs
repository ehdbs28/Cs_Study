using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris_10113
{
    public partial class Form1 : Form
    {
        Game game;
        Board board;
        int bx;
        int by;
        int bwidth;
        int bheight;
        int nextBoxSize;
        int nextBoxStart_X;
        int nextBoxEnd_X;
        int holdBoxSize;
        int holdBoxStart_X;
        int holdBoxEnd_X;
        int holdBoxStart_Y;
        int holdBoxEnd_Y;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            game = Game.Singleton;
            board = Board.GameBoard;
            bx = GameRule.BX;
            by = GameRule.BY;
            bwidth = GameRule.B_WIDTH;
            bheight = GameRule.B_HEIGHT;

            nextBoxSize = GameRule.NextBlockBoxSize;
            nextBoxStart_X = GameRule.NextBlockBoxStart_X;
            nextBoxEnd_X = GameRule.NextBlockBoxEnd_X;

            holdBoxSize = GameRule.HoldBlockBoxSize;
            holdBoxStart_X = GameRule.HoldBlockBoxStart_X;
            holdBoxStart_Y = GameRule.HoldBlockBoxStart_Y;
            holdBoxEnd_X = GameRule.HoldBlockBoxEnd_X;
            holdBoxEnd_Y = GameRule.HoldBlockBoxEnd_Y;
            SetClientSizeCore(nextBoxEnd_X * bwidth, by * bheight);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawGraduation(e.Graphics);
            DrawGraduationNextBoard(e.Graphics);
            DrawGraduationHoldBoard(e.Graphics);
            DrawDiagramEndPos(e.Graphics);
            DrawDiagram(e.Graphics);
            DoubleBuffered = true;
            DrawBoard(e.Graphics);
            DrawNextBlockBoard(e.Graphics);
            DrawHoldBlockBoard(e.Graphics);
        }

        private void DrawBoard(Graphics graphics)
        {
            for(int xx = 0; xx< bx; xx++)
            {
                for(int yy = 0; yy<by; yy++)
                {
                    if (game[xx, yy] != 0)
                    {
                        Rectangle now_rt = new Rectangle(xx * bwidth + 2, yy * bheight + 2, bwidth - 4, bheight - 4);
                        graphics.DrawRectangle(Pens.Green, now_rt);
                        graphics.FillRectangle(new SolidBrush(game.AlreadyStoreBlockColor[xx, yy]), now_rt);
                    }
                }
            }
        }

        private void DrawNextBlockBoard(Graphics graphics)
        {
            for(int xx = 0; xx < 4; xx++)
            {
                for(int yy = 0; yy < 4; yy++)
                {
                    if(game.NextBlockBoard[xx, yy] != 0)
                    {
                        Rectangle now_rt = new Rectangle(xx * bwidth + 2 + nextBoxStart_X * bwidth, yy * bheight + 2, bwidth - 4, bheight - 4);
                        graphics.DrawRectangle(Pens.Green, now_rt);
                        graphics.FillRectangle(new SolidBrush(game.Diagrams.First().BlockColor), now_rt);
                    }
                }
            }
        }

        private void DrawHoldBlockBoard(Graphics graphics)
        {
            for (int xx = 0; xx < 4; xx++)
            {
                for (int yy = 0; yy < 4; yy++)
                {
                    if (game.HoldBlockBoard[xx, yy] != 0)
                    {
                        Rectangle now_rt = new Rectangle(xx * bwidth + 2 + holdBoxStart_X * bwidth, yy * bheight + 2 + holdBoxStart_Y * bheight, bwidth - 4, bheight - 4);
                        graphics.DrawRectangle(Pens.Green, now_rt);
                        graphics.FillRectangle(new SolidBrush(game.HoldBlock.BlockColor), now_rt);
                    }
                }
            }
        }

        private void DrawDiagram(Graphics graphics)
        {
            Color diagramColor = game.BlockColor;
            Pen dpen = new Pen(diagramColor, 4);
            Point now = game.NowPosition;
            int bn = game.BlockNum;
            int tn = game.Turn;

            for(int xx = 0; xx<4; xx++)
            {
                for(int yy = 0; yy<4; yy++)
                {
                    if(BlockValue.bvals[bn, tn, xx, yy] != 0)
                    {
                        Rectangle now_rt = new Rectangle((now.X+xx) * bwidth + 2, (now.Y+yy) * bheight + 2, bwidth - 4, bheight - 4);
                        graphics.DrawRectangle(dpen, now_rt);

                    }
                }
            }
        }

        private void DrawDiagramEndPos(Graphics graphics)
        {
            Pen dpen = new Pen(Color.Black, 2);
            Point now = game.NowPosition;
            int bn = game.BlockNum;
            int tn = game.Turn;

            for(int xx = 0; xx < 4; xx++)
            {
                for(int yy = 0; yy < 4; yy++)
                {
                    for(int endY = now.Y; endY < by - 3; endY++)
                    {
                        if (board.MoveEnable(bn, tn, now.X, endY) && endY != 16)
                        {
                            continue;
                        }
                        else
                        {
                            if(BlockValue.bvals[bn, tn, xx, yy] != 0)
                            {
                                int pos_Y = 0;
                                if(endY == 16)
                                {
                                    for (int xxx = 0; xxx < 4; xxx++)
                                    {
                                        for (int yyy = 0; yyy < 4; yyy++)
                                        {
                                            if(yyy == 3)
                                            {
                                                if(xxx == 1 || xxx == 2)
                                                {
                                                    if(BlockValue.bvals[bn, tn, xxx, yyy] == 0)
                                                    {
                                                        pos_Y = endY + yy + 1;
                                                    }
                                                    else
                                                    {
                                                        pos_Y = endY + yy;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    pos_Y = endY + yy - 1;
                                }

                                Rectangle end_rt = new Rectangle((now.X + xx) * bwidth + 2, pos_Y * bheight + 2, bwidth - 4, bheight - 4);
                                graphics.DrawRectangle(dpen, end_rt);
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void DrawGraduation(Graphics graphics)
        {
            DrawHorizons(graphics, 0, by, 0, bx);
            DrawDiagramVerticals(graphics, 0, bx, 0,by);
        }

        private void DrawGraduationNextBoard(Graphics graphics)
        {
            DrawHorizons(graphics, 0, nextBoxSize, nextBoxStart_X, nextBoxEnd_X);
            DrawDiagramVerticals(graphics, nextBoxStart_X, nextBoxEnd_X, 0, nextBoxSize);
        }

        private void DrawGraduationHoldBoard(Graphics graphics)
        {
            DrawHorizons(graphics, holdBoxStart_Y, holdBoxEnd_Y, holdBoxStart_X, holdBoxEnd_X);
            DrawDiagramVerticals(graphics, holdBoxStart_X, holdBoxEnd_X, holdBoxStart_Y, holdBoxEnd_Y);
        }

        private void DrawDiagramVerticals(Graphics graphics, int str_X, int end_X, int str_Y, int end_Y) // 수직선 그리는 메서드
        {
            Point st = new Point(); // 시작점
            Point et = new Point(); // 끝점

            for(int cx = str_X; cx <= end_X; cx++)
            {
                st.X = cx * bwidth;
                st.Y = str_Y * bheight;
                et.X = st.X;
                et.Y = end_Y * bheight;
                graphics.DrawLine(Pens.Blue, st, et);
            }
        }

        private void DrawHorizons(Graphics graphics, int str_Y, int end_Y, int str_X, int end_X) // 수평선 메서드
        {
            Point st = new Point();
            Point et = new Point();

            for(int cy = str_Y; cy <= end_Y; cy++)
            {
                st.X = str_X * bwidth;
                st.Y = cy * bheight;
                et.X = end_X * bwidth;
                et.Y = st.Y;
                graphics.DrawLine(Pens.Brown, st, et);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right: MoveRight(); return;
                case Keys.Left: MoveLeft(); return;
                case Keys.Space: MoveDown(); return;
                case Keys.Up: MoveTurn(); return;
                case Keys.Down: MoveSSDown(); return;
                case Keys.C: Hold(); return;
            }
        }

        private void Hold()
        {
            game.Hold();
        }

        private void MoveSSDown()
        {
            while(game.MoveDown()) //아래로 한 칸 내려간 경우 true가 반환돼서 if문 수행
            {
                Region rg = MakeRegion(0, -1);
                Invalidate(rg);
            }
            EndingCheck();
        }

        private void MoveDown()
        {
            if (game.MoveDown()) //아래로 한 칸 내려간 경우 true가 반환돼서 if문 수행
            {
                Region rg = MakeRegion(0, -1);
                Invalidate(rg);
            }
            else
            {
                EndingCheck();
            }
        }

        private void EndingCheck()
        {
            if (game.Next())
            {
                Invalidate(); // 게임 진행 가능시 무효화
            }
            else
            {
                timer_down.Enabled = false;
                DialogResult re = MessageBox.Show("다시 시작 하시겠습니까?", "재시작", MessageBoxButtons.YesNo);
                if(re == DialogResult.Yes)
                {
                    game.Restart();
                    timer_down.Enabled = true;
                    Invalidate();
                }
                else
                {
                    Close();
                }
            }
        }

        private Region MakeRegion(int cx, int cy) // 이전 좌표의 상대적 위치가 매개변수로 들어옴
        {
            Point now = game.NowPosition; // 움직인 뒤의 현재 좌표
            int bn = game.BlockNum;
            int tn = game.Turn;

            Region region = new Region();

            for(int xx= 0; xx<4; xx++)
            {
                for(int yy= 0; yy<4; yy++)
                {
                    if(BlockValue.bvals[bn, tn, xx, yy] != 0)
                    {
                        Rectangle rect1 = new Rectangle((now.X+xx)*bwidth, (now.Y+yy) *bheight, bwidth, bheight); // 현재 사각형
                        Rectangle rect2 = new Rectangle((now.X +xx+ cx) * bwidth, (now.Y+yy + cy) * bheight, bwidth, bheight); // 이동전 사각형
                        Region rg1 = new Region(rect1);
                        Region rg2 = new Region(rect2);
                        region.Union(rg1); // rg1영역을 region에 저장
                        region.Union(rg2); // rg2영역을 region에 저장

                    }
                }
            }

            return region; // 움직인 후의 현재 도형 영역과 움직이기 이전의 도형 영역을 합쳐 반환
        }

        private Region MakeRegion() //Turn에 대한 영역 작업 해줄 메서드
         {
            Point now = game.NowPosition;
            int bn = game.BlockNum;
            int tn = game.Turn; //현재 몇번째 턴인지
            int oldtn = (tn + 3) % 4; // 이전 턴의 정보

            Region region = new Region();

            for(int xx= 0; xx<4; xx++)
            {
                for(int yy = 0; yy<4; yy++)
                {
                    if(BlockValue.bvals[bn,tn,xx,yy] != 0)
                    {
                        Rectangle rect1 = new Rectangle((now.X + xx) * bwidth, (now.Y + yy) * bheight, bwidth, bheight);
                        Region rg1 = new Region(rect1);
                        region.Union(rg1);
                    }
                    if(BlockValue.bvals[bn,oldtn,xx,yy] != 0)
                    {
                        Rectangle rect1 = new Rectangle((now.X + xx) * bwidth, (now.Y + yy) * bheight, bwidth, bheight);
                        Region rg1 = new Region(rect1);
                        region.Union(rg1);
                    }
                }
            }
            return region;
         }

        private void MoveLeft()
        {
            if (game.MoveLeft())
            {
                Region rg = MakeRegion(1, 0);
                Invalidate(rg);
            }
        }

        private void MoveRight()
        {
            if (game.MoveRight())
            {
                Region rg = MakeRegion(-1, 0);
                Invalidate(rg);
            }
        }

        private void MoveTurn()
        {
            if (game.MoveTurn())
            {
                Region rg = MakeRegion();
                Invalidate(rg);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            MoveDown();
        }
    }
}

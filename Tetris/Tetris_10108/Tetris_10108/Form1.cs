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
            SetClientSizeCore(bx * bwidth, by * bheight);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawGraduation(e.Graphics);
            DrawDiagram(e.Graphics);
            DoubleBuffered = true;
            DrawBoard(e.Graphics);
            DrawDiagramEndPos(e.Graphics);
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
            Pen dpen = new Pen(Color.Black, 4);
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
                                Rectangle end_rt = new Rectangle((now.X + xx) * bwidth + 2, (endY + yy - 1) * bheight + 2, bwidth - 4, bheight - 4);
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
            DrawHorizons(graphics);
            DrawDiagramVerticals(graphics);
        }

        private void DrawDiagramVerticals(Graphics graphics) // 수직선 그리는 메서드
        {
            Point st = new Point(); // 시작점
            Point et = new Point(); // 끝점

            for(int cx = 0; cx < bx; cx++)
            {
                st.X = cx * bwidth;
                st.Y = 0;
                et.X = st.X;
                et.Y = by * bheight;
                graphics.DrawLine(Pens.Blue, st, et);
            }
        }

        private void DrawHorizons(Graphics graphics) // 수평선 메서드
        {
            Point st = new Point();
            Point et = new Point();

            for(int cy = 0; cy <by; cy++)
            {
                st.X = 0;
                st.Y = cy * bheight;
                et.X = bx * bwidth;
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
            }
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

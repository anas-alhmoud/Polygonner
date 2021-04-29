using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            List<Shape> shapeList = new List<Shape>();
            InitializeComponent();
        }

        Shape myA;
        Color sColor = Color.Blue;

        public abstract class Shape
        {
            public int x = 0;
            public int y = 0;
            public int xlineStart;
            public int xlineEnd;
            public Color myColor = Color.Red;

            public int ylineStart;
            public int ylineEnd;

            public GraphicsPath GP { get; set; }
            public abstract Shape drawShape(Graphics g);
        }

        public class UpperPieace: Shape
        {
            public UpperPieace(int start, int end)
            {
                x = start;
                y = end;

                GP = new GraphicsPath();
            }

            public Point lineStart;
            public Point lineEnd;

            public override Shape drawShape(Graphics g)
            {
                Point[] po =
                    {
                    new Point(x , y),
                    new Point(300 + x, y),
                    new Point(300 + x, 100 + y),
                    new Point(200 + x, 100 + y),
                    new Point(200 + x, 150 + y),
                    new Point(100 + x, 150 + y), // start
                    new Point(100 + x, 100 + y), // end
                    new Point(x, 100 + y),
                    new Point(x, y)
                };

                lineStart = new Point(100 + x, 150 + y);
                lineEnd = new Point(100 + x, 100 + y);

                xlineStart = 100 + x;
                ylineStart = 150 + y;

                xlineEnd = 100 + x;
                ylineEnd = 100 + y;

                GP.Reset();
                GP.AddPolygon(po);

                g.DrawPath(new Pen(myColor, 5), GP);

                return this;
            }
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Point[] po2 = 
                {
                    new Point(200,250), // start 
                    new Point(200, 200), // end
                    new Point(100, 200),
                    new Point(100, 300),
                    new Point(400,300), 
                    new Point(400,200),
                    new Point(300,200),
                    new Point(300,250)
            };

            Graphics g = e.Graphics;

            Shape myP;

            if(selected == null)
            {
                myP = new UpperPieace(400, 200);
            } else
            {
                myP = selected;
            }

            myA = myP;

            myP.drawShape(g);

            GraphicsPath myPath2 = new GraphicsPath();
            myPath2.AddPolygon(po2);

            g.DrawPath(new Pen(sColor, 5), myPath2);

        }

        Shape selected = null;
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if(myA.GP.GetBounds().Contains(e.X, e.Y))
            {
                selected = myA;
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            selected = null;
        }
        bool locked = false;
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if(selected != null)
            {
                selected.x = e.X;
                selected.y = e.Y;

                if (selected.xlineStart == 200 && selected.ylineStart == 250 && selected.xlineEnd == 200 && selected.ylineEnd == 200 && !locked)
                {
                    locked = true;
                    sColor = Color.Gold;
                    selected.myColor = Color.Gold;
                    Invalidate();
                } 
                else if ( (selected.xlineStart > 170 && selected.xlineStart < 230)  && (selected.ylineStart > 220 && selected.ylineStart < 280) && (selected.xlineEnd > 170 && selected.xlineEnd < 230) && (selected.ylineEnd > 170 && selected.ylineEnd < 230) && !locked)
                {
                    selected.x = 100;
                    selected.y = 100;

                    locked = true;
                    sColor = Color.Gold;
                    selected.myColor = Color.Gold;
                    Invalidate();
                }
                

                if (!locked)
                {
                    Invalidate();
                }
            }
        }
    }
}

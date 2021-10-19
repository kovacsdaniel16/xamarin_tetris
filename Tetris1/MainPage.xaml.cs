using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Windows.Input;

namespace Tetris1
{
    public class Board //2. lépés a tábla létrehozása
    {

        private int rows;
        private int columns;
        private int score;
        private int line;
        private Tetramino currTetramino;
        private Label[,] blockContolls;

        static private Brush noBrush = Brush.Transparent;

        /*https://docs.microsoft.com/en-us/dotnet/api/xamarin.forms.frame?//view=xamarin-forms*/

        public Board(Grid tetrisGrid) // a. konstruktor
        {
            rows = tetrisGrid.RowDefinitions.Count;
            columns = tetrisGrid.RowDefinitions.Count;
            score = 0;
            line = 0;



            blockContolls = new Label[columns, rows];

            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    blockContolls[i, j] = new Label();
                    blockContolls[i, j].Background = noBrush;
                    //border osztály .netCoreban nem elérhető




                    Grid.SetRow(blockContolls[i, j], j);
                    Grid.SetColumn(blockContolls[i, j], i);
                    tetrisGrid.Children.Add(blockContolls[i, j]);


                }
            }
            currTetramino = new Tetramino();
            currTetraminoDraw();
        }

        private void currTetraminoDraw()
        {
            Point position = currTetramino.getCurrPosition();

            Point[] shape = currTetramino.getCurrShape();

            Brush color = currTetramino.getCurrColor();

            foreach (Point S in shape)
            {
                blockContolls[(int)(S.X + position.X) + ((columns / 2) - 1), (int)(S.Y + position.Y) + 2].Background = color;
            }
        }

        private void currTetraminoErease()
        {
            Point position = currTetramino.getCurrPosition();

            Point[] shape = currTetramino.getCurrShape();

            Brush color = currTetramino.getCurrColor();

            foreach (Point S in shape)
            {
                blockContolls[(int)(S.X + position.X) + ((columns / 2) - 1), (int)(S.Y + position.Y) + 2].Background = color;
            }
        }

        public int getScore()
        {
            return score;
        }

        public int getLine()
        {
            return line;
        }

    }

    public class Tetramino  //1.lépés tetramino osztály látrehozása (tetramino=az alakzat, ami felülről jön... :D)
    {

        private Point currPosition;  //b. változók
        private Point[] currShape; //mátrix a forma tárolására
        private Brush currColor;
        private bool rotate;
        public Tetramino() //a. konstruktor
        {
            currPosition = new Point(0, 0);
            currColor = Brush.Transparent;
            currShape = setRandomShape();
        }
        public Brush getCurrColor() //d. c-ből visszakapott érték
        {
            return currColor;
        }
        public Point getCurrPosition()
        {
            return currPosition; //e. c-ből visszakapott érték
        }

        public Point[] getCurrShape() //f. c-ből visszakapott érték
        {
            return currShape;
        }

        public void moveLeft()
        {
            currPosition.X -= 1; //balra mozgatáskor a koordináta rendszeren 1-et lépjen balra
        }
        public void moveRight()
        {
            currPosition.X += 1;

        }
        public void moveRotate()
        {
            if (rotate) //ha van forgatás
            {
                for (int i = 0; i < currShape.Length; i++)
                {
                    double x = currShape[i].X;
                    currShape[i].X = currShape[i].Y * -1;
                    currShape[i].Y = x;
                }
            }
        }
        public void moveDown()
        {
            currPosition.Y -= 1;

        }



        private Point[] setRandomShape() //c random forma függvénye
        {
            Random rand = new Random();
            switch (rand.Next() % 7)
            {
                case 0: //I
                    rotate = true;
                    currColor = Brush.Cyan;
                    return new Point[]
                    {
                    new Point(0,0),
                    new Point(-1,0),
                    new Point(1,0),
                    new Point(2,0)
                    };
                // ####

                case 1: //J
                    rotate = true;
                    currColor = Brush.Blue;
                    return new Point[]
                                        {
                    new Point(1,-1),
                    new Point(-1,0),
                    new Point(0,0),
                    new Point(1,0)
                                        };
                // #
                // ##
                //  #

                case 2: //L
                    rotate = true;
                    currColor = Brush.Orange;
                    return new Point[]
                                        {
                    new Point(0,0),
                    new Point(-1,0),
                    new Point(1,0),
                    new Point(1,-1)
                                        };

                //###
                //  #

                case 3: //0
                    rotate = false; // a négyzetet nem kell forgatni
                    currColor = Brush.Yellow;
                    return new Point[]
                                        {
                    new Point(0,0),
                    new Point(0,1),
                    new Point(1,0),
                    new Point(1,1)
                                        };

                // ##
                // ##

                case 4: //S
                    rotate = true;
                    currColor = Brush.Green;
                    return new Point[]
                                        {
                    new Point(0,0),
                    new Point(-1,0),
                    new Point(0,-1),
                    new Point(1,0)
                                        };

                // ##
                //  ##

                case 5: //T
                    rotate = true;
                    currColor = Brush.Purple;
                    return new Point[]
                                        {
                    new Point(0,0),
                    new Point(-1,0),
                    new Point(0,-1),
                    new Point(1,0)
                                        };
                //  #
                // ##
                //  #
                case 6: //Z
                    rotate = true;
                    currColor = Brush.Red;
                    return new Point[]
                                        {
                    new Point(0,0),
                    new Point(-1,0),
                    new Point(0,1),
                    new Point(1,1)
                                        };
                default: return null;
            }
        }
    }



    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }


    }
}
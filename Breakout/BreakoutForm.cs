using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using BreakoutClasses;
using NeuralNetwork;
using System.IO;

/// <summary>
/// Classic example of Neural Network
/// Read about in on: https://stevenmiller888.github.io/mind-how-to-build-a-neural-network/
/// Learn about in on: https://www.youtube.com/watch?v=9aHJ-FAzQaE
/// Game idea and architecture of classes is all mine
/// End serri fir mi Englesh:)
/// </summary>

namespace Breakout
{
    public partial class BreakoutForm : Form
    {
        static TextReader wFile = new StreamReader(@"text.txt"); // saved weights
        
        GameLogic newGame = new GameLogic();
        Neural myNetwork = new Neural(wFile, Neural.Sigmoid, Neural.DerivateSigmoid);

        public BreakoutForm()
        {         
            DoubleBuffered = true;
            InitializeComponent(); 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BackColor = Color.FromArgb(255, 25, 129, 181);
            ballPicBox.Image = (Image)new Bitmap(newGame.mBall.Diameter, newGame.mBall.Diameter);

            using (Graphics g = Graphics.FromImage(ballPicBox.Image))
            {
                g.FillEllipse(new SolidBrush(Color.White), 0, 0, newGame.mBall.Diameter, newGame.mBall.Diameter);
            }
            ballPicBox.Invalidate();
            ballPicBox.BackColor = Color.Transparent;
            platformPicBox.Image = (Image)new Bitmap(newGame.mPlatform.PlatformWidth, newGame.mPlatform.PlatformHeight);
            using (Graphics g = Graphics.FromImage(platformPicBox.Image))
            {
                g.FillRectangle(new SolidBrush(Color.White), 0, 0, newGame.mPlatform.PlatformWidth, newGame.mPlatform.PlatformHeight);
            }

            newGame.mBall.DownSide += () =>
            {
                myNetwork.Target = newGame.mBall.X / 600; // it's not just dividing on 600. It's MIN-MAX data normalization, it's so easy just in my case. Go and read about it
                MyChart.Series[0].Points.AddY(Math.Abs(newGame.mBall.X - newGame.mPlatform.X));
                // we can check margins in this over comfortable chart. Enjoy
                Thread thr = new Thread(() =>
                {
                    try
                    {
                        myNetwork.BackPropagation();
                        // we use TRY-CATCH to avoid program crashing in case of some arithmetics errors
                        // it would be really dumb to lose all trained weights just because of one rare trouble
                        // P.S. actually, for 3 neurons there won't be any errors, but i can't affirm it in cause of other count of neurons
                    }
                    catch (ArithmeticException)
                    {
                        MessageBox.Show("NaN");
                    }
                });
                thr.Start();

                if ((newGame.mBall.X + newGame.mBall.Diameter < newGame.mPlatform.X) || (newGame.mBall.X > newGame.mPlatform.X + newGame.mPlatform.PlatformWidth))
                {
                    newGame.mBall.Clear();
                }
                else
                {
                    Tuple<float, float> mTup = newGame.mPlatform.NewDxDy(newGame.mBall.X);
                    newGame.mBall.Dx = mTup.Item1; newGame.mBall.Dy = mTup.Item2;
                    Thread newthr = new Thread(() => { Console.Beep(400, 100); });
                    newthr.Start();
                    return;
                }
            };

            newGame.mData.FullDataEvent += () => {
                myNetwork.Inputs[0] = newGame.mData.secondX / 600;
                float temp = newGame.mData.GenerateCos();
                myNetwork.Inputs[1] = temp;
                Thread thr = new Thread(() => myNetwork.FrontPropagation());
                thr.Start();
            };
            myNetwork.NewOutputEvent += () =>
            {
                newGame.mPlatform.GoToCoordinates(myNetwork.Output*600); // multiply on 600 because of normalization
            };
        }

        private void MainTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                // because of receiving info at the down side and some calculations at the same time
                // program can generate some little bags, but it won't have any influence on the result
                // so we can just pass it
                newGame.mBall.DoMove();
                platformPicBox.Location = new Point((int)newGame.mPlatform.X, 600);
                ballPicBox.Location = new Point((int)newGame.mBall.X, (int)newGame.mBall.Y);
            }
            catch (Exception)
            {    
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Just closing an app doesn't save trained weights
            // you have to do it yourself
            wFile.Close();
            TextWriter txtWrt = new StreamWriter(@"text.txt");
            myNetwork.PrintAllWeightsInFile(txtWrt);
            txtWrt.Close();
           
        }
    }
}

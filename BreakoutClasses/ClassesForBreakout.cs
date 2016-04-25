using System;

/// <summary>
/// Classic example of Neural Network
/// Read about in on: https://stevenmiller888.github.io/mind-how-to-build-a-neural-network/
/// Learn about in on: https://www.youtube.com/watch?v=9aHJ-FAzQaE
/// Game idea and architecture of classes is all mine
/// End serri fir mi Englesh:)
/// </summary>

namespace BreakoutClasses
{
    public class Ball
    {
        public delegate void inDistrict();
        // these events will be worked out in GameLogic
        public event inDistrict LeftRightSide;
        public event inDistrict UpSide;
        public event inDistrict DownSide;
        public event inDistrict CheckingFloorEvent;
        public event inDistrict StartPoint;

        public float firstfloor = 300;
        public float secondfloor = 380; 

        public int Diameter { get; } = 20;
        public float X { get; set; } = 290;
        public float Y { get; set; } = 300;
        public float Dx { get; set; } = 1;
        public float Dy { get; set; } = 5;

        public void DoMove()
        {
            X += Dx;
            Y += Dy;
            if (((Y >= firstfloor-10) && (Y <= firstfloor+10)) || ((Y >= secondfloor-10) && (Y <= secondfloor + 10))) CheckingFloorEvent();
            if (Y >= 0 && Y <= 200) StartPoint();
            if ((X < 1) || (X + Diameter > 599))
            {
                LeftRightSide();
                return;
            }
            if (Y < 1)
            {
                UpSide();
                return;
            }
            if (Y + Diameter > 590)
            {
                DownSide();
                return;
            }
        }
        public void Clear()
        {
            X = 290;
            Y = 5;
            Random rnd = new Random(DateTime.Now.Millisecond);
            Dx = rnd.Next(-5, 5);
            Dy = 6;
        }
    }

    public class Platform
    {
        public int PlatformWidth { get; } = 75;
        public int PlatformHeight { get; } = 20;
        public float X { get; set; } = 300;

        public int Speed { get; set; } = 60;

        public Tuple<float, float> NewDxDy(float x)
        {
            // new vector of movement when ball reachs platform calculates with a bit modified
            // x^3 function that has ZERO in the middle of platform
            float dx = 0; float dy = 0;
            dx = (float)Math.Pow(((x - X) - ((PlatformWidth - 10) / 2)) / ((PlatformWidth - 10) / 2), 3);
            dy = (-1)*(float)Math.Sqrt(Speed - dx * dx); // we should do that do save speed
            return new Tuple<float, float>(dx, dy);
        }
        public void GoToCoordinates(float newX)
        {
            X = newX;// - PlatformWidth / 2;

            // in uncommentared case we move LEFT EXTREME POINT of platform to newX
            // in commentared part we would move MIDDLE of platform to there.
            // first case is good for training of neural network, because when we use
            // second one then in one moment platform will just reflect a ball in 90 degrees all the time
        }
    }

    public class Field
    {
        public int Height { get; } = 600;
        public int Width { get; } = 600;
    }

    public class MovingData
    {
        public float firstX { get; set; } = -1;
        public float secondX { get; set; } = -1;

        public float firstfloor = 300;
        public float secondfloor = 380; // lines where we grab an info about ball to give it to the network

        public delegate void FullData();
        public event FullData FullDataEvent;

        public void Clear()
        {
            firstX = -1;
            secondX = -1;
        }
        public void AddCoords(float x, float y)
        {
            if ((y >= firstfloor - 10) && (y <= firstfloor + 10) && (secondX == -1))
            {
                firstX = x;
                return;
            }
            if ((y >= secondfloor - 10) && (y <= secondfloor + 10) && (secondX == -1))
            {
                secondX = x;
                FullDataEvent();
                return;
            }
            else return;
        }
        public float GenerateCos()
        {
            double a = firstX+5;
            double b = Math.Sqrt(Math.Pow(secondX - firstX, 2) + 640);
            double c = Math.Sqrt(Math.Pow(secondX + 5, 2) + 640);
            
            float res = (float)((c * c - a * a - b * b) / ((-2) * a * b));
            return res;
        }
    }

    public class GameLogic
    {
        public Field mField { get; set; } = new Field();
        public Ball mBall { get; set; } = new Ball();
        public Platform mPlatform { get; set; } = new Platform();
        public MovingData mData { get; set; } = new MovingData();

        public delegate bool Failure();
        public event Failure FailEvent;

        public GameLogic()
        {
            mBall.LeftRightSide += () => { mBall.Dx *= (-1); };
            mBall.UpSide += () => { mBall.Dy *= (-1); };
            
            mBall.StartPoint += () => mData.Clear();
            mBall.CheckingFloorEvent += () => {
                mData.AddCoords(mBall.X, mBall.Y);
            };
        }

    }
}

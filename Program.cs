using System;
using System.Collections.Generic;
using System.Threading;

namespace snake
{
    class Program
    {
        enum Direction
        {
            Right,
            Up,
            Left,
            Down
        }
        struct Position
        {
            public int x, y;
            public Position GetPosition()
            {
                return this;
            }
        }
        const int BorderX = 40;
        const int BorderY = 20;
        static Direction direction = 0;
        static Queue<Position> snakeBody;
        static Position snakeHead;
        static Position fruit;
        static bool snakeAlive;
        static int score;
        static void Main(string[] args)
        {
            Console.Clear();
            Console.SetCursorPosition(10, 10);
            Console.WriteLine("Press any key to start...");
            Console.ReadKey();
            StartGame();
            DrawBorder();
            GenerateFruit();
            Timer timer = new Timer(GameCycle, null, 0, 500);
            while (snakeAlive)
            {
                SetDirection(Console.ReadKey().Key);
                System.Threading.Thread.Sleep(100);
            }
            timer.Dispose();
            GameOver();
            Console.ReadKey();
        }
        static void GameCycle(Object o)
        {
            if (HasIntersectionWithBorder())
            {
                snakeAlive = false;
                return;
            }
            else if (HasIntersectionWithFruit())
            {
                GrownUp();
                Console.Beep(1500, 100);
                GenerateFruit();
            }
            else
            {
                Move();
                Console.Beep(800, 65);
            }
            Clear();
            Draw();
            score += 1;
        }
        static void StartGame()
        {
            snakeBody = new Queue<Position>();
            Position segment;
            segment.x = 10;
            segment.y = 10;
            snakeHead.x = segment.x;
            snakeHead.y = segment.y;
            for (int i = 1; i < 3; i++)
            {
                segment.x = 10 + i;
                segment.y = 10;
                snakeBody.Enqueue(segment);
            }
            snakeAlive = true;
            score = 0;
        }
        static bool HasIntersectionWithBorder()
        {
            foreach(Position segment in snakeBody)
            {
                if (snakeHead.x == segment.x && snakeHead.y == segment.y)
                {
                    return true;
                }
            }
            if (snakeHead.x == 0 || snakeHead.x == BorderX || snakeHead.y == 0 || snakeHead.y == BorderY)
            {
                return true;
            }
            return false;
        }
        static bool HasIntersectionWithFruit()
        {
            if (snakeHead.x == fruit.x && snakeHead.y == fruit.y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static void GenerateFruit()
        {
            Position position;
            position.x = 1;
            position.y = 1;
            var randomizer = new Random();
            bool rightPosition = false;
            while(!rightPosition)
            {
                rightPosition = true;
                position.x = randomizer.Next(1, BorderX);
                position.y = randomizer.Next(1, BorderY);
                foreach (Position segment in snakeBody)
                {
                    if (position.x == segment.x && position.y == segment.y)
                    {
                        rightPosition = false;
                    }
                }
            }
            fruit = position;
        }
        static void Move()
        {
            snakeBody.Dequeue();
            snakeBody.Enqueue(snakeHead);
            switch(direction)
            {
                case Direction.Up:
                    snakeHead.y--;
                    break;
                case Direction.Down:
                    snakeHead.y++;
                    break;
                case Direction.Left:
                    snakeHead.x--;
                    break;
                case Direction.Right:
                    snakeHead.x++;
                    break;
            }
        }
        static void GrownUp()
        {
            score += 100;
            snakeBody.Enqueue(snakeHead);
            switch (direction)
            {
                case Direction.Up:
                    snakeHead.y--;
                    break;
                case Direction.Down:
                    snakeHead.y++;
                    break;
                case Direction.Left:
                    snakeHead.x--;
                    break;
                case Direction.Right:
                    snakeHead.x++;
                    break;
            // 
            }
        }
        static void GameOver()
        {
            Console.SetCursorPosition(snakeHead.x, snakeHead.y);
            Console.Write('X');
            Console.SetCursorPosition(22, 10);
            Console.Write("Lol, u ded");
        }
        static void SetDirection(ConsoleKey key)
        {
            switch(key)
            {
                case ConsoleKey.UpArrow:
                    direction = Direction.Up;
                    break;
                case ConsoleKey.DownArrow:
                    direction = Direction.Down;
                    break;
                case ConsoleKey.LeftArrow:
                    direction = Direction.Left;
                    break;
                case ConsoleKey.RightArrow:
                    direction = Direction.Right;
                    break;
            }
        }
        static void Draw()
        {

            Console.SetCursorPosition(snakeHead.x, snakeHead.y);
            switch(direction)
            {
                case Direction.Up:
                    Console.Write('╩');
                    break;
                case Direction.Down:
                    Console.Write('╦');
                    break;
                case Direction.Left:
                    Console.Write('╣');
                    break;
                case Direction.Right:
                    Console.Write('╠');
                    break;
            }
            foreach(Position segment in snakeBody)
            {
                Console.SetCursorPosition(segment.x, segment.y);
                Console.Write('█');
            }

            Console.SetCursorPosition(fruit.x, fruit.y);
            Console.Write('¤');

            Console.SetCursorPosition(45, 5);
            Console.Write("Очки");
            Console.SetCursorPosition(45, 6);
            Console.Write(score);
        }
        static void Clear()
        {
            for (int i = 1; i < BorderX; i++)
            {
                for (int j = 1; j < BorderY; j++)
                {
                    Console.SetCursorPosition(i, j);
                    Console.Write(' ');
                }
            }
        }
        static void DrawBorder()
        {
            for (int i = 0; i <= BorderX; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write('▓');
                Console.SetCursorPosition(i, BorderY);
                Console.Write('▓');
            }
            for (int i = 0; i <= BorderY; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write('▓');
                Console.SetCursorPosition(BorderX, i);
                Console.Write('▓');
            }
        }
    }
}

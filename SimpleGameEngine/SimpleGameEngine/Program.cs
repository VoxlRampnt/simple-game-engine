using System;
using static System.Console;

class Program
{
    const int WIDTH = 20;
    const int HEIGHT = 20;

    //the speed increase to the game after picking up a pickup.
    const double SPEED_INCREASE = 0.1;

    //the speed the game will start at.
    const double START_SPEED = 3.5;

    private static Canvas canvas;
    private static Snake snake;
    private static Pickup pickup;
    private static int score;

    static void Main()
    {
        //Initializing our variables
        canvas = new Canvas(WIDTH, HEIGHT);
        snake = new Snake(WIDTH / 2, HEIGHT / 2, START_SPEED);
        pickup = new Pickup(WIDTH, HEIGHT);
        score = 0;

        //Drawing our snake on the canvas
        canvas.Draw(snake.X, snake.Y, 'O');

        //Writing the score at the top of the canvas
        canvas.WriteMessageTop("Score: 0");

        //Hiding the cursor in the console.
        Console.CursorVisible = false;

        //Starting the GameLoop
        GameLoop();
    }

    static void GameLoop()
    {
        //Create game loop
        while (true)
        {
            Time.Update();
            Input.Update();

            //check if esc was pressed so we can exit the game
            if (Input.KeyPressed == InputType.ESC)
            {
                canvas.WriteMessageBottom("Exiting Game...");
                WriteLine();
                break;
            }

            
            int prevSnakeX, prevSnakeY;
            prevSnakeX = snake.X;
            prevSnakeY = snake.Y;
            /
            if (snake.X != prevSnakeX || snake.Y != prevSnakeY)
            {

                if (snake.X == pickup.X & snake.Y == pickup.Y)
                {
                    score += 100;
                    canvas.WriteMessageTop(score + "");
                    snake.Speed += SPEED_INCREASE;
                    snake.AddSegment();
                    pickup.SetPosition();
                }


                

                //This method call will update the positions of all the snake segments.
                snake.UpdateSegments();
                if (IsSnakeInBounds() == false || snake.IsHeadTouchingBody() == true)
                {
                    canvas.WriteMessageBottom("Game Over: " + "/n" + "Score" + score);
                    Console.WriteLine();
                    Console.Beep();
                    break;
                }

               

                //draw pickup
                Clear();
                foreach (SnakeSegment segment in snake.Segments)
                {
                    canvas.Draw(segment.X, segment.Y, segment.Character);
                }
                canvas.Draw(pickup.X, pickup.Y, '', ConsoleColor.Red);
            }
        }
    }

    static bool IsSnakeInBounds()
    {
        
        if (snake.X == 0 | snake.X == WIDTH | snake.Y == 0 | snake.Y == HEIGHT) 
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }
}
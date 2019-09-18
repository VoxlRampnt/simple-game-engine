using System;
using System.Collections.Generic;
using static System.Console;

public class Snake{
    private double x; //the x position of the snake
    private double y; //the y position of the snake

    private int xDir = 0; //the x direction of the snake

    private int yDir = 1; //the y direction of the snake

    private double speed; //the speed that the snake is moving at

    /*  A List holding all the SnakeSegments of the snake.
        List are like arrays, but you don't have to set a size.
        The name between <> specifies the type List stores. */
    private List<SnakeSegment> segments = new List<SnakeSegment>();

    public Snake(int x, int y, double speed)
    {
        /*  ------------------------------------------
            2.1
            ------------------------------------------   
            Initialize the x, y and speed fields 
            using the values passed to the constructor
        */
        this.x = x;
        this.y = y;
        this.speed = speed;
        
        /*  Adds a segment to the segments List
            This segment will serve as the head of the snake. */
        segments.Add(new SnakeSegment(x,y,'O'));
    }

    /*  You will notice that we round the double x and y fields
        to integers before returning them as ints in our properties
        below.

        Our snake's x and y positions will only be increasing by fractions
        with every execution of our GameLoop (see the Update method in this class). 
        Our game canvas only supports whole numbers. 
        
        Returning our positions in this way means we keep the precision
        of our positions, but only expose whole numbers the can place our
        snake correctly on our canvas */
    public int X {
        /*  Math.Floor will round our value 
            down to the nearest whole number. 
            We still have to cast it to an int */
        get {return (int) Math.Floor(x);}
    }

    public int Y {
        get {return (int) Math.Floor(y);}
    }

    public double Speed {
        get { return speed; }
        set { speed = value; }
    }

    public List<SnakeSegment> Segments {
        get { return segments; }
    }

    public void AddSegment()
    {
        
        SnakeSegment lastSegment = segments[segments.Count];
        segments.Add(new SnakeSegment(lastSegment.PrevX, lastSegment.PrevY, 'o'));
    }

    public void UpdateSegments(){
       
        
        segments[0].X = (int)x;
        segments[0].Y = (int)y;
        for (int i = 0; i < segments.Count; i++)
        {
            segments[i].X = segments[i + 1].PrevX;
            segments[i].Y = segments[i + 1].PrevY;
        }
    }

    public bool IsHeadTouchingBody(){
        
        for (int i = 0; i < segments.Count; i++)
        {
            if (segments[0].X == segments[i].X && segments[0].Y == segments[i].Y)
            {
                return true;
            }
        }
        
        return false;
    }

    public void Update(){
        if(Input.KeyPressed == InputType.UP){
            yDir = -1;
            xDir = 0;
        }
        else if(Input.KeyPressed == InputType.DOWN){
            yDir = 1;
            xDir = 0;
        }
        
        if (Input.KeyPressed == InputType.RIGHT)
        {
            yDir = 0;
            xDir = 1;
        }
        else if (Input.KeyPressed == InputType.LEFT)
        {
            yDir = 0;
            xDir = -1;
        }

        
        x = xDir * speed * Time.DeltaTime;
        y = yDir * speed * Time.DeltaTime;
    }
}
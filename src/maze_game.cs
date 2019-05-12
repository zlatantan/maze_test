using System;

public class Example
{   
    const string wall = "■";
    const string path = "□";

    public static void Main()
    {   
        

        string  [,] map =  {
        {"■","■","■","■","■","■","■","■","■","■","■",},
        {"■","□","□","□","□","□","□","□","□","□","■",},
        {"■","□","■","□","■","□","■","□","■","□","■",},
        {"■","□","□","□","□","□","□","□","□","□","■",},
        {"■","□","■","□","■","□","■","□","■","□","■",},
        {"■","□","□","□","□","□","□","□","□","□","■",},
        {"■","□","■","□","■","□","■","□","■","□","■",},
        {"■","□","□","□","□","□","□","□","□","□","■",},
        {"■","□","■","□","■","□","■","□","■","□","■",},
        {"■","□","□","□","□","□","□","□","□","□","■",},
        {"■","■","■","■","■","■","■","■","■","■","■",}};
    
         
        var rnd = new Random();
        for (int x = 2; x < map.GetLength(0) - 1; x+=2)
        {   
            for (int y = 2; y < map.GetLength(0) -1; y+=2)
            {   
                int direction = rnd.Next(4);
                switch (direction)
                {
                    case 0:
                        map[x+1,y] = wall; 
                        break;
                    case 1:
                        map[x,y-1] = wall;
                        break;
                    case 2:
                        map[x,y+1] = wall;
                        break;
                    //case 3:
                    //    map[x-1,y] = wall;
                    //    break;
                }
                
            }
        }

        MazeEntrance(map);

        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(0); j++)
            {
                Console.Write(map[i,j]);
            }
                Console.Write("\n");
        }
    }

    public static void MazeEntrance(string[,] map)
    {

        var rnd = new Random();

        int entrance ;//入口の場所
        int maze_exit; //出口の場所

        do
        {
            entrance = rnd.Next(1,10);
        } while (map[1,entrance] == wall);

        do
        {
            maze_exit = rnd.Next(1,10);
        } while (map[9,maze_exit] == wall);

            map[0,entrance] = path;
            map[10,maze_exit] = path;
        
    }
}
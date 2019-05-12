using System;

public class Example
{   
    const string WALL = "■";
    const string PATH = "□";
    const int MAP_HEIGHT = 11;
    const int MAP_WIDTH = 11;

    /*メイン関数 */
    public static void Main()
    {   
        string[,] map = new string[MAP_HEIGHT,MAP_WIDTH];

        //マップ生成
        MazeInit(map);

        //迷路生成
        MazeGenerate(map); 
        
        //入口出口生成
        MazeEntrance(map);

        //画面描写
        MazeView(map);
        


        Console.Write(":何かキーを押してください");
        Console.ReadLine();
    }

    /*マップの入り口と出口を作る関数 */
    public static void MazeEntrance(string[,] map)
    {
        var rnd = new Random();

        int entrance ;//入口の場所
        int exit; //出口の場所

        //指定した入口の"下"のマスが壁の時は位置を再設定、出口の時は"上"のマス
        do
        {
            entrance = rnd.Next(1,map.GetLength(0) -1);
        } while (map[1,entrance] == WALL);

        do
        {
            exit = rnd.Next(1,map.GetLength(0) -1);
        } while (map[9,exit] == WALL);

            map[0,entrance] = PATH;
            map[map.GetLength(0) -1,exit] = PATH;
    }

    /*初期マップ生成関数*/
    public static void MazeInit(string[,] map)
    {   
        for (int x = 0; x < MAP_HEIGHT; x++)
        {
            for (int y = 0; y < MAP_WIDTH; y++)
            {
                if (x == 0 || y == 0 || x == MAP_HEIGHT -1 || y == MAP_WIDTH -1 )
                {
                    map[x,y] = WALL; //外周は全部壁
                }
                else
                {
                    map[x,y] = PATH; //それ以外は道    
                }       
            }
        }
    }

    /*迷路生成関数 */
    public static void MazeGenerate(string[,] map)
    {
        var rnd = new Random();
        for (int x = 2; x < map.GetLength(0) - 1; x+=2)
        {   
            for (int y = 2; y < map.GetLength(0) -1; y+=2)
            {   
                map[x,y] = WALL; //[ x * 2 , y * 2 }に柱をたてる
                //柱からランダムに下、左、右に壁を立てる
                int direction = rnd.Next(4);　
                switch (direction)
                {
                    case 0:
                        map[x+1,y] = WALL;  //下
                        break;
                    case 1:
                        map[x,y-1] = WALL;  //左
                        break;
                    case 2:
                        map[x,y+1] = WALL;  //右
                        break;
                }       
            }
        }
    }

    /*画面描写関数*/
    public static void MazeView(string[,] map)
    {
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(0); j++)
            {
                Console.Write(map[i,j]);
            }
                Console.Write("\n");
        }
    }
}
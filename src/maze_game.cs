using System;

public class Example
{   
    const string PLAYER = "Ｐ";
    const string WALL = "■";
    const string PATH = "□";
    const string START = "Ｓ";
    const string GOAL = "Ｇ";
    const string OUT_OF_MAP = "　";
    const int MAP_HEIGHT = 55;
    const int MAP_WIDTH = 55;
    
    public struct stPlayer
    {   //縦軸:x 横軸:y
        public int x;
        public int y;
    }

    public static stPlayer p_position = new stPlayer();
    static string[,] map = new string[MAP_HEIGHT,MAP_WIDTH];

    /*メイン関数 */
    public static void Main()
    {   
        //マップ生成
        MazeInit();

        //迷路生成
        MazeGenerate(); 
        
        //入口出口生成
        MazeEntrance();

        bool End_Flag = false;
        while(End_Flag == false) {
            Console.Write("プレイヤー現在位置 :" + "y :" + p_position.y + "x :" + p_position.x + "\n");
            //画面描写
            PlayerMapView();

            ConsoleKeyInfo a = Console.ReadKey();
            Console.Write("\n");
            int moveX;
            int moveY;
            switch(a.Key){
                case ConsoleKey.UpArrow:
                    moveX = p_position.x;
                    moveY = p_position.y - 1;
                    break;
             case ConsoleKey.DownArrow:
                    moveX = p_position.x;
                    moveY = p_position.y + 1;
                    break;
                case ConsoleKey.RightArrow:
                    moveX = p_position.x + 1;
                    moveY = p_position.y;
                    break;
                case ConsoleKey.LeftArrow:
                    moveX = p_position.x - 1;
                    moveY = p_position.y;
                    break;
                default:
                    continue;
            }

            var moveResult = MoveCheck(moveY,moveX);

            if (moveResult == 0){
                p_position.x = moveX;
                p_position.y = moveY;
            }else if (moveResult == 1){
                End_Flag =true;
            }else{
                Console.Write("移動できへんで\n");
            }
        }

        //クリア処理
        Console.Write("クリアです\n");
        MazeView();

        Console.Write(":何かキーを押してください");
        Console.ReadLine();
    }

    /*マップの入り口と出口を作る関数 */
    public static void MazeEntrance()
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
        } while (map[map.GetLength(0) -2,exit] == WALL);

            map[0,entrance] = START;
            p_position.x = entrance;
            p_position.y = 1;
            map[map.GetLength(0) -1,exit] = GOAL;
    }

    /*初期マップ生成関数*/
    public static void MazeInit()
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
    public static void MazeGenerate()
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
    public static void MazeView()
    {
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(0); j++)
            {
                if (p_position.x == j  && p_position.y == i){
                    Console.Write(PLAYER);
                }
                else{
                Console.Write(map[i,j]);
                }
            }
                Console.Write("\n");
        }
    }

    public static void PlayerMapView()
    {
        for (int i = p_position.y -2; i <= p_position.y +2; i++)
        {
            for (int j = p_position.x -2; j <= p_position.x +2; j++)
            {
                if (p_position.x == j  && p_position.y == i){
                    Console.Write(PLAYER);
                }
                else if (i < 0 || j < 0 || i >= map.GetLength(0) || j >= map.GetLength(0)){
                    Console.Write(OUT_OF_MAP);
                }
                else{
                Console.Write(map[i,j]);
                }
            }
                Console.Write("\n");
        }
    }

    /*入力判定*/
    public static int MoveCheck(int y,int x)
    {
        int retVal;
        if (map[y,x] == PATH){
            retVal = 0;
        }else if (map[y,x] == GOAL){
            retVal = 1;
        }else{
            retVal = 2;
        }
        return retVal;
    }
}
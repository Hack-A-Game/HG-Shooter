using UnityEngine;
using System.Collections;

public class Constants
{
    public static byte LAYER_WALL = 9;
    public static int LAYER_WALL_MASK = 1 << (LAYER_WALL - 1);

    public static byte LAYER_PLAYER = 10;
    public static int LAYER_PLAYER_MASK = 1 << (LAYER_PLAYER - 1);

    public static byte LAYER_ENEMY = 11;
    public static int LAYER_ENEMY_MASK = 1 << (LAYER_ENEMY - 1);
}


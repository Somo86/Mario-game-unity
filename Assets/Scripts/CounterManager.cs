using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CounterManager
{
    // Start is called before the first frame update
    private static int score = 0;
    private static int coins = 0;

    public static void addScore(int points)
    {
        score = score + points;
    }

    public static int getScore()
    {
        return score;
    }

    public static int getCoins()
    {
        return coins;
    }
    
    public static void addCoin()
    {
        coins = coins + 1;
    }

    public static void restartValues()
    {
        score = 0;
        coins = 0;
    }
}

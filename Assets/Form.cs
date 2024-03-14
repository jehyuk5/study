using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Tool
{
    public static string GetGameTag(GameTag _value)
    {
        return _value.ToString();
    }

    public static bool IsEneterFirstScence = false;//첫 씬이 내가 원하는 씬이 아닐시 다시 원하는 씬으로 가게하는 코드
    //단 다른 씬부터 시작하지는 못 하게한다
    //매니저와 다른 스크립트에도 넣어야한다
}

public enum GameTag
{
    None,
    Enemy,
    Player,
    Item
}

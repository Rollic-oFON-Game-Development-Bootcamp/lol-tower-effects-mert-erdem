using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySingleton
{
    private static MySingleton instance;
    public static MySingleton Instance => instance ?? (instance = new MySingleton());

    private MySingleton()
    {

    }
}

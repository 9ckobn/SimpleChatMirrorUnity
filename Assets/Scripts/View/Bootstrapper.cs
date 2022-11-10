using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    Game _game;

    void Start()
    {
        _game = new Game();

        DontDestroyOnLoad(gameObject);
    }
}

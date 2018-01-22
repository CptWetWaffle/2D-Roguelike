using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    private BoardManager _boardManager;

    private int _level;

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if(instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        _level = 3;
        _boardManager = GetComponent<BoardManager>();
        InitGame();
    }

    private void InitGame()
    {
        _boardManager.SetupScene(_level);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

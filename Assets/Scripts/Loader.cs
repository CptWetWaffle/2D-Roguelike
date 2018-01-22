using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    public GameObject GameManagerPrefab;

    // Use this for initialization
    void Awake()
    {
        if (GameManager.instance == null)
            Instantiate(GameManagerPrefab);
    }
}

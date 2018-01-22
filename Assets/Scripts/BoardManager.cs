using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
    [Serializable]
    public class Count
    {
        public int Minimum { get; set; }
        public int Maximum { get; set; }

        public Count(int minimum, int maximum)
        {
            this.Minimum = minimum;
            this.Maximum = maximum;
        }
    }

    public int Columns;
    public int Rows;
    public GameObject Exit;
    public GameObject[] FloorTiles, WallTiles, FoodTiles, EnemyTiles, OuterWallTiles;
    private Count _wallCount;
    private Count _foodCount;
    private Transform _boardHolder;
    private IList<Vector3> _gridPositions;

    private BoardManager()
    {
        _wallCount = new Count(5, 9);
        _foodCount = new Count(1, 5);
        _gridPositions = new List<Vector3>();
    }

    private void InitializeList()
    {
        _gridPositions.Clear();

        for (int x = 1; x < Columns - 1; x++)
            for (int y = 1; y < Rows - 1; y++)
            {
                _gridPositions.Add(new Vector3(x, y));
            }
    }

    private void BoardSetup()
    {
        _boardHolder = new GameObject("Board").transform;
        for (int x = -1; x < Columns + 1; x++)
            for (int y = -1; y < Rows + 1; y++)
            {
                GameObject toInstanciate;
                if (x == -1 || x == Columns || y == -1 || y == Rows)
                    toInstanciate = OuterWallTiles[Random.Range(0, OuterWallTiles.Length)];
                else
                    toInstanciate = FloorTiles[Random.Range(0, FloorTiles.Length)];

                var instance = Instantiate(toInstanciate, new Vector3(x,y), Quaternion.identity) as GameObject;

                instance.transform.parent = _boardHolder;
            }
    }

    private Vector3 RandomPosition()
    {
        int randomIndex = Random.Range(0, _gridPositions.Count);
        Vector3 randomPosition = _gridPositions[randomIndex];
        _gridPositions.RemoveAt(randomIndex);
        return randomPosition;
    }

    private void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
    {
        int objectCount = Random.Range(minimum, maximum + 1);

        for (int i = 0; i < objectCount; i++)
        {
            Vector3 randomPosition = RandomPosition();
            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
            Instantiate(tileChoice, randomPosition, Quaternion.identity);
        }
    }

    public void SetupScene(int level)
    {
        BoardSetup();
        InitializeList();
        LayoutObjectAtRandom(WallTiles, _wallCount.Minimum, _wallCount.Maximum);
        LayoutObjectAtRandom(FoodTiles, _foodCount.Minimum, _foodCount.Maximum);
        var enemyCount = (int) Mathf.Log(level, 2f);
        LayoutObjectAtRandom(EnemyTiles, enemyCount, enemyCount);
        Instantiate(Exit, new Vector3(Rows - 1, Columns - 1), Quaternion.identity);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] GameObject _bgTilePref;

    [SerializeField] private Candy[] _candies;
    [SerializeField] private Candy[,] _allCandies;
    private void Start()
    {
        _allCandies = new Candy[width , height];
        Setup();
    }
    private void Setup()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // TODO: OBJECT POOLING
                Vector2 pos = new Vector2(x, y);
                GameObject bgTile = Instantiate(_bgTilePref, pos, Quaternion.identity);
                bgTile.transform.parent = transform;
                bgTile.name = "BG Tile -" + x + ", " + y;

                int candyToUse = Random.Range(0, _candies.Length);
                SpawnCandy(new Vector2Int(x, y), _candies[candyToUse]);

            }
        }
    }

    private void SpawnCandy(Vector2Int spawnPosition,Candy candyToSpawn)
    {
        Candy candy = Instantiate(candyToSpawn, new Vector3(spawnPosition.x,spawnPosition.y,0f), Quaternion.identity);
        candy.transform.parent = this.transform;
        candy.name = "Candy - " + spawnPosition.x + ", " + spawnPosition.y;
        _allCandies[spawnPosition.x, spawnPosition.y] = candy;
        candy.SetupCandy(spawnPosition, this);
    }
}

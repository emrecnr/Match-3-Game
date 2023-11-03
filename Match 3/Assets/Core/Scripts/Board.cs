using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int width;
    public int height;
    [SerializeField] GameObject _bgTilePref;

    [SerializeField] private Candy[] _candies;
    public Candy[,] _allCandies;

    public float candySpeed;

    public MatchFinder _matchFinder;
    private void Start()
    {
        _allCandies = new Candy[width, height];
        Setup();
        _matchFinder = FindObjectOfType<MatchFinder>();
    }
    private void Update()
    {
        _matchFinder.FindAllMatches();
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

                int iterations = 0;
                while (MatchesAt(new Vector2Int(x, y), _candies[candyToUse]) && iterations < 100)
                {
                    candyToUse = Random.Range(0, _candies.Length);
                    iterations++;                    
                }
                SpawnCandy(new Vector2Int(x, y), _candies[candyToUse]);

            }
        }
    }

    private void SpawnCandy(Vector2Int spawnPosition, Candy candyToSpawn)
    {
        Candy candy = Instantiate(candyToSpawn, new Vector3(spawnPosition.x, spawnPosition.y, 0f), Quaternion.identity);
        candy.transform.parent = this.transform;
        candy.name = "Candy - " + spawnPosition.x + ", " + spawnPosition.y;
        _allCandies[spawnPosition.x, spawnPosition.y] = candy;
        candy.SetupCandy(spawnPosition, this);
    }
    private bool MatchesAt(Vector2Int positionToCheck, Candy candyToCheck)
    {
        if (positionToCheck.x > 1)
        {
            if (_allCandies[positionToCheck.x - 1, positionToCheck.y].type == candyToCheck.type && _allCandies[positionToCheck.x - 2, positionToCheck.y].type == candyToCheck.type)
                return true;
        }
        if (positionToCheck.y > 1)
        {
            if (_allCandies[positionToCheck.x, positionToCheck.y - 1].type == candyToCheck.type && _allCandies[positionToCheck.x, positionToCheck.y - 2].type == candyToCheck.type)
                return true;
        }
        return false;
    }
    private void DestroyMatchedGemAt(Vector2Int position)
    {
        // TODO: Object Pooling
        if (_allCandies[position.x,position.y] != null)
        {
            if (_allCandies[position.x,position.y].isMatched)
            {
                Destroy(_allCandies[position.x, position.y].gameObject);
                _allCandies[position.x, position.y] = null;
            }
        }
    }
    public void DestroyMatches()
    {
        for (int i = 0; i < _matchFinder.currentMatches.Count; i++)
        {
            if (_matchFinder.currentMatches[i] != null)
            {
                DestroyMatchedGemAt(_matchFinder.currentMatches[i].posIndex);
            }
        }
        StartCoroutine(DecreaseRowCr());
    }

    private IEnumerator DecreaseRowCr()
    {
        yield return new WaitForSeconds(.25f);
        int nullCount = 0;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (_allCandies[x,y] == null)
                {
                    nullCount++;
                }
                else if(nullCount > 0)
                {
                    _allCandies[x, y].posIndex.y -= nullCount;
                    _allCandies[x,y-nullCount] = _allCandies[x,y];
                    _allCandies[x, y] = null;
                }
            }
            nullCount = 0;
        }
    }
}

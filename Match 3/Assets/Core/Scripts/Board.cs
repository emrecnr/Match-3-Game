using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using UnityEngine.UIElements;

public class Board : MonoBehaviour
{
    public int width;
    public int height;
    [SerializeField] private List<GameObject> _bgTilePool = new List<GameObject>();

    [SerializeField] private Candy[] _candies;
    [SerializeField] private List<Candy> _candyPool;
    [SerializeField] private List<Candy> _bombPool;
    public Candy[,] _allCandies;

    public float candySpeed;

    public MatchFinder _matchFinder;
    [SerializeField] private RoundManager _roundManager;

    public enum BoardState { wait, move }
    public BoardState currentState = BoardState.move;
    public Candy _bomb;
    public float bombChance = 2f;
    private void Start()
    {
        _allCandies = new Candy[width, height];
        Setup();
        _matchFinder = FindObjectOfType<MatchFinder>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShuffleBoard();
            print("s");
        }
    }
    private void Setup()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2 pos = new Vector2(x, y);
                GameObject bgTile = GetPooledBgTile();
                SpawnTile(bgTile, pos);
                int candyToUse = Random.Range(0, _candyPool.Count);//


                int iterations = 0;

                while (MatchesAt(new Vector2Int(x, y), _candyPool[candyToUse]) && iterations < 100)
                {
                    candyToUse = Random.Range(0, _candyPool.Count);
                    iterations++;
                }


                if (Random.Range(0, 100f) < bombChance)
                {
                    int bombToUse = Random.Range(0, _bombPool.Count);
                    SpawnCandy(new Vector2Int(x, y), _bombPool[bombToUse], true);
                }
                else
                {

                    SpawnCandy(new Vector2Int(x, y), _candyPool[candyToUse], false);
                }
            }
        }
    }



    private void SpawnCandy(Vector2Int spawnPosition, Candy candyToSpawn, bool isBomb = false)
    {
        if (isBomb)
        {
            _bombPool.Remove(candyToSpawn);
            candyToSpawn.name = "Bomb - " + spawnPosition.x + ", " + spawnPosition.y;
        }

        else
        {
            if (candyToSpawn != null && _candyPool.Contains(candyToSpawn))
            {
                _candyPool.Remove(candyToSpawn);
                candyToSpawn.name = "Candy - " + spawnPosition.x + ", " + spawnPosition.y;
            }
        }
        candyToSpawn.transform.position = new Vector3(spawnPosition.x, spawnPosition.y + height, 0f);
        candyToSpawn.gameObject.SetActive(true);
        _allCandies[spawnPosition.x, spawnPosition.y] = candyToSpawn;
        candyToSpawn.SetupCandy(spawnPosition, this);
    }
    private GameObject GetPooledBgTile()
    {
        // Havuzdan uygun bir bgTile al
        for (int i = 0; i < _bgTilePool.Count; i++)
        {
            if (!_bgTilePool[i].activeInHierarchy)
            {
                return _bgTilePool[i];
            }
        }
        return null;
    }
    private GameObject GetPooledDestroyEffect(Vector2Int position)
    {
        for (int i = 0; i < _matchFinder.currentMatches.Count; i++)
        {
            if (!_allCandies[position.x, position.y]._destroyEffectList[i].activeInHierarchy)
            {
                return _allCandies[position.x, position.y]._destroyEffectList[i];
            }
        }
        return null;
    }
    private void SpawnTile(GameObject tile, Vector2 spawnPosition)
    {
        tile.transform.position = spawnPosition;
        tile.gameObject.SetActive(true);
        tile.name = "BG Tile -" + spawnPosition.x + ", " + spawnPosition.y;
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
        if (_allCandies[position.x, position.y] != null)
        {
            if (_allCandies[position.x, position.y].isMatched)
            {
                GameObject effect = GetPooledDestroyEffect(position);
                if (effect != null)
                {
                    effect.transform.position = new Vector3(position.x, position.y, 0);
                    effect.SetActive(true);
                    _candyPool.Add(_allCandies[position.x, position.y]);
                    //Þeker nesnesini devre dýþý býrak
                    _allCandies[position.x, position.y].gameObject.SetActive(false);
                    // _allCandies dizisindeki referansý temizle
                    _allCandies[position.x, position.y] = null;
                }
                // Þeker nesnesini havuza geri ekle

            }
        }
    }
    public void DestroyMatches()
    {
        for (int i = 0; i < _matchFinder.currentMatches.Count; i++)
        {
            if (_matchFinder.currentMatches[i] != null)
            {
                ScoreCheck(_matchFinder.currentMatches[i]);
                DestroyMatchedGemAt(_matchFinder.currentMatches[i].posIndex);
                _roundManager.CheckGoal(_matchFinder.currentMatches.Count, _matchFinder.currentMatches[i].type);
            }
        }
        StartCoroutine(DecreaseRowCr());
    }

    private IEnumerator DecreaseRowCr()
    {
        yield return new WaitForSeconds(.2f);
        int nullCount = 0;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (_allCandies[x, y] == null)
                {
                    nullCount++;
                }
                else if (nullCount > 0)
                {
                    _allCandies[x, y].posIndex.y -= nullCount;
                    _allCandies[x, y - nullCount] = _allCandies[x, y];
                    _allCandies[x, y] = null;
                }
            }
            nullCount = 0;
        }
        StartCoroutine(FillBoardCr());
    }
    private IEnumerator FillBoardCr()
    {
        yield return new WaitForSeconds(.5f);
        RefillBoard();
        yield return new WaitForSeconds(.5f);
        _matchFinder.FindAllMatches();
        if (_matchFinder.currentMatches.Count > 0)
        {
            yield return new WaitForSeconds(.5f);
            DestroyMatches();
        }
        else
        {
            yield return new WaitForSeconds(.5f);
            currentState = BoardState.move;
        }
    }
    private void RefillBoard()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (_allCandies[x, y] == null)
                {

                    if (Random.Range(0, 100f) < bombChance)
                    {
                        int bombToUse = Random.Range(0, _bombPool.Count);//
                        SpawnCandy(new Vector2Int(x, y), _bombPool[bombToUse], true);
                    }
                    else
                    {
                        int candyToUse = Random.Range(0, _candyPool.Count);
                        SpawnCandy(new Vector2Int(x, y), _candyPool[candyToUse], false);
                    }
                }

            }
        }
        CheckMisplacedGems();
    }
    private void CheckMisplacedGems()
    {
        List<Candy> foundCandy = new List<Candy>();
        foundCandy.AddRange(FindObjectsOfType<Candy>());
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (foundCandy.Contains(_allCandies[x, y]))
                {
                    foundCandy.Remove(_allCandies[x, y]);
                }
            }
        }
        foreach (Candy c in foundCandy)
        {
            Destroy(c.gameObject);
        }
    }

    public void ShuffleBoard()
    {
        if (currentState != BoardState.wait)
        {
            currentState = BoardState.wait;
            List<Candy> candysFromBoard = new List<Candy>();
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    candysFromBoard.Add(_allCandies[x, y]);
                    _allCandies[x, y] = null;
                }
            }
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int candyToUse = Random.Range(0, candysFromBoard.Count);
                    int iterations = 0;
                    while (MatchesAt(new Vector2Int(x, y), candysFromBoard[candyToUse]) && iterations < 100 && candysFromBoard.Count > 1)
                    {
                        candyToUse = Random.Range(0, candysFromBoard.Count);
                        iterations++;
                    }
                    candysFromBoard[candyToUse].SetupCandy(new Vector2Int(x, y), this);
                    _allCandies[x, y] = candysFromBoard[candyToUse];
                    candysFromBoard.RemoveAt(candyToUse);
                }
            }
            StartCoroutine(FillBoardCr());
        }
    }
    public void ScoreCheck(Candy candyToCheck)
    {
        _roundManager._currentScore += candyToCheck.scoreValue;
    }
}

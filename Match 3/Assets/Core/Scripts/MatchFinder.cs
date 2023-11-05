using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MatchFinder : MonoBehaviour
{
    private Board _board;
    public List<Candy> currentMatches = new List<Candy>();

    private void Awake()
    {
        _board = FindObjectOfType<Board>();
    }

    public void FindAllMatches()
    {
        currentMatches.Clear();
        for (int x = 0; x < _board.width; x++)
        {
            for (int y = 0; y < _board.height; y++)
            {
                Candy currentCandy = _board._allCandies[x, y];
                if (currentCandy != null)
                {
                    if (x > 0 && x < _board.width - 1)
                    {
                        Candy leftCandy = _board._allCandies[x - 1, y];
                        Candy rightCandy = _board._allCandies[x + 1, y];
                        if (leftCandy != null && rightCandy != null)
                        {
                            if (leftCandy.type == currentCandy.type && rightCandy.type == currentCandy.type)
                            {
                                currentCandy.isMatched = true;
                                leftCandy.isMatched = true;
                                rightCandy.isMatched = true;

                                currentMatches.Add(currentCandy);
                                currentMatches.Add(leftCandy);
                                currentMatches.Add(rightCandy);
                                
                            }
                        }
                    }
                    if (y > 0 && y < _board.height - 1)
                    {
                        Candy aboveCandy = _board._allCandies[x, y + 1];
                        Candy belowCandy = _board._allCandies[x, y - 1];
                        if (aboveCandy != null && belowCandy != null)
                        {
                            if (aboveCandy.type == currentCandy.type && belowCandy.type == currentCandy.type)
                            {
                                currentCandy.isMatched = true;
                                aboveCandy.isMatched = true;
                                belowCandy.isMatched = true;

                                currentMatches.Add(currentCandy);
                                currentMatches.Add(aboveCandy);
                                currentMatches.Add(belowCandy);
                                
                            }
                        }
                    }
                }
            }
        }
        if (currentMatches.Count > 0)
        {
            currentMatches = currentMatches.Distinct().ToList();
        }
        CheckForBombs();
    }
    public void CheckForBombs()
    {
        for (int i = 0; i < currentMatches.Count; i++)
        {
            Candy candy = currentMatches[i];

            int x = candy.posIndex.x;
            int y = candy.posIndex.y;
            if (candy.posIndex.x >0)
            {
                if (_board._allCandies[x-1 , y] !=null)
                {
                    if (_board._allCandies[x-1,y].type == Candy.CandyType.bomb)
                    {
                        MarkBombArea(new Vector2Int(x - 1, y), _board._allCandies[x-1,y]);
                    }
                }
            }
            if (candy.posIndex.x < _board.width - 1)
            {
                if (_board._allCandies[x + 1, y] != null)
                {
                    if (_board._allCandies[x + 1, y].type == Candy.CandyType.bomb)
                    {
                        MarkBombArea(new Vector2Int(x + 1, y), _board._allCandies[x + 1, y]);
                    }
                }
            }
            if (candy.posIndex.y > 0)
            {
                if (_board._allCandies[x, y -1] != null)
                {
                    if (_board._allCandies[x , y - 1].type == Candy.CandyType.bomb)
                    {
                        MarkBombArea(new Vector2Int(x , y - 1), _board._allCandies[x, y - 1]);
                    }
                }
            }
            if (candy.posIndex.y < _board.height - 1)
            {
                if (_board._allCandies[x, y + 1] != null)
                {
                    if (_board._allCandies[x, y + 1].type == Candy.CandyType.bomb)
                    {
                        MarkBombArea(new Vector2Int(x, y + 1), _board._allCandies[x, y + 1]);
                    }
                }
            }
        }
    }
    public void MarkBombArea(Vector2Int bombPosition, Candy theBomb)
    {
        for (int x =bombPosition.x - theBomb.blastSize; x <= bombPosition.x + theBomb.blastSize; x++)
        {
            for (int y = bombPosition.y-theBomb.blastSize; y <= bombPosition.y + theBomb.blastSize; y++)
            {
                if (x >= 0 && x < _board.width && y>=0 && y<_board.height)
                {
                    if (_board._allCandies[x,y] != null)
                    {
                        _board._allCandies[x, y].isMatched = true;
                        currentMatches.Add(_board._allCandies[x, y]);
                    }
                }
            }
        }
        currentMatches = currentMatches.Distinct().ToList();
    }
}


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
    }
}


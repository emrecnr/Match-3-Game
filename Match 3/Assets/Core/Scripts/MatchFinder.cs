using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchFinder : MonoBehaviour
{
    private Board _board;

    private void Awake()
    {
        _board = FindObjectOfType<Board>();
    }

    public void FindAllMatches()
    {
        for (int x = 0; x < _board.width; x++)
        {
            for (int y = 0; y < _board.height; y++)
            {
                Candy currentCandy = _board._allCandies[x, y];
                if (currentCandy != null)
                {
                    if (x > 0 && x <_board.width -1)
                    {
                        Candy leftCandy = _board._allCandies[x - 1, y];
                        Candy rightCandy = _board._allCandies[x + 1, y];
                        if(leftCandy != null && rightCandy != null)
                        {
                            if (leftCandy.type == currentCandy.type && rightCandy.type == currentCandy.type)
                            {
                                currentCandy.isMatched = true;
                                leftCandy.isMatched = true;
                                rightCandy.isMatched = true;
                                Debug.Log("Matched");
                            }
                        }
                    }
                }
            }
        }
    }
}

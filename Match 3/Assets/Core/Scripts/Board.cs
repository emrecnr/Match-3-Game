using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] GameObject _bgTilePref;

    private void Start()
    {
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
            }
        }
    }
}

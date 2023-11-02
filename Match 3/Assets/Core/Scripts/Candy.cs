using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour
{
    [SerializeField] private Vector2Int posIndex;
    [SerializeField] private Board _board;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetupCandy(Vector2Int position,Board board)
    {
        posIndex = position;
        _board = board; 
    }
}

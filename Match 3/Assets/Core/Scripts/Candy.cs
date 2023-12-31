using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour
{
    public enum CandyType { twister, pink, cookie, triangle, chocolate, waffle, pinkRound, blue, stick, lollipop, bean ,bomb}
    public CandyType type;

    
    [HideInInspector]
    public Vector2Int posIndex;
    [HideInInspector]
    public Board _board;
    [HideInInspector]
    public Vector2Int previousPosition;
    
    private Candy _otherCandy;

    private Vector2 firstTouchPosition;
    private Vector2 finalTouchPosition;

    private bool _mousePressed;
    private float _swipeAngle;
    public List<GameObject> _destroyEffectList = new List<GameObject>();
    public int blastSize = 1;
    public int scoreValue = 10;


    public bool isMatched;
    

    // Update is called once per frame
    void Update()
    {

        if (Vector2.Distance(transform.position, posIndex) > .01f)
        {
            transform.position = Vector2.Lerp(transform.position, posIndex, _board.candySpeed * Time.deltaTime);
        }
        else
        {
            Debug.LogWarning("!!!!!");
            transform.position = new Vector3(posIndex.x, posIndex.y, 0f);
            _board._allCandies[posIndex.x, posIndex.y] = this;
        }

        if (_mousePressed && Input.GetMouseButtonUp(0) &&!RoundManager.isGameOver)
        {
            _mousePressed = false;
            if (_board.currentState == Board.BoardState.move)
            {
                finalTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                CalculateAngle();
            }
        }
    }
    public void SetupCandy(Vector2Int position, Board board)
    {
        posIndex = position;
        _board = board;
    }
    private void OnEnable()
    {
        isMatched = false;
        
    }

    private void OnMouseDown()
    {
        if (_board.currentState == Board.BoardState.move && !RoundManager.isGameOver)
        {
            firstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // ilk dokunmayi World Pos donustur
            _mousePressed = true;

        }
        if (_board.currentState == Board.BoardState.wait && !RoundManager.isGameOver /*&& BoosterManager.isSwitcherBoostUsed*/)
        {
            if (BoosterManager.selectedCandy1 == null)
            {
                BoosterManager.selectedCandy1 = this;
                GetComponent<Animator>().SetBool("isSelected", true);
                
                Debug.Log(BoosterManager.selectedCandy1);
                
            }
            else if(BoosterManager.selectedCandy2 == null)
            {
                BoosterManager.selectedCandy2 = this;
                GetComponent<Animator>().SetBool("isSelected", true);
                Debug.Log(BoosterManager.selectedCandy2);
            }
           
             
        } 
    }
   
    private void CalculateAngle()
    {
        _swipeAngle = Mathf.Atan2(finalTouchPosition.y - firstTouchPosition.y, finalTouchPosition.x - firstTouchPosition.x);
        _swipeAngle = _swipeAngle * 180 / Mathf.PI;
        

        if (Vector3.Distance(firstTouchPosition, finalTouchPosition) > .5f)
        {
            MovePieces();
            
        }
    }
    private void MovePieces()
    {
        previousPosition = posIndex; // nesnenin haraket etmeden onceki konumu

        if (_swipeAngle < 45 && _swipeAngle > -45 && posIndex.x < _board.width - 1)
        {
            _otherCandy = _board._allCandies[posIndex.x + 1, posIndex.y];
            _otherCandy.posIndex.x--;
            posIndex.x++;
        }
        else if (_swipeAngle > 45 && _swipeAngle <= 135 && posIndex.y < _board.height - 1)
        {
            _otherCandy = _board._allCandies[posIndex.x, posIndex.y + 1];
            _otherCandy.posIndex.y--;
            posIndex.y++;
        }
        else if (_swipeAngle < -45 && _swipeAngle >= -135 && posIndex.y > 0)
        {
            _otherCandy = _board._allCandies[posIndex.x, posIndex.y - 1];
            _otherCandy.posIndex.y++;
            posIndex.y--;

        }
        else if (_swipeAngle > 135 || _swipeAngle < -135 && posIndex.x > 0)
        {
            _otherCandy = _board._allCandies[posIndex.x - 1, posIndex.y];
            _otherCandy.posIndex.x++;
            posIndex.x--;
        }
        _board._allCandies[posIndex.x, posIndex.y] = this;
        _board._allCandies[_otherCandy.posIndex.x, _otherCandy.posIndex.y] = _otherCandy;
        StartCoroutine(CheckMoveCr());
    }
    public IEnumerator CheckMoveCr()
    {
        _board.currentState = Board.BoardState.wait;
        yield return new WaitForSeconds(.5f);
        _board._matchFinder.FindAllMatches();
        if (_otherCandy != null)
        {
            if (!isMatched && !_otherCandy.isMatched)
            {
                _otherCandy.posIndex = posIndex;
                posIndex = previousPosition;

                _board._allCandies[posIndex.x, posIndex.y] = this;
                _board._allCandies[_otherCandy.posIndex.x, _otherCandy.posIndex.y] = _otherCandy;
                yield return new WaitForSeconds(.5f);
                _board.currentState = Board.BoardState.move;
            }
            else
            {
                _board.DestroyMatches();
                // Kayd�rma hakk� azaltma 
                _board.DecreaseMove();
            }
        }
    }
    
}


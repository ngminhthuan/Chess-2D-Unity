using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessBoardCell : MonoBehaviour
{
    [SerializeField] private GameObject _cellCanMove;
    private Vector2 _cellPos;
    private BoardManager _boardManager;
    private ChessPiece _currentChessPiece;

    public void Setup(Vector2 position, BoardManager boardManager)
    {
        _cellPos = position;
        _boardManager = boardManager;

        FillColor(this.GetComponent<SpriteRenderer>(), (int) position.x, (int) position.y);
    }

    private void FillColor(SpriteRenderer cell, int x, int y)
    {
        bool oddY = (y % 2 != 0) ? true : false;
        bool oddX = (x % 2 != 0) ? true : false;
        if((oddY && oddX)||(!oddY && !oddX)) 
        {
            cell.color = new Color32(120, 79, 72, 255);
            return;
        }
    }

    internal void SetChessPiece(ChessPiece piece)
    {
        _currentChessPiece = piece;
    }

    private void OnMouseDown()
    {
        if(_currentChessPiece != null)
        {
            if(CanSelected())
            {
                _boardManager.ResetAllBoardCell();
                _boardManager.GameManager.OnClickOnChessPiece?.Invoke(_cellPos, _currentChessPiece, _boardManager.SuggestWayForChessPiece);
            }

        }

        ClickToMoveChessPieceTo();
    }

    private bool CanSelected()
    {
        var selectedChessPiece = _boardManager.GameManager.SelectedChessPiece;
        if(selectedChessPiece != null)
        {
            if(_currentChessPiece.Skin != selectedChessPiece.Skin)
            {
                return false;
            }
        }
        return true;
    }

    private void ClickToMoveChessPieceTo()
    {
        if(_cellCanMove.activeSelf)
        {
            _cellCanMove.SetActive(false);

            var chessPiece = _boardManager.GameManager.SelectedChessPiece;
            var selectedChessPieceTransform = chessPiece.transform;

            Occupied(chessPiece);
            _boardManager.RemoveChessPieceFromCell(chessPiece);
            _currentChessPiece = chessPiece;
            chessPiece.Move(new Vector2Int((int)_cellPos.x, (int)_cellPos.y));
            selectedChessPieceTransform.parent = this.gameObject.transform;
            
            _boardManager.GameManager.OnMoved?.Invoke();
            _boardManager.ResetAllBoardCell();
        }
    }

    private void Occupied(ChessPiece newChessPiece)
    {
        if(_currentChessPiece != null)
        {
            if(newChessPiece.Skin != _currentChessPiece.Skin)
            {
                Debug.Log("kill him now");
                _boardManager.GameManager.KillOpponentKing(_currentChessPiece);
                Destroy(_currentChessPiece.gameObject);
            }
        }
    }

    internal bool IsEmpty() => _currentChessPiece == null;

    internal bool HasEnemy(ChessSkin chessSkin)
    {
        if(IsEmpty()) return false;

        ChessPiece piece = _currentChessPiece;
        return piece.Skin != chessSkin;
    }

    internal void ShowCellCanMove()
    {
        _cellCanMove.SetActive(true);
    }

    internal void HideCellCanMove()
    {
         _cellCanMove.SetActive(false);
    }

    internal void RemoveChessPiece()
    {
        _currentChessPiece = null;
    }
}

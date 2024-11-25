using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
     //Creating a reference field for the cell prefab in the Unity Editor
    [SerializeField] private GameObject _cellPrefab;
    private ChessBoardCell[,] _allCells = new ChessBoardCell[8, 8];
    private List<ChessBoardCell> _onSuggestCells;

    internal GameManager GameManager;
    public ChessBoardCell[,] AllCells { get => _allCells;}

    public void InitBoard(GameManager gameManager)
    {
        this.GameManager = gameManager;
        _onSuggestCells = new List<ChessBoardCell>(30);

        CreateBoard();
    }

    private void CreateBoard()
    {
        //Creating the all the cells
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                
                //Instantiating a new cell from the Cell Prefab
                GameObject newCell = Instantiate(_cellPrefab, transform);

                //Setting the position of the Cell
                //Transform cellTrans = newCell.GetComponent<Transform>();
                newCell.transform.position = new Vector2(x, y);

                ////Setting up the Cells
                _allCells[x, y] = newCell.GetComponent<ChessBoardCell>();
                _allCells[x, y].Setup(new Vector2Int(x, y), this);
            }
        }

    }

    public ChessBoardCell GetCell(int x, int y)
    {
        return this._allCells[x, y];
    }

    //public static ChessBoardCell IsEmpty(this BoardManager boardManager)
    //{
    //    return cell._currentChessPiece == null;
    //}

    internal void SuggestWayForChessPiece(ChessPiece chessPiece)
    {
        List<(int, int)> suggestWays = chessPiece.GetWays();

        foreach(var item in suggestWays)
        {
            var cell = _allCells[item.Item1, item.Item2];
            cell.ShowCellCanMove();
            _onSuggestCells.Add(cell);
        } 
    }

    internal void ResetAllBoardCell()
    {
        if(_onSuggestCells.Count == 0) return;

        foreach(var cell in _onSuggestCells)
        {
            cell.HideCellCanMove();            
        }
        _onSuggestCells.Clear();
    }

    internal void RemoveChessPieceFromCell(ChessPiece chessPiece)
    {
        var cell = GetCell(chessPiece.Position.x, chessPiece.Position.y);
        cell.RemoveChessPiece();
    }
}

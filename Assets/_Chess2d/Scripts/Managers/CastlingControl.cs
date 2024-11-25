using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastlingControl : MonoBehaviour
{
    [SerializeField] private BoardManager _boardManager;
    private GameManager _gameManager;

    private King _whiteKing;
    private King _blackKing;

    private List<Rook> _whiteRooks;
    private List<Rook> _blackRooks;

    Vector2Int[] castlingDirections = {
            new Vector2Int(-2, 0),  // Left
            new Vector2Int(2, 0),  // Right
        };

    public void InitControl(GameManager gameManager)
    {
        _gameManager = gameManager;

        _whiteRooks = new List<Rook>(2);
        _blackRooks = new List<Rook>(2);
    }

    public void SetKing(King king)
    {
        king.SetCastling(this);
        if(king.Skin == ChessSkin.White)
            _whiteKing = king;
        else
            _blackKing = king;
    }

    public void SetRook(Rook rook)
    {
        rook.SetCastling(this);
        if(rook.Skin == ChessSkin.White)
            _whiteRooks.Add(rook);
        else
            _blackRooks.Add(rook);
    }

    public List<(int, int)> GetCastlingSuggest(King king)
    {
        List<(int, int)> validMoves = new List<(int, int)>();
        var kingPos = king.Position;

        List<Rook> rooks = king.Skin == ChessSkin.White ? _whiteRooks : _blackRooks;
        int directionLeft = -1;
        int directionRight = 1; 

        CheckCastlingDirection(kingPos, rooks[0], directionLeft, validMoves);
        CheckCastlingDirection(kingPos, rooks[1], directionRight, validMoves);

        return validMoves;
    }

    private void CheckCastlingDirection(Vector2Int kingPos, Rook rook, int direction, List<(int, int)> validMoves)
    {
        if (rook.HasMove) return; 

        bool legitToCastling = true;
        int targetColumn = (direction == -1) ? 0 : 7; 

        for (int i = kingPos.x + direction; i != targetColumn; i += direction)
        {
            var cell = _boardManager.GetCell(i, kingPos.y);
            if (!cell.IsEmpty())
            {
                legitToCastling = false;
                break;
            }
        }

        if (legitToCastling)
        {
            Vector2Int targetPos = kingPos + new Vector2Int(2 * direction, 0);
            validMoves.Add((targetPos.x, targetPos.y));
        }
    }

    public void MoveCastling(King king, int moveValue)
    {
        List<Rook> rooks = (king.Skin == ChessSkin.White) ? _whiteRooks : _blackRooks;

        int rookIndex = (moveValue > 0) ? 1 : 0;  
        int rookOffset = (moveValue > 0) ? 1 : -1; 

        Vector2Int rookTargetPos = new Vector2Int(king.Position.x + rookOffset, king.Position.y);

        _boardManager.RemoveChessPieceFromCell(rooks[rookIndex]);
        rooks[rookIndex].Move(rookTargetPos);
       
        var cell = _boardManager.GetCell(rookTargetPos.x, rookTargetPos.y);
        cell.SetChessPiece(rooks[rookIndex]);
    }

}

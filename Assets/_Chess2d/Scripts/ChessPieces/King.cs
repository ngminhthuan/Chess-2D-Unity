using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : ChessPiece
{
    private bool _hasMove;
    private CastlingControl _castling;

    Vector2Int[] directions = {
            new Vector2Int(1, 0),   // Right
            new Vector2Int(-1, 0),  // Left
            new Vector2Int(0, 1),   // Up
            new Vector2Int(0, -1),  // Down
            new Vector2Int(1, 1),   // Top-right
            new Vector2Int(-1, 1),  // Top-left
            new Vector2Int(1, -1),  // Bottom-right
            new Vector2Int(-1, -1)  // Bottom-left
        };

    public void SetCastling(CastlingControl castling)
    {
        _hasMove = false;
        _castling = castling;
    }

    public override List<(int, int)> GetWays()
    {
        List<(int, int)> validMoves = new List<(int, int)>();

        foreach (var direction in directions)
        {
            Vector2Int targetPos = CurrentPosition + direction;

            if (IsInsideBoard(targetPos.x, targetPos.y))
            {
                var cell = BoardManager.GetCell(targetPos.x, targetPos.y);

                if (cell.HasEnemy(Skin) || cell.IsEmpty())
                {
                    validMoves.Add((targetPos.x, targetPos.y));
                }
            }
        }

        if (!_hasMove)
            validMoves.AddRange(_castling.GetCastlingSuggest(this));

        return validMoves;
    }

    public override void Move(Vector2Int newPos)
    {
        if(!_hasMove)
        {
            _hasMove = true;
            var moveValue = newPos.x - this.transform.position.x;
            if (Mathf.Abs(moveValue) > 1)
                _castling.MoveCastling(this,(int) moveValue);
        }

        this.transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
        SetPosition(newPos);    
    }
}

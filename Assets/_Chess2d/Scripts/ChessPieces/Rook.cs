using System.Collections.Generic;
using UnityEngine;

public class Rook : ChessPiece
{
    private bool _hasMove;
    private CastlingControl _castling;

    public bool HasMove => _hasMove;

    Vector2Int[] directions = {
            new Vector2Int(1, 0),  
            new Vector2Int(-1, 0), 
            new Vector2Int(0, 1),  
            new Vector2Int(0, -1)  
        };

    public void SetCastling(CastlingControl castling)
    {
        _hasMove = false;
        _castling = castling;
    }

    public override List<(int, int)> GetWays()
    {
        List <(int, int)> validMoves = new List<(int,int)>();

        foreach (var direction in directions)
        {
            Vector2Int currentPos = CurrentPosition;

            while (true)
            {
                currentPos += direction;

                if (!IsInsideBoard(currentPos.x, currentPos.y))
                    break;

                var cell = BoardManager.GetCell(currentPos.x, currentPos.y);

                if (cell.HasEnemy(Skin))
                {
                    validMoves.Add((currentPos.x, currentPos.y));
                    break;
                }
                else if (cell.IsEmpty())
                {
                    validMoves.Add((currentPos.x, currentPos.y));
                }
                else
                    break;
            }
        }

        return validMoves;
    }


    public override void Move(Vector2Int newPos)
    {
        if(!_hasMove)
            _hasMove = true;

        this.transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
        SetPosition(newPos);
    }
}

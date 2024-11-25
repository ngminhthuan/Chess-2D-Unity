using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Queen : ChessPiece
{
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

    public override List<(int, int)> GetWays()
    {
        List <(int, int)> validMoves = new List<(int,int)>();

        foreach (var direction in directions)
        {
            Vector2Int currentPos = CurrentPosition;
            while (true)
            {
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
        }

        return validMoves;
    }

    public override void Move(Vector2Int newPos)
    {
        this.transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
        SetPosition(newPos);
    }
}

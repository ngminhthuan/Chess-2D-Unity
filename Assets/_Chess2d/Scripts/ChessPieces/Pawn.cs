using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;

public class Pawn : ChessPiece
{
    private bool _isFirstMove = true;

    public Action<Vector2Int, Pawn> OnReachExchangeToQueenDestination;

    public override List<(int, int)> GetWays()
    {
        List<(int, int)> moves = new List<(int, int)>();

        int direction = Skin == ChessSkin.White ? 1 : -1; // White moves up, Black moves down
        Debug.Log("direction " + direction);
        // One step forward

        int forwardY = CurrentPosition.y + direction;
        Debug.Log("forward " + forwardY);
        if (IsInsideBoard(CurrentPosition.x, forwardY) && BoardManager.AllCells[CurrentPosition.x, forwardY].IsEmpty())
        {
           moves.Add((CurrentPosition.x, forwardY));

           if(_isFirstMove)
           {
                int twoStepsY = forwardY + 1 * direction;

                if(BoardManager.AllCells[CurrentPosition.x, twoStepsY].IsEmpty())
                    moves.Add((CurrentPosition.x, twoStepsY));
           }
        }

        // Diagonal captures
        foreach (int dx in new[] { -1, 1 })
        {
           int diagonalX = CurrentPosition.x + dx;
           int diagonalY = CurrentPosition.y + direction;
           if (IsInsideBoard(diagonalX, diagonalY) && BoardManager.AllCells[diagonalX, diagonalY].HasEnemy(Skin))
           {
               moves.Add((diagonalX, diagonalY));
           }
        }

        return moves;
    }

    public override void Move(Vector2Int newPos)
    {
        _isFirstMove = false;

        this.transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
        SetPosition(newPos);

        if(newPos.y == 0 || newPos.y == 7)
        {
            OnReachExchangeToQueenDestination?.Invoke(newPos, this);
        }
    }
}

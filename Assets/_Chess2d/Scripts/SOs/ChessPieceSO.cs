using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "ChessPieces", menuName = "ScriptableObjects/ChessPieces")]
public class ChessPieceSO : ScriptableObject
{
    [SerializeField] private List<ChessPieceModel> _chessPieces;

    internal ChessPiece GetChessByType(ChessName chessName)
    {
        foreach (var piece in _chessPieces)
        {
            if(piece.ChessPieceName == chessName)
            {
                return piece.ChessPiece;
            }    
        }
        return null;
    }
}

[Serializable]
public class ChessPieceModel
{
    public ChessName ChessPieceName;
    public ChessPiece ChessPiece;
}

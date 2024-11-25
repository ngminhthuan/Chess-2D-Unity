using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessSpawnManager : MonoBehaviour
{
    [SerializeField] private ChessPieceSO _chessPiece;
    private CastlingControl _castlingControl;

    private BoardManager _boardManager;

    public void InitChessSpawner(BoardManager board, CastlingControl castlingControl)
    {
        _boardManager = board;
        _castlingControl = castlingControl;
        InitChessPieces(_boardManager.AllCells);
    }

    private void InitChessPieces(ChessBoardCell[,] allCells)
    {
        // Place Pawns
        var pawn = _chessPiece.GetChessByType(ChessName.Pawn);
        InitPawns(allCells, pawn);

        //// Place Rooks
        var rook = _chessPiece.GetChessByType(ChessName.Rook);
        var whiteRookCellsPos = new (int, int)[] { (0, 0), (7, 0) };
        var blackRookCellsPos = new (int, int)[] { (0, 7), (7, 7) };
        InitGeneralChessPiece(whiteRookCellsPos, rook, ChessSkin.White);
        InitGeneralChessPiece(blackRookCellsPos, rook, ChessSkin.Black);

        //// Place Knights
        var knight = _chessPiece.GetChessByType(ChessName.Knight);
        var whiteKnightCellsPos = new (int, int)[] { (1, 0), (6, 0) };
        var blackKnightCellsPos = new (int, int)[] { (1, 7), (6, 7) };
        InitGeneralChessPiece(whiteKnightCellsPos, knight, ChessSkin.White);
        InitGeneralChessPiece(blackKnightCellsPos, knight, ChessSkin.Black);

        //// Place Bishops
        var bishop = _chessPiece.GetChessByType(ChessName.Bishop);
        var whiteBishopCellsPos = new (int, int)[] { (2, 0), (5, 0) };
        var blackBishopCellsPos = new (int, int)[] { (2, 7), (5, 7) };
        InitGeneralChessPiece(whiteBishopCellsPos, bishop, ChessSkin.White);
        InitGeneralChessPiece(blackBishopCellsPos, bishop, ChessSkin.Black);

        //// Place Queens
        var queen = _chessPiece.GetChessByType(ChessName.Queen);
        InitGeneralChessPiece(new (int, int)[] {(3, 0)}, queen, ChessSkin.White);
        InitGeneralChessPiece(new (int, int)[] {(3, 7)}, queen, ChessSkin.Black);

        //// Place Kings
        var king = _chessPiece.GetChessByType(ChessName.King);
        InitGeneralChessPiece(new (int, int)[] { (4, 0) }, king, ChessSkin.White);
        InitGeneralChessPiece(new (int, int)[] { (4, 7) }, king, ChessSkin.Black);
    }

    private void InitPawns(ChessBoardCell[,] allCells, ChessPiece pawn)
    {
        for (int i = 0; i < 8; i++)
        {
            var whitePawn = Instantiate(pawn, allCells[i, 1].transform);
            var whitePawnInstant = whitePawn.GetComponent<Pawn>();
            whitePawnInstant.OnReachExchangeToQueenDestination += OnPawnExchangeToQueen;
            whitePawnInstant.InitChessPiece(ChessSkin.White, _boardManager);
            allCells[i, 1].SetChessPiece(whitePawnInstant); // White pawns

            var blackPawn = Instantiate(pawn, allCells[i, 6].transform);
            var blackPawnInstant = blackPawn.GetComponent<Pawn>();
            blackPawnInstant.OnReachExchangeToQueenDestination += OnPawnExchangeToQueen;
            blackPawnInstant.InitChessPiece(ChessSkin.Black, _boardManager);
            allCells[i, 6].SetChessPiece(blackPawnInstant); // Black pawns
        }
    }

    private void InitGeneralChessPiece((int, int)[] positions, ChessPiece generalChessPiece, ChessSkin skin)
    {
        foreach(var pos in positions)
        {
            var cell = _boardManager.AllCells[pos.Item1, pos.Item2];
            var chessPiece = Instantiate(generalChessPiece, cell.transform);
            var chessPieceInstant = chessPiece.GetComponent<ChessPiece>();

            chessPieceInstant.InitChessPiece(skin, _boardManager);
            cell.SetChessPiece(chessPiece);

            if (chessPieceInstant is King)
                _castlingControl.SetKing((King)chessPieceInstant);
            if (chessPieceInstant is Rook)
                _castlingControl.SetRook((Rook)chessPieceInstant);
        }
    }

    public void OnPawnExchangeToQueen(Vector2Int exchangePos, Pawn pawn)
    {
        pawn.OnReachExchangeToQueenDestination -= OnPawnExchangeToQueen;

        var queen = _chessPiece.GetChessByType(ChessName.Queen);
        InitGeneralChessPiece(new (int, int)[] { (exchangePos.x, exchangePos.y) }, queen, pawn.Skin);

        Destroy(pawn.gameObject);
    }
}

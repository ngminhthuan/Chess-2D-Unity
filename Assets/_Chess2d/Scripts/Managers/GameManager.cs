using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private MainGameUI _mainGameUI;

    [SerializeField] private GameObject _cellSelectedObj;

    [SerializeField] private BoardManager _board;
    [SerializeField] private ChessSpawnManager _chessSpawnManager;
    [SerializeField] private CastlingControl _castingControl;

    private ChessSkin _currentGameTurn;
    [SerializeField] private ChessPiece _selectedChessPiece;

    public ChessPiece SelectedChessPiece => _selectedChessPiece;
    public Action<Vector2, ChessPiece, Action<ChessPiece>> OnClickOnChessPiece;
    public Action OnMoved;


    void Awake()
    {
        InitGame();
    }

    private void InitGame()
    {
        _board.InitBoard(this);
        _castingControl.InitControl(this);
        _chessSpawnManager.InitChessSpawner(_board, _castingControl);

        OnClickOnChessPiece += OnPlayerClickOnChessPiece;
        OnMoved += OnOnePlayerMoved;

        StartGame();
    }

    private void StartGame()
    {
        _currentGameTurn = ChessSkin.White;
        _mainGameUI.ChangeTurnTitle(_currentGameTurn);
    }

    private void EndGame(ChessSkin chessSkin)
    {
        _mainGameUI.ShowEndGameUI(chessSkin);

        OnClickOnChessPiece -= OnPlayerClickOnChessPiece;
        OnMoved -= OnOnePlayerMoved;
    }

    private void OnPlayerClickOnChessPiece(Vector2 currentChessPiecePos, ChessPiece chessPiece, Action<ChessPiece> suggestWaysForMove)
    {
        if(!LegitToSelect(chessPiece))
        {
            _cellSelectedObj.SetActive(false);
            return;
        }

        _cellSelectedObj.SetActive(true);
        _cellSelectedObj.transform.position = currentChessPiecePos;

        _selectedChessPiece = chessPiece;

        suggestWaysForMove?.Invoke(chessPiece);
    }

    private bool LegitToSelect(ChessPiece chessPiece)
    {
        if (chessPiece.Skin != _currentGameTurn) return false;
        if (chessPiece.Skin != _currentGameTurn) return false;
        return true;
    }

    private void OnOnePlayerMoved()
    {
        _currentGameTurn = _currentGameTurn == ChessSkin.White ? ChessSkin.Black : ChessSkin.White; 
        _mainGameUI.ChangeTurnTitle(_currentGameTurn);

        _selectedChessPiece = null;
        _cellSelectedObj.SetActive(false);
    }

    public void KillOpponentKing(ChessPiece chessPiece)
    {
        var king = chessPiece.GetComponent<King>();

        if(king != null)
        {
            EndGame(king.Skin);
        }
    }    
}

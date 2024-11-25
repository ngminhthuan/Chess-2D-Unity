using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChessPiece : MonoBehaviour
{
    [SerializeField] protected Sprite WhiteSkin;
    [SerializeField] protected Sprite BlackSkin;

    [SerializeField] protected Vector2Int CurrentPosition;
    protected BoardManager BoardManager;
    private ChessSkin _skin;
    
    public ChessSkin Skin => _skin;
    public Vector2Int Position => CurrentPosition;

    private void Start()
    {
        SetPosition(new Vector2Int((int)transform.position.x, (int) transform.position.y));
    }

    internal void InitChessPiece(ChessSkin chessSkin, BoardManager boardManager)
    {
        SetSkin(chessSkin);
        SetBoardData(boardManager);
    }

    private void SetSkin(ChessSkin chessSkin)
    {
        _skin = chessSkin;
        var renderer = GetComponent<SpriteRenderer>();
        switch (chessSkin)
        {
            case ChessSkin.White:
                renderer.sprite = WhiteSkin;
                break;
            case ChessSkin.Black:
                renderer.sprite = BlackSkin;
                break;
            default:
                renderer.sprite = WhiteSkin;
                break;
        }
    }

    private void SetBoardData(BoardManager boardManager)
    {
        BoardManager = boardManager;
    }


    protected void SetPosition(Vector2Int newPosition)
    {
        CurrentPosition = newPosition;
    }

    protected bool IsInsideBoard(int x, int y)
    {
        return x >= 0 && x < 8 && y >= 0 && y < 8;
    }

    public abstract List<(int, int)> GetWays();
    public abstract void Move(Vector2Int newPos);
}

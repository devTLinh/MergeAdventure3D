using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance;

    public int width = 5;
    public int height = 5;

    public BoardSlot[] slots; // drag từ inspector

    private BoardSlot[,] grid;

    void Awake()
    {
        Instance = this;

        grid = new BoardSlot[width, height];

        foreach (var slot in slots)
        {
            grid[slot.pos.x, slot.pos.y] = slot;
        }
    }

    public BoardSlot GetSlot(Vector2Int pos)
    {
        return grid[pos.x, pos.y];
    }
}
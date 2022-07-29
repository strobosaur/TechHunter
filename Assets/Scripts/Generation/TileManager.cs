using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    [SerializeField] private Tilemap floorTilemap, wallTilemap, blockTilemap;
    [SerializeField] private List<TileBase> floorTiles;
    [SerializeField] private List<TileBase> wallTiles;
    [SerializeField] private float decoChance = 0.125f;

    public void ClearTiles()
    {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
        blockTilemap.ClearAllTiles();
    }
    
    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        PaintTiles(floorPositions, floorTilemap, floorTiles, decoChance);
    }

    public void PaintSingleWall(Vector2Int position)
    {
        PaintSingleTile(wallTilemap, wallTiles[Random.Range(0,wallTiles.Count)], position);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, List<TileBase> tiles, float decoChance = 0.125f)
    {
        foreach (var position in positions)
        {
            TileBase tile;
            if (Globals.Chance(decoChance))
                tile = tiles[Random.Range(1,tiles.Count)];
            else
                tile = tiles[0];
            PaintSingleTile(tilemap, tile, position);
        }
    }

    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
    {
        var tilePos = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePos, tile);
    }

    public void PaintVoid(IEnumerable<Vector2Int> voidPositions)
    {
        foreach (var position in voidPositions)
        {
            PaintSingleTile(blockTilemap, wallTiles[0], position);
        }
    }

    public HashSet<Vector2Int> CreateVoid(int inSize, bool offset = true)
    {
        int offs = 0;
        int size = Mathf.RoundToInt(inSize * 1.5f);
        HashSet<Vector2Int> outSet = new HashSet<Vector2Int>();
        if (offset) offs = size / 2;

        for (int j = 0; j < size; j++)
        {
            for (int i = 0; i < size; i++)
            {
                outSet.Add(new Vector2Int(i-offs, j-offs));
                //PaintSingleTile(blockTilemap, wallTiles[0], new Vector2Int(i-offs, j-offs));
            }
        }

        return outSet;
    }
}

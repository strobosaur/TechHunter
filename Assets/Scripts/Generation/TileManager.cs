using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    [SerializeField] private Tilemap floorTilemap, wallTilemap;
    [SerializeField] private List<TileBase> floorTiles;
    [SerializeField] private List<TileBase> wallTiles;
    [SerializeField] private float decoChance = 0.125f;

    public void ClearTiles()
    {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
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
}

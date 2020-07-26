using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum TileType{
    Empty = -1,
    Grass = 15,
    Tree = 16,
    Hills = 17,
    Mountains = 18,
    Towns = 19,
    Castle = 20,
    Monster = 21
}
public class Map
{
    public Tile[] tiles;
    public int columns;
    public int rows;
    
    // get all tiles on the edge
    public Tile[] coastTiles{
        get {
            return tiles.Where(t => t.autotileID < (int)TileType.Grass).ToArray();
        }
    }

    // get all inner tiles
    public Tile[] landTiles {
        get {
            return tiles.Where(t => t.autotileID == (int)TileType.Grass).ToArray();
        }
    }

    public Tile castleTile {
        get{
            return tiles.FirstOrDefault(t => t.autotileID == (int)TileType.Castle);
        }
    }

    // Create a new map given w and h
    public void NewMap(int width, int height)
    {
        columns = width;
        rows = height;

        // use 1D array (+ math) for speed
        tiles = new Tile[columns * rows];
        CreateTiles();
    }

    public void CreateIsland(
        float erodePercent,
        int erodeIterations,
        float treePercent,
        float hillPercent,
        float mountainPercent,
        float townPercent,
        float monsterPercent,
        float lakePercent
    ){
        DecorateTile (landTiles, lakePercent, TileType.Empty);

        for(var i = 0; i < erodeIterations; i++){
            DecorateTile (coastTiles, erodePercent, TileType.Empty);
        }

        var openTiles = landTiles;
        RandomizeTileArray(openTiles);
        openTiles[0].autotileID = (int)TileType.Castle;

        // in order, remaining percentage is reduced
        DecorateTile(landTiles,treePercent, TileType.Tree);
        DecorateTile(landTiles,hillPercent, TileType.Hills);
        DecorateTile(landTiles,mountainPercent, TileType.Mountains);
        DecorateTile(landTiles,townPercent, TileType.Towns);
        DecorateTile(landTiles,monsterPercent, TileType.Monster);

    }

    // Create all the tiles for the map into 'tiles' array
    private void CreateTiles(){
        var total = tiles.Length;
        for(var i = 0; i < total; i++){
            var tile = new Tile();
            tile.id = i;
            tiles[i] = tile;
        }

        FindNeighbors();
    }

    // Populate Neigbors of tiles
    private void FindNeighbors() {
        for (var r = 0; r < rows; r++) {
            for (var c = 0; c < columns; c++) {
                var tile = tiles[columns * r + c];
                // bottom neighbor
                if (r < rows - 1) {
                    tile.AddNeighbor(Sides.Bottom, tiles[columns * (r + 1) + c]);
                }
                // right neighbor
                if (c < columns - 1) {
                    tile.AddNeighbor(Sides.Right, tiles[columns * r + (c + 1)]);
                }
                // left neighbor
                if (c > 0) {
                    tile.AddNeighbor(Sides.Left, tiles[columns * r + (c - 1)]);
                }
                // bottom neighbor
                if (r > 0) {
                    tile.AddNeighbor(Sides.Top, tiles[columns * (r - 1) + c]);
                }
            }
        }
    }
    public void DecorateTile (Tile[] tiles, float percent, TileType type){

        // 
        var total = Mathf.FloorToInt(tiles.Length * percent);
        RandomizeTileArray(tiles);
        for (var i = 0; i < total; i++) {
            var tile = tiles[i];
            if (type == TileType.Empty) tile.ClearNeighbors();
            tile.autotileID = (int)type;
        }
    }

    // shuffle an array
    public void RandomizeTileArray(Tile[] tiles) {
        for (var i = 0; i < tiles.Length; i++ ) {
            var tmp = tiles [i];
            var r = Random.Range(i, tiles.Length);
            tiles[i] = tiles[r];
            tiles[r] = tmp;
        }
    }
}

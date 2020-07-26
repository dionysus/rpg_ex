using System.Collections;
using System.Collections.Generic;

public enum Sides{
    Bottom,
    Right,
    Left,
    Top
}

public class Tile
{
    public int id = 0;
    public Tile[] neighbors = new Tile[4]; // adj tiles: bot, right, left, top
    public int autotileID;
    public void AddNeighbor(Sides side, Tile tile){
        neighbors[(int)side] = tile;
        CalculateAutoTileID();
    }

    public void RemoveNeighbor(Tile tile) {
        var total = neighbors.Length;
        for (var i = 0; i < total; i++) {
            if (neighbors[i] != null) {
                if (neighbors[i].id == tile.id) {
                    neighbors[i] = null;
                }
            }
        }

        CalculateAutoTileID();
    }

    public void ClearNeighbors(){
        var total = neighbors.Length;
        for (var i = 0; i < total; i++) {
            var tile = neighbors[i];
            if (tile != null) {
                tile.RemoveNeighbor (this);
                neighbors[i] = null;
            }
            CalculateAutoTileID();
        }
    }

    private void CalculateAutoTileID() {
        int sideValues = 0;

        // b,r,l,t - assign 1 if neighbor exists
        // 1111 = 15 = all neighbors
        // 1010 = 10 = b,l neighbors
        foreach(Tile tile in neighbors) {
            sideValues = sideValues << 1;
            if (tile != null) sideValues = sideValues | 1; 
        }

        autotileID = sideValues;
    }

}

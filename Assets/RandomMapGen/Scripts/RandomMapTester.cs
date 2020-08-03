using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMapTester : MonoBehaviour
{   
    [Header("Map Dimensions")]
    public int mapWidth = 20;
    public int mapHeight = 20;
    
    [Space]
    [Header("Visualize Map")]
    public GameObject mapContainer;
    public GameObject tilePrefab;
    public Vector2 tileSize = new Vector2(16, 16);

    [Space]
    [Header("Map Sprites")]
    public Texture2D islandTexture;

    [Space]
    [Header("Player")]
    public GameObject playerPrefab;
    public GameObject player;

    [Space]
    [Header("Decorate Map")]
    [Range(0, 0.9f)] // slider
    public float erodePercent = .5f;
    public int erodeIterations = 2;
    [Range(0, 0.9f)] // slider
    public float treePercent = .3f;
    [Range(0, 0.9f)] // slider
    public float hillPercent = .2f;
    [Range(0, 0.9f)] // slider
    public float mountainPercent = .1f;
    [Range(0, 0.9f)] // slider
    public float townPercent = .05f;
    [Range(0, 0.9f)] // slider
    public float monsterPercent = .1f;
    [Range(0, 0.9f)] // slider
    public float lakePercent = .05f;

    public Map map;
    private int tmpX;
    private int tmpY;

    // Start is called before the first frame update
    void Start()
    {
        map = new Map();
        MakeMap();
        StartCoroutine (AddPlayer());
    }

    IEnumerator AddPlayer(){
        yield return new WaitForEndOfFrame();
        CreatePlayer();
    }

    public void MakeMap(){
        map.NewMap(mapWidth, mapHeight);
        map.CreateIsland(
            erodePercent,
            erodeIterations,
            treePercent,
            hillPercent,
            mountainPercent,
            townPercent,
            monsterPercent,
            lakePercent
            );
        // Debug.Log(string.Format("Created a new {0} x {0} map!", mapWidth, mapHeight));
        CreateGrid();
        CenterMap(map.castleTile.id);
    }

    void CreateGrid(){
        ClearMapContainer();

        Sprite[] sprites = Resources.LoadAll<Sprite>(islandTexture.name);

        var total = map.tiles.Length;
        var maxColumns = map.columns;
        var column = 0;
        var row = 0;

        // loop over each tile in the tile array
        for (var i = 0; i < total; i ++) {
            // convert 1D array into col x row
            column = i % maxColumns;
            var newX = column * tileSize.x;
            var newY = -row * tileSize.y;

            // make the tilePrefab
            var go = Instantiate(tilePrefab);
            go.name = "Tile " + i;
            go.transform.SetParent(mapContainer.transform);
            go.transform.position = new Vector3 (newX, newY, 0);

            // assign Sprite and render it
            var tile = map.tiles[i];
            var spriteId = tile.autotileID;
            if(spriteId >= 0) {
                var sr = go.GetComponent<SpriteRenderer>();
                sr.sprite = sprites[spriteId];
            }

            // check if at last column, start new row
            if (column == maxColumns - 1) row++;
        }
    }

    //
    public void CreatePlayer(){
        player = Instantiate(playerPrefab);
        player.name = "Player";
        player.transform.SetParent(mapContainer.transform);

        // PosUtil.CalculatePos(map.castleTile.id, map.columns, out tmpX, out tmpY);

        // tmpX *= (int)tileSize.x;
        // tmpY *= -(int)tileSize.y;

        // player.transform.position = new Vector3(tmpX, tmpY, 0);
        var controller = player.GetComponent<MapMovementController>();
        controller.map = map;
        controller.tileSize = tileSize;
        controller.MoveTo(map.castleTile.id);
    }

    // Destroy all existing tiles
    void ClearMapContainer (){
        var children = mapContainer.transform.GetComponentsInChildren<Transform> ();
        for (var i = children.Length - 1; i > 0; i--) {
            Destroy(children[i].gameObject);
        }
    }

    void CenterMap(int index){

        var camPos = Camera.main.transform.position;
        var width = map.columns;

        PosUtil.CalculatePos(index, width, out tmpX, out tmpY);

        camPos.x = tmpX * tileSize.x;
        camPos.y = (-tmpY) * tileSize.y;
        Camera.main.transform.position = camPos;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{

    [SerializeField]
    private Tilemap groundMap;

    [SerializeField]
    private Tilemap topMap;

    [SerializeField]
    private List<TileData> tileDatas;

    private Dictionary<TileBase, TileData> dataFromTiles; 
    
    [SerializeField]
    private TileBase lavaTile;

    [SerializeField]
    private TileBase animatedTopTile;
    
    [SerializeField]
    private Map_Presets mapPresets;



    private void Awake()
    {
        dataFromTiles = new Dictionary<TileBase, TileData>();
        foreach (var tileData in tileDatas) 
        {
            foreach (var tile in tileData.tiles) 
            {
                dataFromTiles.Add(tile, tileData);
            }
        }
    }
    private void Start()
    {
        StartCoroutine(changeTile());
    }

    public bool GetTileDealsDamage(Vector2 worldPosition) 
    {
        Vector3Int gridPosition = groundMap.WorldToCell(worldPosition);
        TileBase tile = groundMap.GetTile(gridPosition);
        if (tile == null || !dataFromTiles.ContainsKey(tile)) {
            return false;   
        }
        bool dealsDamage = dataFromTiles[tile].dealsDamage;
        return dealsDamage; 
    }

    IEnumerator changeTile() 
    {
        Color transparent_color = new Color32(211, 211, 211, 255);
        Color original_color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        int random_no;
        
        foreach (var round in mapPresets.map_preset) 
        {
            int temp_time = mapPresets.shrinkTime - 5;
            yield return new WaitForSeconds(temp_time);
            for (int i = 0; i < 5; i++) 
            {
                foreach (var tile in round.tiles) 
                {
                    groundMap.SetTileFlags(new Vector3Int(tile.x, tile.y, 0), TileFlags.None);
                    groundMap.SetColor(new Vector3Int(tile.x, tile.y, 0), transparent_color);
                }

                yield return new WaitForSeconds(0.5f);

                foreach (var tile in round.tiles) 
                {
                    groundMap.SetColor(new Vector3Int(tile.x, tile.y, 0), original_color);
                }
                
                yield return new WaitForSeconds(0.5f);

            }
            // yield return new WaitForSeconds(mapPresets.shrinkTime);
            foreach (var tile in round.tiles) 
            {
                random_no = Random.Range(0, 60);
                if (random_no == 0 && animatedTopTile != null) 
                {
                    groundMap.SetTile(new Vector3Int(tile.x, tile.y, 0), lavaTile);
                    topMap.SetTile(new Vector3Int(tile.x, tile.y, 0), animatedTopTile);    
                } 
                else 
                {
                    groundMap.SetTile(new Vector3Int(tile.x, tile.y, 0), lavaTile);
                    topMap.SetTile(new Vector3Int(tile.x, tile.y, 0), lavaTile);
                }
            }
        }
    }
    
}


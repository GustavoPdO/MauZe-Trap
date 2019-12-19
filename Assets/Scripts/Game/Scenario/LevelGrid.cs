using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    public GameObject tile;
    public GameObject rat;

    public int xSize = 1;
    public int ySize = 1;

    public Transform ratSpawn;
    public Enums.Direction ratDirection;

    public void BuildGrid()
    {
        DestroyGrid();
        for(int z = 0; z < ySize; z++){
            for(int x = 0; x < xSize; x++){
                GameObject newTile = Instantiate(tile, new Vector3(x, 0, -z), Quaternion.identity, gameObject.transform);
                newTile.name = "Tile " + x + '-' + z;
                TileBehaviour tileScript = newTile.GetComponent<TileBehaviour>();
                if(z == 0){
                    tileScript.setWall(Enums.Direction.North);
                }
                if(x == 0){
                    tileScript.setWall(Enums.Direction.West);
                }
                if(x == (xSize-1)){
                    tileScript.setWall(Enums.Direction.East);
                }
                if(z == (ySize-1)){
                    tileScript.setWall(Enums.Direction.South);
                }                
            }
        }
    }

    public void DestroyGrid()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
        foreach (GameObject tile in tiles)
        {
            DestroyImmediate(tile);
        }
    }

    public void PlaceRat() 
    {
        GameObject[] rats = GameObject.FindGameObjectsWithTag("Rat");
        foreach (GameObject rat in rats)
        {
            DestroyImmediate(rat);
        }
        Instantiate(rat, new Vector3(ratSpawn.position.x, ratSpawn.position.y+0.01f, ratSpawn.position.z), Quaternion.Euler(0, (float)ratDirection, 0));
    }
}

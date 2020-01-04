using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    public GameObject tile;
    public GameObject spawn;
    private GameObject parent;
    public bool gridCreated = false;

    public int xSize = 1;
    public int ySize = 1;
    public int xSpawn = 0;
    public int ySpawn = 0;

    public int row = 10;
    public int column = 0;

    public Transform ratSpawn;
    public Enums.Direction ratDirection;


    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        if(!gridCreated){
            Gizmos.DrawSphere(new Vector3(0, 0, 0), 0.1f);
        }
        else
        {
            for(int row = 0; row < ySize; row++){
                for(int column = 0; column <= xSize+1; column++){
                    drawGizmos(row, column-1);
                }
            }
        }
        
    }

    public void BuildGrid()
    {
        DestroyGrid();
        for(int row = 0; row <= ySize; row++){
            parent = new GameObject();
            parent.name = "Row " + row;
            parent.transform.SetParent(transform);
            for(int column = 0; column < xSize; column++){
                GameObject newTile = Instantiate(tile, new Vector3(column, 0, row), Quaternion.identity, parent.transform);
                newTile.name = "Tile " + row + "-" + column;                
                TileBehaviour tileBehaviour = newTile.GetComponent<TileBehaviour>();
                setBoundaryWalls(tileBehaviour, row, column);
            }
        }
        gridCreated = true;
        SetSpawn();
    }

    public void DestroyGrid()
    {
        gridCreated = false;
        for(int i = transform.childCount-1; i >= 0; i--)
        {
            Transform child = transform.GetChild(i);
            DestroyImmediate(child.gameObject);
        }
        DeleteSpawn();
    }

    public void DeleteSpawn()
    {
        GameObject[] points = GameObject.FindGameObjectsWithTag("Rat");

        foreach(var point in points)
        {
            DestroyImmediate(point);
        }
    }

    public void SetSpawn() 
    {
        DeleteSpawn();
        if(gridCreated){
            Instantiate(spawn, new Vector3(xSpawn, 0.01f, ySpawn), Quaternion.Euler(0, (float)ratDirection, 0));
        }
    }

    private void setBoundaryWalls(TileBehaviour tileBehaviour, int row, int column)
    {
        if(row == 0){
            tileBehaviour.setWall(Enums.Direction.South);
        }
        if(column == 0){
            tileBehaviour.setWall(Enums.Direction.West);
        }
        if(column == (xSize-1)){
            tileBehaviour.setWall(Enums.Direction.East);
        }
        if(row == (ySize-1)){
            tileBehaviour.setWall(Enums.Direction.North);
        }   
    }

    private void drawGizmos(int row, int column)
    {
        if(row == 0){
            Gizmos.DrawSphere(new Vector3(column, 0, -1), 0.1f);
        }
        if(column == 0){
            Gizmos.DrawSphere(new Vector3(-1, 0, row), 0.1f);
        }
        if(column == (xSize-1)){
            Gizmos.DrawSphere(new Vector3(xSize, 0, row), 0.1f);
        }
        if(row == (ySize-1)){
            Gizmos.DrawSphere(new Vector3(column, 0, ySize), 0.1f);
        }
        
    }
}

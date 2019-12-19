using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteAlways, SelectionBase]
public class TileBehaviour : MonoBehaviour
{
    private GameObject[] wallTypes = new GameObject[3];

    [SerializeField]
    private GameObject[] northWallList = null;
    [SerializeField]
    private GameObject[] eastWallList = null;
    [SerializeField]
    private GameObject[] southWallList = null;
    [SerializeField]
    private GameObject[] westWallList = null;

    [Space(10)]
    [Header("Wall Mounting")]
    [SerializeField]
    Enums.WallType northWall;
    [SerializeField]
    Enums.WallType eastWall;
    [SerializeField]
    Enums.WallType southWall;
    [SerializeField]
    Enums.WallType westWall;

    private GameObject floor;   
    int cont = 0;

    #if UNITY_EDITOR
    private void Update() 
    {        
        UpdateWalls();
    }
    #endif
    
    void UpdateWalls(){
        
        switch(northWall){
            case Enums.WallType.None:
                for(int i=0; i< northWallList.Length; i++){
                    northWallList[i].SetActive(false);
                }
                break;
            case Enums.WallType.Concrete:
                for(int i=0; i< northWallList.Length; i++){
                    northWallList[i].SetActive(false);
                }
                northWallList[0].SetActive(true);
                break;
            case Enums.WallType.Glass:
                for(int i=0; i< northWallList.Length; i++){
                    northWallList[i].SetActive(false);
                }
                northWallList[1].SetActive(true);
                break;
            case Enums.WallType.Trap:
                for(int i=0; i< northWallList.Length; i++){
                    northWallList[i].SetActive(false);
                }
                northWallList[2].SetActive(true);
                break;
        }

        switch(eastWall){
            case Enums.WallType.None:
                for(int i=0; i< eastWallList.Length; i++){
                    eastWallList[i].SetActive(false);
                }
                break;
            case Enums.WallType.Concrete:
                for(int i=0; i< eastWallList.Length; i++){
                    eastWallList[i].SetActive(false);
                }
                eastWallList[0].SetActive(true);
                break;
            case Enums.WallType.Glass:
                for(int i=0; i< eastWallList.Length; i++){
                    eastWallList[i].SetActive(false);
                }
                eastWallList[1].SetActive(true);
                break;
            case Enums.WallType.Trap:
                for(int i=0; i< eastWallList.Length; i++){
                    eastWallList[i].SetActive(false);
                }
                eastWallList[2].SetActive(true);
                break;
        }

        switch(southWall){
            case Enums.WallType.None:
                for(int i=0; i< southWallList.Length; i++){
                    southWallList[i].SetActive(false);
                }
                break;
            case Enums.WallType.Concrete:
                for(int i=0; i< southWallList.Length; i++){
                    southWallList[i].SetActive(false);
                }
                southWallList[0].SetActive(true);
                break;
            case Enums.WallType.Glass:
                for(int i=0; i< southWallList.Length; i++){
                    southWallList[i].SetActive(false);
                }
                southWallList[1].SetActive(true);
                break;
            case Enums.WallType.Trap:
                for(int i=0; i< southWallList.Length; i++){
                    southWallList[i].SetActive(false);
                }
                southWallList[2].SetActive(true);
                break;
        }

        switch(westWall){
            case Enums.WallType.None:
                for(int i=0; i< westWallList.Length; i++){
                    westWallList[i].SetActive(false);
                }
                break;
            case Enums.WallType.Concrete:
                for(int i=0; i< westWallList.Length; i++){
                    westWallList[i].SetActive(false);
                }
                westWallList[0].SetActive(true);
                break;
            case Enums.WallType.Glass:
                for(int i=0; i< westWallList.Length; i++){
                    westWallList[i].SetActive(false);
                }
                westWallList[1].SetActive(true);
                break;
            case Enums.WallType.Trap:
                for(int i=0; i< westWallList.Length; i++){
                    westWallList[i].SetActive(false);
                }
                westWallList[2].SetActive(true);
                break;
        }
        
      
    }

    public void setWall(Enums.Direction direction)
    {
        Debug.Log(direction);
        switch(direction){
            case Enums.Direction.North:
                northWall = Enums.WallType.Concrete;
                break;
            case Enums.Direction.East:
                eastWall = Enums.WallType.Concrete;
                break;
            case Enums.Direction.South:
                southWall = Enums.WallType.Concrete;
                break;
            case Enums.Direction.West:
                westWall = Enums.WallType.Concrete;
                break;
        }
    }
}

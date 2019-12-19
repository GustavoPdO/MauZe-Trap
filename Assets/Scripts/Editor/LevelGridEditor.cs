using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelGrid))]
public class LevelGridEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //DrawDefaultInspector();

        LevelGrid levelGrid = (LevelGrid)target;
        levelGrid.tile = EditorGUIUtility.Load("Assets/Prefabs/Scenarios/Floor.prefab") as GameObject;
        levelGrid.rat = EditorGUIUtility.Load("Assets/Prefabs/Characters/XicoGuita.prefab") as GameObject;

        GUILayout.Label("Grid Options", EditorStyles.boldLabel);
        levelGrid.xSize = EditorGUILayout.IntField("X Size", levelGrid.xSize);
        levelGrid.ySize = EditorGUILayout.IntField("Y Size", levelGrid.ySize);

        GUILayout.Space(20);
        if(GUILayout.Button("Generate Grid"))
        {
            levelGrid.BuildGrid();
        }
        if(GUILayout.Button("Destroy Grid"))
        {
            levelGrid.DestroyGrid();
        }

        GUILayout.Space(20);
        GUILayout.Label("Rat Options", EditorStyles.boldLabel);

        levelGrid.ratSpawn = EditorGUILayout.ObjectField("Rat Spawn", levelGrid.ratSpawn, typeof(Transform), true) as Transform;
        //levelGrid.ratYAngle = EditorGUILayout.FloatField("Rat Spawn Angle", levelGrid.ratYAngle);
        levelGrid.ratDirection = (Enums.Direction)EditorGUILayout.EnumPopup("Rat Direction", levelGrid.ratDirection);


        if(GUILayout.Button("Place Rat"))
        {
            levelGrid.PlaceRat();
        }
    
    }
}

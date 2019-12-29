using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelGrid))]
public class LevelGridEditor : Editor
{

    int test = 0;
    string[] options = new string[3] {"1", "2", "3"};
    LevelGrid levelGrid;

    private void OnEnable() {
        levelGrid = (LevelGrid)target;
        levelGrid.tile = EditorGUIUtility.Load("Assets/Prefabs/Scenarios/Floor.prefab") as GameObject;
        levelGrid.spawn = EditorGUIUtility.Load("Assets/Prefabs/Characters/XicoGuita.prefab") as GameObject;
    }

    public override void OnInspectorGUI()
    {
        //DrawDefaultInspector();

        levelGrid.gridCreated = EditorGUILayout.Toggle("grid", levelGrid.gridCreated);

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

        EditorGUI.BeginChangeCheck();
        GUILayout.Space(20);
        GUILayout.Label("Spawn Position", EditorStyles.boldLabel);
        levelGrid.xSpawn = EditorGUILayout.IntSlider("X Position", levelGrid.xSpawn, 0, levelGrid.xSize-1);
        levelGrid.ySpawn = EditorGUILayout.IntSlider("Y Position", levelGrid.ySpawn, 0, levelGrid.ySize-1);
        levelGrid.ratDirection = (Enums.Direction)EditorGUILayout.EnumPopup("Rat Direction", levelGrid.ratDirection);

        if(EditorGUI.EndChangeCheck())
        {
            levelGrid.SetSpawn();
        }
      
    }   
    
}

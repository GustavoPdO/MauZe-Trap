using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;


public class LevelCreator : EditorWindow
{
    // Grid config attributes
    int x_Size;
    int y_Size;
    int[,] gridMapping;
    bool gridCreated = false;

    // Game objects for spawning logic
    GameObject level;
    GameObject tile;
    GameObject spawn;
    GameObject pivot;
    GameObject cheese;

    [MenuItem("Tools/LevelCreator %_q")]
    public static void ShowWindow()
    {
        // Instantitate the editor windows when selected on menu or called by shortkeys
        LevelCreator window = GetWindow<LevelCreator>();
        window.titleContent = new GUIContent("LevelCreator");
        window.minSize = new Vector2(370, 460);
        window.maxSize = new Vector2(370, 460);
    }

    public void OnEnable()
    {
        // Search for all game objects prefabs
        level = EditorGUIUtility.Load("Assets/Prefabs/Scenarios/Level.prefab") as GameObject;
        tile = EditorGUIUtility.Load("Assets/Prefabs/Scenarios/Floor.prefab") as GameObject;
        pivot = EditorGUIUtility.Load("Assets/Prefabs/Scenarios/Pivot.prefab") as GameObject;
        spawn = EditorGUIUtility.Load("Assets/Prefabs/Characters/XicoGuita.prefab") as GameObject;
        cheese = EditorGUIUtility.Load("Assets/Prefabs/Characters/Cheese.prefab") as GameObject;

        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;
        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/Editor/LevelCreator/LevelCreator.uxml");
        VisualElement window = visualTree.CloneTree();
        // Import USS
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/Editor/LevelCreator/LevelCreator.uss");
        window.styleSheets.Add(styleSheet);
        // Add the UXML with USS to rootVisualElement     
        root.Add(window);

        // Grid size fields
        // Query for size integer fields, add a listener for changes on its fields and assign them to size variables
        // -> Still in need to make this window data persistent
        var y = root.Q<IntegerField>("Y Size");
        var x = root.Q<IntegerField>("X Size");
        x.RegisterValueChangedCallback(e => x_Size = e.newValue <= 15? e.newValue : 15);
        y.RegisterValueChangedCallback(e => y_Size = e.newValue <= 15? e.newValue : 15);        

        SetupGridButton(root);
        root.Query<IntegerField>(className: "grid-input").ForEach(
            field => field.RegisterValueChangedCallback(
                e => SetupGridButton(root)
            )
        );

        // Grid creation button method assigning
        var gridCreationButton = root.Q<Button>(className: "creation-button");
        gridCreationButton.clickable.clicked += () => BuildGrid();
    }

    // Query for the Box element on the rootVisualElement and then instantiate the grid for map creation
    private void SetupGridButton(VisualElement root)
    {
        root.Q<Box>().Clear();
        gridMapping = new int[x_Size, y_Size];

        for(int y= 0; y < y_Size; y++)
        {
            var row = new VisualElement();
            row.AddToClassList("grid-row");
            root.Q<Box>().Add(row);      

            for(int x = x_Size-1; x >= 0; x--)
            {
                var button = new Button();
                button.AddToClassList("grid");
                button.text = gridMapping[x, y].ToString();
                button.viewDataKey = x.ToString() + y.ToString();
                button.clickable.clicked += () => GridClick(button);
                row.Add(button);
            }
        }
    }

    // Get the current text from the button and change accordingly, along with USS classes to change its background colors
    private void GridClick(Button button)
    {
        int[] position = new int[2];
        position[0] = int.Parse(button.viewDataKey.Substring(0, 1));
        position[1] = int.Parse(button.viewDataKey.Substring(1));

        switch(button.text)
        {
            case "0":
                button.text = "1";
                gridMapping[position[0],position[1]] = 1;
                button.ToggleInClassList("tile");
                return;
            case "1":
                button.text = "2";
                gridMapping[position[0],position[1]] = 2;
                button.ToggleInClassList("tile");
                button.ToggleInClassList("rat");
                return;
            case "2":
                button.text = "3";
                gridMapping[position[0],position[1]] = 3;
                button.ToggleInClassList("rat");
                button.ToggleInClassList("corner");
                return;
            case "3":
                button.text = "4";
                gridMapping[position[0],position[1]] = 4;
                button.ToggleInClassList("corner");
                button.ToggleInClassList("cheese");
                return;
            case "4":
                button.text = "0";
                gridMapping[position[0],position[1]] = 0;
                button.ToggleInClassList("cheese");
                return;            
        }
    }

    // Run for the gridMapping array reading each index value and instantiating the accordingly game object
    private void BuildGrid()
    {
        // Delete the grid if there was one previously instantiated  
        DestroyGrid();
        
        GameObject newLevel = Instantiate(level, Vector3.zero, Quaternion.identity); // This GO comes tagged as "Tiles"
        newLevel.name = "Level";
        GameObject tiles = GameObject.Find("Tiles");

        int pivotCount = 1;
        int cheeseCount = 1;

        for(int row = 0; row < x_Size; row++){
            GameObject parent = new GameObject();
            parent.name = "Row " + row;
            parent.transform.SetParent(tiles.transform);
            
            for(int column = 0; column < y_Size; column++){
                int[] position = new int[2];

                if(gridMapping[row,column] == 1 || gridMapping[row,column] == 2) // The Rat game object (no 2) can only spawn where there's also a tile
                {
                    GameObject newTile = Instantiate(tile, new Vector3(column, 0, row), Quaternion.identity, parent.transform);
                    newTile.name = "Tile " + row + "-" + column;                
                    TileBehaviour tileBehaviour = newTile.GetComponent<TileBehaviour>();
                    setBoundaryWalls(tileBehaviour, row, column);

                    gridCreated = true;
                }
                if(gridMapping[row,column] == 2)
                {
                    SetSpawn(row, column, newLevel);
                }
                if(gridMapping[row,column] == 3)
                {
                    GameObject trail = GameObject.Find("Trail");
                    GameObject newPivot = Instantiate(pivot, new Vector3(column, 0 , row), Quaternion.identity, trail.transform);
                    newPivot.name = "Pivot-" + pivotCount++;
                }
                if(gridMapping[row,column] == 4)
                {
                    GameObject levelCheese = Instantiate(cheese, new Vector3(column, 0.25f, row), Quaternion.identity, newLevel.transform);
                    levelCheese.name = "Cheese-" + cheeseCount++;
                }
            }
        }
        
        
    }

    private void DestroyGrid()
    {
        level = GameObject.FindGameObjectWithTag("Grid");
        DestroyImmediate(level);
        level = EditorGUIUtility.Load("Assets/Prefabs/Scenarios/Level.prefab") as GameObject;
        gridCreated = false;
        DeleteSpawn();
    }

    private void DeleteSpawn()
    {
        GameObject[] points = GameObject.FindGameObjectsWithTag("Rat");

        foreach(var point in points)
        {
            DestroyImmediate(point);
        }
    }

    private void SetSpawn(int x, int y, GameObject level) 
    {
        DeleteSpawn();
        if(gridCreated){
            GameObject rat = Instantiate(spawn, new Vector3(y, 0.01f, x), Quaternion.identity, level.transform);
            rat.name = "Xico the Rat";
        }
    }

    private void setBoundaryWalls(TileBehaviour tileBehaviour, int row, int column)
    {
        if(row == 0 || (gridMapping[row-1, column] != 1 && gridMapping[row-1, column] != 2)){
            tileBehaviour.setWall(Enums.Direction.South);
        }
        if(column == 0 || (gridMapping[row, column-1] != 1 && gridMapping[row, column-1] != 2)){
            tileBehaviour.setWall(Enums.Direction.West);
        }
        if(column == (y_Size-1) || (gridMapping[row, column+1] != 1 && gridMapping[row, column+1] != 2)){
            tileBehaviour.setWall(Enums.Direction.East);
        }
        if(row == (x_Size-1) || (gridMapping[row+1, column] != 1 && gridMapping[row+1, column] != 2)){
            tileBehaviour.setWall(Enums.Direction.North);
        }   
    }
}
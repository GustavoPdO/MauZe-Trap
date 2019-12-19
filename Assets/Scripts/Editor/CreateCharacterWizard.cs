using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateCharacterWizard : ScriptableWizard
{
    public Texture2D portraitTexture;
    public Color color = Color.white;
    public string nickname = "Default Nickname";


    [MenuItem("My Tools/ Create Character Wizard...")]
    static void CreateWizard() {
        ScriptableWizard.DisplayWizard<CreateCharacterWizard>("Create Character", "Create new", "Update selected");
    }

    private void OnWizardCreate() {
        GameObject characterGO = new GameObject();

        Character characterComponent = characterGO.AddComponent<Character> ();
        characterComponent.portrait = portraitTexture;
        characterComponent.color = color;
        characterComponent.nickname = nickname;

        CheeseMovement cheeseMovement = characterGO.AddComponent<CheeseMovement>();
        characterComponent.cheeseMovement = cheeseMovement;
    }

    private void OnWizardOtherButton() {
        if(Selection.activeTransform)
        {
            Character characterComponent = Selection.activeTransform.GetComponent<Character>();

            if(characterComponent)
            {
                characterComponent.portrait = portraitTexture;
                characterComponent.color = color;
                characterComponent.nickname = nickname;
            }
        }
    }

    private void OnWizardUpdate() {
        helpString = "Enter character details";
    }
}

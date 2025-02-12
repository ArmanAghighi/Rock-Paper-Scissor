using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class CharacterEditorWindow : EditorWindow
{
    private string newCharacterName = "";
    private Sprite newCharacterSprite;
    private CharacterCategory newCharacterCategory = CharacterCategory.Unknown;

    private List<HeroScriptalbeObjects> allCharacters = new List<HeroScriptalbeObjects>();
    private Dictionary<HeroScriptalbeObjects, List<CharacterCategory>> defeatRelations = new Dictionary<HeroScriptalbeObjects, List<CharacterCategory>>();
    private Dictionary<HeroScriptalbeObjects, List<CharacterCategory>> defeatedByRelations = new Dictionary<HeroScriptalbeObjects, List<CharacterCategory>>();

    private Vector2 scrollPosition;
    private string savePath = "Assets/ScriptableObjects/";

    [MenuItem("Game Tools/Character Editor")]
    public static void ShowWindow()
    {
        GetWindow<CharacterEditorWindow>("Character Editor");
    }

    private void OnEnable()
    {
        LoadCharacters();
    }

    private void LoadCharacters()
    {
        allCharacters.Clear();
        defeatRelations.Clear();
        defeatedByRelations.Clear();

        string[] guids = AssetDatabase.FindAssets("t:HeroScriptalbeObjects", new[] { savePath });
        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            HeroScriptalbeObjects character = AssetDatabase.LoadAssetAtPath<HeroScriptalbeObjects>(assetPath);
            if (character != null)
            {
                allCharacters.Add(character);
                defeatRelations[character] = new List<CharacterCategory>(character.CanDefeatEnemyList);
                defeatedByRelations[character] = new List<CharacterCategory>(character.CanBeDefeatedByEnemyList);
            }
        }
    }

    private void OnGUI()
    {
        GUILayout.Label("Character Editor", EditorStyles.boldLabel);

        scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Height(300));
        DrawCreateCharacterForm();
        GUILayout.EndScrollView();

        GUILayout.Space(10);
        DrawCharacterTable();
    }

    private void DrawCharacterTable()
    {
        EditorGUILayout.BeginVertical("box");
        GUILayout.Label("Existing Characters", EditorStyles.boldLabel);

        if (allCharacters.Count == 0)
        {
            GUILayout.Label("No characters found.");
            EditorGUILayout.EndVertical();
            return;
        }

        foreach (var character in allCharacters)
        {
            EditorGUILayout.BeginVertical("box");
            GUILayout.Label(character.hero_Name, EditorStyles.boldLabel);

            GUILayout.Label("Beats:");
            DrawMultiSelection(character, defeatRelations[character]);

            GUILayout.Label("Loses To:");
            DrawMultiSelection(character, defeatedByRelations[character]);

            EditorGUILayout.EndVertical();
        }

        if (GUILayout.Button("Save Battle Relations"))
        {
            SaveBattleRelations();
        }

        EditorGUILayout.EndVertical();
    }

    private void DrawMultiSelection(HeroScriptalbeObjects character, List<CharacterCategory> selectionList)
    {
        EditorGUILayout.BeginHorizontal();
        foreach (CharacterCategory category in System.Enum.GetValues(typeof(CharacterCategory)))
        {
            if (category == CharacterCategory.Unknown) continue;

            bool isSelected = selectionList.Contains(category);
            bool newSelection = GUILayout.Toggle(isSelected, category.ToString(), "Button");

            if (newSelection && !isSelected)
                selectionList.Add(category);
            else if (!newSelection && isSelected)
                selectionList.Remove(category);
        }
        EditorGUILayout.EndHorizontal();
    }

    private void DrawCreateCharacterForm()
    {
        GUILayout.Label("Create New Character", EditorStyles.boldLabel);

        newCharacterName = EditorGUILayout.TextField("Name", newCharacterName);
        newCharacterSprite = (Sprite)EditorGUILayout.ObjectField("Sprite", newCharacterSprite, typeof(Sprite), false);
        newCharacterCategory = (CharacterCategory)EditorGUILayout.EnumPopup("Category", newCharacterCategory);

        if (GUILayout.Button("Save Character"))
        {
            SaveCharacter();
        }
    }

    private void SaveCharacter()
    {
        if (string.IsNullOrEmpty(newCharacterName) || newCharacterSprite == null || newCharacterCategory == CharacterCategory.Unknown)
        {
            Debug.LogWarning("Fill all fields before saving.");
            return;
        }

        if (!Directory.Exists(savePath))
            Directory.CreateDirectory(savePath);

        HeroScriptalbeObjects newCharacter = ScriptableObject.CreateInstance<HeroScriptalbeObjects>();
        newCharacter.hero_Name = newCharacterName;
        newCharacter.hero_Sprite = newCharacterSprite;
        newCharacter.hero_Catagory = newCharacterCategory;
        newCharacter.CanDefeatEnemyList = new List<CharacterCategory>();
        newCharacter.CanBeDefeatedByEnemyList = new List<CharacterCategory>();

        string assetPath = $"{savePath}{newCharacterName}.asset";
        AssetDatabase.CreateAsset(newCharacter, assetPath);
        AssetDatabase.SaveAssets();

        LoadCharacters();
    }

    private void SaveBattleRelations()
    {
        foreach (var character in allCharacters)
        {
            character.CanDefeatEnemyList.Clear();
            character.CanBeDefeatedByEnemyList.Clear();

            character.CanDefeatEnemyList.AddRange(defeatRelations[character]);
            character.CanBeDefeatedByEnemyList.AddRange(defeatedByRelations[character]);

            EditorUtility.SetDirty(character);
        }

        AssetDatabase.SaveAssets();
        Debug.Log("Battle relations saved!");
    }
}

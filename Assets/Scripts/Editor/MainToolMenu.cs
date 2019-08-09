using UnityEngine;
using UnityEditor;

public class MainToolMenu
{
    private const string pathResourcesFolder = "Assets/Resources";

    private const string menuTitle = "Game Tools/";

    [MenuItem(menuTitle + "!! Create Game Settings !!", false, 530)]
    static void CreateGameData()
    {
        GameSettings asset = ScriptableObject.CreateInstance<GameSettings>();

        AssetDatabase.CreateAsset(asset, "Assets/Resources/gamesettings.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }


    [MenuItem(menuTitle + "Open Game Settings", false, 410)]
    static void OpenGameData()
    {
        GameSettings asset = Resources.Load<GameSettings>("gamesettings");

        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }

}

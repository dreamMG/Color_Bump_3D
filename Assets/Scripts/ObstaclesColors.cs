using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[ExecuteInEditMode]
public class ObstaclesColors : EditorWindow
{
    Material material_1;
    Material material_2;

    [MenuItem("Window/ObstaclesColors %a")]
    public static void ShowWindow()
    {
        GetWindow<ObstaclesColors>("ObstaclesColors");
    }

    private void OnGUI()
    {
        GUILayout.Label("Color the selected parent Object or one!", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();

        material_1 = (Material)EditorGUILayout.ObjectField(material_1, typeof(Material), true);
        if (GUILayout.Button("SetMaterial!"))
        {
            SetMaterial(1);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        material_2 = (Material)EditorGUILayout.ObjectField(material_2, typeof(Material), true);
        if (GUILayout.Button("SetMaterial!"))
        {
            SetMaterial(2);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(20);
        GUILayout.Label("Change color faster by Michał Gabryś", EditorStyles.helpBox);
        GUILayout.Label("Helper for player prefs");
        GUILayout.Label("Score lv 1  " + PlayerPrefs.GetFloat("Lvl0"));
        GUILayout.Label("Score lv 2  " + PlayerPrefs.GetFloat("Lvl1"));
        GUILayout.Label("Helper for player prefs");
        if (GUILayout.Button("Delete Player Perfs!"))
        {
            PlayerPrefs.DeleteAll();
        }

    }

    void SetMaterial(int index)
    {
        foreach (GameObject obj in Selection.gameObjects)
        {
            Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();

            if (renderers != null)
            {
                switch (index)
                {
                    case (1):
                        for (int i = 0; i < renderers.Length; i++)
                        {
                            renderers[i].sharedMaterial = material_1;
                        }
                        break;
                    case (2):
                        for (int i = 0; i < renderers.Length; i++)
                        {
                            renderers[i].sharedMaterial = material_2;
                        }
                        break;
                }
            }
        }
    }
}
#endif
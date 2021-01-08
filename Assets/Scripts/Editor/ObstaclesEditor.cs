using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor(typeof(Obstacles)), CanEditMultipleObjects]
public class ObstaclesEditor : Editor
{

    public SerializedProperty
        type_Prop,
        materials_Prop,
        moveX_Prop,
        moveAndBack_Prop,
        speed_Prop,
        rotate_Prop,
        player_Prop;


    void OnEnable()
    {
        // Setup the SerializedProperties
        type_Prop = serializedObject.FindProperty("type");
        materials_Prop = serializedObject.FindProperty("materials");
        moveX_Prop = serializedObject.FindProperty("moveX");
        moveAndBack_Prop = serializedObject.FindProperty("moveAndBack");
        speed_Prop = serializedObject.FindProperty("speed");
        rotate_Prop = serializedObject.FindProperty("rotate");
        player_Prop = serializedObject.FindProperty("player");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(type_Prop);

        Obstacles.Type type = (Obstacles.Type)type_Prop.intValue;

        switch (type)
        {
            case Obstacles.Type.ChangeColor:
                EditorGUILayout.PropertyField(materials_Prop, new GUIContent("Materials"));
                EditorGUILayout.Slider(speed_Prop, 0, 5, new GUIContent("Speed"));
                break;

            case Obstacles.Type.Move:
                EditorGUILayout.PropertyField(moveX_Prop, new GUIContent("MoveX"));
                EditorGUILayout.PropertyField(moveAndBack_Prop, new GUIContent("Move And Back"));
                EditorGUILayout.Slider(speed_Prop, 0, 10, new GUIContent("SpeedMove"));
                break;
            case Obstacles.Type.Tilt:
                EditorGUILayout.Slider(rotate_Prop, -10, 10, new GUIContent("Rotate"));
                EditorGUILayout.PropertyField(player_Prop, new GUIContent("Player"));
                break;
        }


        serializedObject.ApplyModifiedProperties();
    }
}
#endif
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapGenerator))]
public class MapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MapGenerator displayScript = (MapGenerator)target;
        base.OnInspectorGUI();
        if (GUILayout.Button("Generate"))
            displayScript.GenerateMap();
        if (GUILayout.Button("Random Seed Map"))
            displayScript.RandomMap();

    }
}

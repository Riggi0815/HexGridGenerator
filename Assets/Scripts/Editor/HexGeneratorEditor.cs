using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HexGenerator))]
public class HexGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw the default inspector
        DrawDefaultInspector();

        // Get the reference to our script
        HexGenerator hexGenerator = (HexGenerator)target;

        // Add some space
        EditorGUILayout.Space();

        // Create a button
        if (GUILayout.Button("Generate Hex Grid", GUILayout.Height(30)))
        {
            // Call the generate method when the button is clicked
            hexGenerator.GenerateHexGrid();
        }
        if (GUILayout.Button("Clear Hex Grid", GUILayout.Height(30)))
        {
            // Call the clear method when the button is clicked
            hexGenerator.ClearAllChildren();
        }
    }
}
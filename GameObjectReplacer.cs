using UnityEngine;
using UnityEditor;

public class GameObjectReplacer : EditorWindow
{
    // Prefab to replace selected objects with
    private GameObject prefab;

    // Options for matching properties from the original object to the prefab
    private bool matchPosition = true;
    private bool matchRotation = true;
    private bool matchScale = true;

    // Adds the custom editor window to the Unity menu
    [MenuItem("Tools/GameObject Replacer")]
    static void OpenWindow()
    {
        // Open or create the window
        GameObjectReplacer window = (GameObjectReplacer)EditorWindow.GetWindow(typeof(GameObjectReplacer));
        window.Show();
    }

    // Draws the UI elements for the custom editor window
    private void OnGUI()
    {
        // Warn user if in Play mode (this tool works only in Edit mode)
        if (EditorApplication.isPlaying)
        {
            EditorGUILayout.HelpBox("GameObject Replacer only works in Edit mode!", MessageType.Warning);
            return;
        }

        // Input field for selecting the prefab
        prefab = (GameObject)EditorGUILayout.ObjectField("Prefab", prefab, typeof(GameObject), false);

        // Toggles for matching the transform properties
        matchPosition = EditorGUILayout.Toggle("Match Position", matchPosition);
        matchRotation = EditorGUILayout.Toggle("Match Rotation", matchRotation);
        matchScale = EditorGUILayout.Toggle("Match Scale", matchScale);

        // Button to execute the replacement operation
        if (GUILayout.Button("Replace"))
        {
            ReplaceSelectedObjects();
        }
    }

    // Replaces the selected game objects with the specified prefab
    private void ReplaceSelectedObjects()
    {
        // Check if a prefab has been assigned
        if (prefab == null)
        {
            Debug.LogError("No prefab selected!");
            return;
        }

        // Record the current selection state for undo operations
        Undo.RecordObjects(Selection.gameObjects, "Replace With Prefab");

        // Iterate through each selected game object
        foreach (GameObject go in Selection.gameObjects)
        {
            // Instantiate the prefab as a new game object
            GameObject newObject = (GameObject)PrefabUtility.InstantiatePrefab(prefab);

            // Verify that the prefab was successfully instantiated
            if (newObject == null)
            {
                Debug.LogError("Failed to instantiate prefab");
                continue;
            }

            // Register the newly created object for undo operations
            Undo.RegisterCreatedObjectUndo(newObject, "Replace With Prefab");

            // Match the position of the original object if the option is enabled
            if (matchPosition)
                newObject.transform.position = go.transform.position;

            // Match the rotation of the original object if the option is enabled
            if (matchRotation)
                newObject.transform.rotation = go.transform.rotation;

            // Match the scale of the original object if the option is enabled
            if (matchScale)
                newObject.transform.localScale = go.transform.localScale;

            // Assign the new object to the same parent as the original object
            newObject.transform.SetParent(go.transform.parent);

            // Destroy the original object and register it for undo operations
            Undo.DestroyObjectImmediate(go);
        }
    }
}

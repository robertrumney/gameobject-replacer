using UnityEngine;
using UnityEditor;

public class GameObjectReplacer : EditorWindow
{
    private GameObject prefab;
    private bool matchPosition = true;
    private bool matchRotation = true;
    private bool matchScale = true;

    [MenuItem("Window/GameObject Replacer")]
    static void OpenWindow()
    {
        GameObjectReplacer window = (GameObjectReplacer)EditorWindow.GetWindow(typeof(GameObjectReplacer));
        window.Show();
    }

    void OnGUI()
    {
        // Ensure it only works in Edit mode
        if (EditorApplication.isPlaying)
        {
            EditorGUILayout.HelpBox("GameObject Replacer only works in Edit mode!", MessageType.Warning);
            return;
        }

        prefab = (GameObject)EditorGUILayout.ObjectField("Prefab", prefab, typeof(GameObject), false);

        matchPosition = EditorGUILayout.Toggle("Match Position", matchPosition);
        matchRotation = EditorGUILayout.Toggle("Match Rotation", matchRotation);
        matchScale = EditorGUILayout.Toggle("Match Scale", matchScale);

        if (GUILayout.Button("Replace"))
        {
            ReplaceSelectedObjects();
        }
    }

    void ReplaceSelectedObjects()
    {
        if (prefab == null)
        {
            Debug.LogError("No prefab selected!");
            return;
        }

        // Using Undo to ensure no hidden objects remain in the scene
        Undo.RecordObjects(Selection.gameObjects, "Replace With Prefab");

        foreach (GameObject go in Selection.gameObjects)
        {
            GameObject newObject = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
            if (newObject == null)
            {
                Debug.LogError("Failed to instantiate prefab");
                continue;
            }

            Undo.RegisterCreatedObjectUndo(newObject, "Replace With Prefab");

            if (matchPosition)
                newObject.transform.position = go.transform.position;

            if (matchRotation)
                newObject.transform.rotation = go.transform.rotation;

            if (matchScale)
                newObject.transform.localScale = go.transform.localScale;

            newObject.transform.SetParent(go.transform.parent);

            Undo.DestroyObjectImmediate(go);
        }
    }
}

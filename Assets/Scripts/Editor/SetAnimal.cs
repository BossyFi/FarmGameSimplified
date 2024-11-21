using UnityEngine;
using UnityEditor;

public class AddScriptsTool : EditorWindow
{
    private GameObject targetGameObject;

    // Lista de scripts que deseas añadir
    private static readonly string[] scriptsToAdd =
    {
        "Animal.AnimalBase", // Con namespace
        "Clickable", // Sin namespace
        "Unity.Behavior.BehaviorGraphAgent" // Con namespace
    };

    [MenuItem("Tools/Add Scripts Tool")]
    public static void ShowWindow()
    {
        GetWindow<AddScriptsTool>("Add Scripts Tool");
    }

    private void OnGUI()
    {
        GUILayout.Label("Add Scripts to GameObject", EditorStyles.boldLabel);

        // Campo para seleccionar un GameObject
        targetGameObject =
            (GameObject)EditorGUILayout.ObjectField("Target GameObject", targetGameObject, typeof(GameObject), true);

        if (GUILayout.Button("Add Scripts"))
        {
            if (targetGameObject != null)
            {
                AddScriptsToGameObject(targetGameObject);
            }
            else
            {
                Debug.LogWarning("Please select a GameObject.");
            }
        }
    }

    private void AddScriptsToGameObject(GameObject obj)
    {
        foreach (var scriptName in scriptsToAdd)
        {
            // Intenta obtener el tipo incluyendo el namespace
            var scriptType = System.Type.GetType(scriptName);

            // Si no se encuentra el script, busca solo por el nombre sin namespace
            if (scriptType == null)
            {
                scriptType = FindTypeByName(scriptName);
            }

            if (scriptType == null)
            {
                Debug.LogError($"Script '{scriptName}' not found.");
                continue;
            }

            // Verifica si ya existe el componente en el GameObject
            if (obj.GetComponent(scriptType) != null)
            {
                Debug.Log($"'{scriptName}' is already assigned to {obj.name}. Skipping.");
                continue;
            }

            // Si no existe, lo añade
            obj.AddComponent(scriptType);
            Debug.Log($"Added '{scriptName}' to {obj.name}.");
        }
    }

    private System.Type FindTypeByName(string name)
    {
        // Busca en todos los ensamblados cargados
        foreach (var assembly in System.AppDomain.CurrentDomain.GetAssemblies())
        {
            var type = assembly.GetType(name);
            if (type != null)
            {
                return type;
            }
        }

        // Si no se encuentra, devuelve null
        return null;
    }
}
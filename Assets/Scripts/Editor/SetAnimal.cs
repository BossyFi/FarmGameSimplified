using UnityEditor;
using UnityEngine;

public class AddScriptsAndPrefabsTool : EditorWindow
{
    // Variables para los scripts
    private static readonly string[] scriptsToAdd =
    {
        "Animal.AnimalBase", // Con namespace
        "Clickable", // Sin namespace
        "Unity.Behavior.BehaviorGraphAgent" // Con namespace
    };

    private string prefabsFolderPath = "Assets/Prefabs/Animals"; // Carpeta de prefabs

    [MenuItem("Tools/Add Scripts and Prefabs")]
    public static void ShowWindow()
    {
        GetWindow<AddScriptsAndPrefabsTool>("Add Scripts and Prefabs");
    }

    private void OnGUI()
    {
        GUILayout.Label("Add Scripts and Prefabs", EditorStyles.boldLabel);

        // Botón para añadir los scripts y prefabs
        if (GUILayout.Button("Add Scripts and Prefabs to Selected GameObject"))
        {
            AddScriptsAndPrefabs();
        }
    }

    private void AddScriptsAndPrefabs()
    {
        if (Selection.activeGameObject == null)
        {
            Debug.LogError("No GameObject selected!");
            return;
        }

        GameObject selectedObject = Selection.activeGameObject;

        // Añadir los scripts
        AddScriptsToGameObject(selectedObject);

        // Buscar y añadir los prefabs
        AddPrefabsFromFolder(selectedObject);
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

    private void AddPrefabsFromFolder(GameObject parent)
    {
        // Buscar todos los prefabs en la carpeta
        string[] prefabGUIDs = AssetDatabase.FindAssets("t:Prefab", new[] { prefabsFolderPath });

        if (prefabGUIDs.Length == 0)
        {
            Debug.LogWarning($"No prefabs found in folder {prefabsFolderPath}");
            return;
        }

        foreach (string guid in prefabGUIDs)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);

            if (prefab != null)
            {
                InstantiatePrefabAsChild(prefab, parent);
            }
            else
            {
                Debug.LogError($"Failed to load prefab at {assetPath}");
            }
        }
    }

    private void InstantiatePrefabAsChild(GameObject prefab, GameObject parent)
    {
        GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab); // Instanciar el prefab
        if (instance != null)
        {
            instance.transform.SetParent(parent.transform); // Hacerlo hijo del GameObject seleccionado
            instance.transform.localPosition = Vector3.zero; // Opcional: Resetear posición local
            instance.transform.localRotation = Quaternion.identity; // Opcional: Resetear rotación local
            Debug.Log($"Prefab {prefab.name} added as a child of {parent.name}.");
        }
        else
        {
            Debug.LogError($"Failed to instantiate prefab {prefab.name}.");
        }
    }
}
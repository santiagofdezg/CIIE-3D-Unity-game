
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class TerrainGrassMenu : MonoBehaviour {
    
    [MenuItem ("Terrain/Add Folder Detail (Grass)")]
    static void AddFolderDetails() {
        string folder = EditorUtility.OpenFolderPanel("Select the folder containing the grass", "Assets/", "");
        if(folder != "") {
            if(folder.IndexOf(Application.dataPath) == -1) {
                Debug.LogWarning("The folder must be in this project anywhere inside the Assets folder!");
                return;
            }
			
            string[] files = Directory.GetFiles(folder);
            if(files.Length > 0) {
                TerrainData currentTerrainData = Selection.activeGameObject.GetComponent<Terrain>().terrainData;
                List<DetailPrototype> detailPrototypesList = new List<DetailPrototype>(currentTerrainData.detailPrototypes);
                
				for(int i = 0; i < files.Length; i++) {
                    DetailPrototype detailPrototype = new DetailPrototype();
                    string relativePath = files[i].Substring(files[i].IndexOf("Assets/"));
                    Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(relativePath);
                    if(texture != null) {
                        detailPrototype.prototypeTexture= texture;
                        detailPrototypesList.Add(detailPrototype);
                    }
                }
                currentTerrainData.detailPrototypes = detailPrototypesList.ToArray();
                Selection.activeGameObject.GetComponent<Terrain>().Flush();
                currentTerrainData.RefreshPrototypes();
                EditorUtility.SetDirty(Selection.activeGameObject.GetComponent<Terrain>());
            }
        }
    }
 
    [MenuItem ("Terrain/Clear Details (Grass) Editor")]
    static void ClearGrassEditor() {
        TerrainData currentTerrainData = Selection.activeGameObject.GetComponent<Terrain>().terrainData;
        currentTerrainData.detailPrototypes = null;
        Selection.activeGameObject.GetComponent<Terrain>().Flush();
        currentTerrainData.RefreshPrototypes();
        EditorUtility.SetDirty(Selection.activeGameObject.GetComponent<Terrain>());
    }
 
    [MenuItem ("Terrain/Add Folder Detail (Grass)", true)]
    static bool ValidateAddFolderDetails() {
        if(Selection.activeGameObject == null || Selection.activeGameObject.GetComponent<Terrain>() == null) {
            Debug.LogWarning("You must have a Terrain selected to perform this action!");
            return false;
        }
        return true;
    }
 
    [MenuItem ("Terrain/Clear Details (Grass) Editor", true)]
    static bool ValidateClearDetailEditor() {
        if(Selection.activeGameObject == null || Selection.activeGameObject.GetComponent<Terrain>() == null) {
            Debug.LogWarning("You must have a Terrain selected to perform this action!");
            return false;
        }
        return true;
    }
}

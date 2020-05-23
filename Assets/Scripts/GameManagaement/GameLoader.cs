using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameLoader : MonoBehaviour
{   
    public static GameLoader instance;
    public GameObject loadingScreen;
    public Slider progressBar;

    private void Awake(){
        instance = this;
        SceneManager.LoadSceneAsync((int) SceneIndexes.MENU, LoadSceneMode.Additive);
    }


    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();
    public void LoadGame(){

        LoadScene((int) SceneIndexes.MENU, (int) SceneIndexes.LEVEL_1);
    }

    public void LoadMenu(int actualScene){


        LoadScene(actualScene, (int) SceneIndexes.MENU);

    }

    public void LoadDeathMenu(int actualScene){


        LoadScene(actualScene, (int) SceneIndexes.DEATH);



    }

    
    public void LoadEndMenu(int actualScene){


        LoadScene(actualScene, (int) SceneIndexes.END);

    }

    public void LoadNextScene(int actualScene){

        LoadScene(actualScene, actualScene+1);

    }

    public void LoadLastScene(int actualScene){
        LoadScene(actualScene, PlayerPrefs.GetInt("lastScene"));
    }


    private void LoadScene(int actualScene, int nextScene){
        loadingScreen.gameObject.SetActive(true);
        Debug.Log(actualScene);
        scenesLoading.Add(SceneManager.UnloadSceneAsync(actualScene));
        scenesLoading.Add(SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Additive));


        PlayerPrefs.SetInt("lastScene", actualScene);

        StartCoroutine(GetSceneLoadProgress(nextScene));
    }
    



    float totalProgress;
    public IEnumerator GetSceneLoadProgress(int nextScene){
        for (int i=0; i<scenesLoading.Count; i++){

            while (!scenesLoading[i].isDone){

                totalProgress = 0;

                foreach(AsyncOperation operation in scenesLoading){

                    totalProgress += operation.progress;
                }

                totalProgress /= scenesLoading.Count;

                progressBar.value = totalProgress;

                yield return null;
            }
        }
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(nextScene));

            loadingScreen.gameObject.SetActive(false);
    }

    

        

}

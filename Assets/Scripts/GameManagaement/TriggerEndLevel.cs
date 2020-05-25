using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerEndLevel : MonoBehaviour {

    public bool isEnd = true;

    private Narrate.ProximityNarrationTrigger dialog;
    private float timeToFinish;


    void Start() {
        dialog = GetComponent<Narrate.ProximityNarrationTrigger>();
        GetComponent<SphereCollider>().radius = dialog.proximity;
        // Get the duration of the last narration of the game
        timeToFinish = dialog.theNarration.phrases[0].duration;
    }

    IEnumerator WaitForFinishingGame() {
        yield return new WaitForSeconds(timeToFinish + 2f);
        if (isEnd) {
            GameLoader.instance.LoadEndMenu(SceneManager.GetActiveScene().buildIndex);
        } else {
            GameLoader.instance.LoadNextScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void OnTriggerEnter(Collider other) {
        Debug.Log("Tag" + other.gameObject.tag);
        if (other.gameObject.CompareTag("Player")) {
            StartCoroutine(WaitForFinishingGame());
        }
    }

}

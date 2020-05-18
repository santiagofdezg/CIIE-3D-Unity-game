using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HitOverlay : MonoBehaviour {
    
    public float initAlpha = 0.65f;
    public float timeToRecover = 1f;
    bool status = false;

    // Start is called before the first frame update    
    Image hitOverlay;


    void Start() {
        hitOverlay =  gameObject.GetComponent<Image>();
    }

    IEnumerator FadeTo(float aValue, float aTime) {
        status = true;
        float alpha = initAlpha;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime) {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha,aValue,t));
            hitOverlay.color = newColor;
            yield return new WaitForFixedUpdate();
        }
        status = false;
    }

    public void flashScreen() {
        if(!status) {
            StartCoroutine(FadeTo(0.0f, timeToRecover));
        } else {
            StopCoroutine(FadeTo(0.0f, timeToRecover));
            StartCoroutine(FadeTo(0.0f, timeToRecover));   
        }
    }

}

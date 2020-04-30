using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class OptionsMenu : MonoBehaviour
{

    public AudioMixer audioMixer;
    public TMPro.TMP_Dropdown resolutionDropdown;
    //os obxecto de tipo Resolution teñen un width, un height e un refresh rate
    Resolution[] resolutions;

   

//recorremos o array e añadimolo a unha lista, ademais cambia o currentRest a resolucion actual da pantalla
    private List<string> resolutionToStringList(Resolution[] resolutions, int currentRes){
        List<string> aux = new List<string>();

        for(int i = 0; i<resolutions.Length; i++){
          //  aux.Add(resolutions[i].width + "x" + resolutions[i].height); //construimos string de tipo Width x Height de cada resolucion dispoñible no equipo
              aux.Add(resolutions[i].ToString());

            if(resolutions[i].Equals(Screen.currentResolution)){ //comproba si a resolucion do indice actual é igual a resolucion actual
                currentRes = i;
            }

        }
    
        return aux;
    }

    void Start(){
        //necesaria para cambiar a resolucion do xogo a resolucion actual
        int currentRes = 0;
        //Creamos un array cas resolucions dispoñibles no equipo que se ejecute
      resolutions = Screen.resolutions;
        //vaciamos o dropdown de resolucions
      resolutionDropdown.ClearOptions();



        //solo se poden añadir opcions no dropdown si lle pasamos unha lista de strings
        //Asi que creamos unha lista de strings a partir do array de resolucions
      List<string> options = resolutionToStringList(resolutions, currentRes);

        //añadimos as opcions
      resolutionDropdown.AddOptions(options);
        Debug.Log(currentRes);
        //Fijamos a resolucion da pantalla como resolucion predeterminada no dropdown
      resolutionDropdown.value = currentRes;

      resolutionDropdown.RefreshShownValue(); //actualizamos o dropdown

    }

//Cambia o volumen de master
    public void SetVolume(float volume){ 
        audioMixer.SetFloat("MasterVolume", volume);
    }

//Cambia a calidade
    public void SetQuality(int qualityIndex){
     
        QualitySettings.SetQualityLevel(qualityIndex);
    }

//fullscreen xd
    public void SetFullscreen (bool isFullscreen){
        Screen.fullScreen = isFullscreen;
    }

  //cambiamos a resolucion  
    public void SetResolution(int resolutionIndex){
        Resolution newRes = resolutions[resolutionIndex];
        //cambiamos a resolution en funcion do dropdown, e tendo en conta si tamos en fullscreen
        Screen.SetResolution(newRes.width, newRes.height, Screen.fullScreen);
    }

}

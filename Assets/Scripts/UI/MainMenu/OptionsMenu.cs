using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class OptionsMenu : MonoBehaviour
{

    public AudioMixer audioMixer;
    public TMPro.TMP_Dropdown resolutionDropdown;
    public TMPro.TMP_Dropdown qualityDropdown;
    public Slider volumeSlider;
    public Toggle isFullscreenToggle;
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

//Cargamos as configs iniciales
    void getResolutions(){
       int currentRes = 0;
      //Creamos un array cas resolucions dispoñibles no equipo que se ejecute
      resolutions = Screen.resolutions;
      //vaciamos o dropdown de resolucions para poñer as dipoñibles no equipo
      resolutionDropdown.ClearOptions();

        //solo se poden añadir opcions no dropdown si lle pasamos unha lista de strings
        //Asi que creamos unha lista de strings a partir do array de resolucions
      List<string> options = resolutionToStringList(resolutions, currentRes);
      
      //añadimos as opcions
      resolutionDropdown.AddOptions(options);
      Debug.Log(currentRes);
      //Fijamos a resolucion actual como predeterminada no dropdown
      resolutionDropdown.value = currentRes;

      resolutionDropdown.RefreshShownValue(); //actualizamos o dropdown

    }

    void loadPrefs(){
      int index;
      float value;
      bool isFullscreen;

      //Cargamos e actualizamos o volumen maestro
        value = PlayerPrefs.GetFloat("MasterVolume", volumeSlider.value);
        audioMixer.SetFloat("MasterVolume", value);
        Debug.Log(value);
        volumeSlider.value = value;

        
      //Cargamos e actualizamos a calidade
        index = PlayerPrefs.GetInt("Quality", qualityDropdown.value);
        QualitySettings.SetQualityLevel(index);
        qualityDropdown.value = index;

      //Cargamos e actualizamos fullscreen
        isFullscreen = (PlayerPrefs.GetInt("Fullscreen") != 0);
        Screen.fullScreen = isFullscreen;
        isFullscreenToggle.isOn = isFullscreen;

      //Cargamos e actualizamos resolucion
        index = PlayerPrefs.GetInt("ResIndex", resolutionDropdown.value);
        QualitySettings.SetQualityLevel(index);
        resolutionDropdown.value= index;


    }
//Awake se llama siempre aunque el objeto no este instanciado
    public OptionsMenu(){


    }

    void Awake(){
      getResolutions();
      loadPrefs();
      Debug.Log("hola");
    }

//Cambia o volumen de master
//PlayerPrefs usamolo para almacenar o estado da config e que sexa persistente
    public void SetVolume(float volume){ 
        audioMixer.SetFloat("MasterVolume", volume);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

//Cambia a calidade
    public void SetQuality(int qualityIndex){
     
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("Quality", qualityIndex);
    }

//fullscreen xd
    public void SetFullscreen (bool isFullscreen){
        Screen.fullScreen = isFullscreen;
        //Como PlayerPrefs non acepta bools, almacenamos 1 si true 0 si non
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0 );

    }

  //cambiamos a resolucion  
    public void SetResolution(int resolutionIndex){
        Resolution newRes = resolutions[resolutionIndex];
        //cambiamos a resolution en funcion do dropdown, e tendo en conta si tamos en fullscreen
        Screen.SetResolution(newRes.width, newRes.height, Screen.fullScreen);
        PlayerPrefs.SetInt("ResIndex", resolutionIndex);

    }

}

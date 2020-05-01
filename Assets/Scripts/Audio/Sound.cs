using UnityEngine;
using UnityEngine.Audio;


//Esta clase e unha representacion de Sound para gardar a Info
//Usada co AudioManager fai que sexa mais visible utilizar sons no xogo
//Sin ter que instancialo usando objetos e prefabs

//Solo garda os parametros que nos interesa modificar 


[System.Serializable] //para que aparezca no inspector de unity
public class Sound 
{
//Fonte de audio de cada clase Sound
    public AudioClip clip;

    public GameObject parent;

//Si fora necesario podense añadir mais parametros
    public string name;
    [Range(0f,1f)]
    public float volume =0;
    [Range(.3f,3f)]
    public float pitch = 1f;
   [Range(0f,1f)]
    public float spatialBlend = 0f;

    public bool loop;
    //parent object of the sound
    

    [HideInInspector] //ocultamolo no inspector para que non se poida modificar dese ahi
    //pero e publico para que se poida modificar via script
    public AudioSource source;

//esta funcion actualiza os parametros de objeto e crea un audio source
    public void updateVariables(GameObject g){

         if (parent == null){
            parent = g;
        }
        
        source = parent.AddComponent<AudioSource>();
        source.spatialBlend = this.spatialBlend;
        source.clip = this.clip;
        source.volume = this.volume;
        source.pitch = this.pitch;
       

    }


}

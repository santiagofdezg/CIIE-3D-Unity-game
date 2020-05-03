using UnityEngine;
using System;
using UnityEngine.Audio;

//AudioManager para que sexa mais facil usar audios no xogo

//Este xestor permite que cada clip agregado ao sound manager, poida ser iniciado e parado
//Mesmos son para diferentes obxectos obligan a aumentar o array de sounds

public class AudioManager : MonoBehaviour
{

//Audios que vai xestionar o manager

//Si o audio non ten un audio mixer, metemos por defecto o main
    public AudioMixerGroup MainMixer;
    
    public Sound[] sounds;

    //Singleton para limitar el numero de AudioManagers a 1
    public static AudioManager instance;



void createAudioSource(Sound s, GameObject parent){
    //A todos os audios no manager, añadimoslle un audio source no propio obxecto
    //A cada sound añadimoslle unha referencia a ese audio source
    
    //añadimoslle ao audiosource os parametros para poder modificalos dende o inspector
    //si o son ten obxecto parent añadimolo como child ali, si non como child do propio audiomanager
        
            if (parent == null){
                parent = gameObject;
            }
            
            if (s.outputMixer == null){
                s.outputMixer = MainMixer;

            }

        
            
            s.source = parent.AddComponent<AudioSource>();
            s.source.spatialBlend = s.spatialBlend;
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.outputAudioMixerGroup = s.outputMixer;

        

    

}      
         



//chamase antes do Start(), asegura que os Audios esten preparados
    void Awake() 
    {
        //Singleton
        if (instance == null)
            instance = this;
        else{
            Destroy(gameObject);
        }

        //evitar a volver crear o obxecto nos cambios de escena para que os sons se sigan reproducindo
        DontDestroyOnLoad(gameObject);

        //createAudioSource();
         
        
    }

    void Start(){

        

    }


    private Sound searchSound(string name){
        Sound s = Array.Find(sounds, sound => sound.name == name);
        return s;
    }

//reproducimos o sonido que queiramos
    public void Play (string name){
        //buscamos o audio no array
        Sound s = searchSound(name);

        if (s==null){
            Debug.LogWarning("Audio: "+name+" non existe!");
            return;
        }

        createAudioSource(s, gameObject);
        //reproducimolo
        s.source.Play();
    }

    public void Play(string name, GameObject gObj){
        Sound s = searchSound(name);

        if (s==null){
            Debug.LogWarning("Audio: "+name+" non existe!");
            return;
        }

        createAudioSource(s, gObj);
        //reproducimolo
        s.source.Play();
    }

    public void Play(string name, Vector3 position){
        GameObject soundObject = new GameObject("Sound");
        Sound s = searchSound(name);

        if (s==null){
            Debug.LogWarning("Audio: "+name+" non existe!");
            return;
        }

        soundObject.transform.position = position;
        createAudioSource(s,soundObject);
        //reproducimolo
        s.source.Play();
    }

    public void Stop (string name){
        Sound s = searchSound(name);

        if (s==null){
            Debug.LogWarning("Audio: "+name+" non existe!");
            return;
        }

        s.source.Stop();

    }

    public void UnPause(string name){
        Sound s = searchSound(name);

        if (s==null){
            Debug.LogWarning("Audio: "+name+" non existe!");
            return;
        }

        s.source.Pause();


    }

    public void Pause(string name){

        Sound s = searchSound(name);

        if (s==null){
            Debug.LogWarning("Audio: "+name+" non existe!");
            return;
        }

        s.source.UnPause();


    }

}

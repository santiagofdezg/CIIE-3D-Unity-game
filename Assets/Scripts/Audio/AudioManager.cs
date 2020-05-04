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
    //A todos os audios no manager, a?adimoslle un audio source no propio obxecto
    //A cada sound a?adimoslle unha referencia a ese audio source
    
    //a?adimoslle ao audiosource os parametros para poder modificalos dende o inspector
    //si o son ten obxecto parent a?adimolo como child ali, si non como child do propio audiomanager
        
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

//comprobamos si o audio e validor pa reproducir
    bool checkIfValid(Sound s, GameObject gObj){

        if (s==null){
            Debug.LogWarning("Audio: "+name+" non existe!");
            return false;
        }
        
        if (s.source == null)
            createAudioSource(s, gObj);

        return true;
    }

    bool checkIfValid(Sound s, Vector3 position){

        if (s==null){
            Debug.LogWarning("Audio: "+name+" non existe!");
            return false;
        }
        
        if (s.source == null){
            GameObject gObj = new GameObject("Sound");
            gObj.transform.position = position;
            createAudioSource(s, gObj);
        }
        return true;
    }


//reproducimos o sonido que queiramos

//Recomendase usar oneshot para sons que se van reproducir varias veces como pasos ou disparos
//Si hai outra chamada a oneshot o audio previo non se acaba de golpe, sin oneshot si
    public void Play (string name, bool oneShot){
        //buscamos o audio no array
        Sound s = searchSound(name);


        //creamos audiosource
        if( !(checkIfValid(s, gameObject)) )
            return;
        //reproducimolo
        if (oneShot){
            s.source.PlayOneShot(s.clip);
        } else {
            s.source.Play();
        }
        
    }

    public void Play(string name, GameObject gObj, bool oneShot){
        Sound s = searchSound(name);

        if( !(checkIfValid(s, gObj)) )
            return;

        //reproducimolo
        if (oneShot){
            s.source.PlayOneShot(s.clip);
        } else {
            s.source.Play();
        }
    }

    public void Play(string name, Vector3 position, bool oneShot){
        Sound s = searchSound(name);

        if( !(checkIfValid(s,position)) )
            return;
            
        //reproducimolo
        if (oneShot){
            s.source.PlayOneShot(s.clip);
        } else {
            s.source.Play();
        }
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

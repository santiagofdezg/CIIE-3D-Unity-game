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

void createAudioSource(){
    //A todos os audios no manager, añadimoslle un audio source no propio obxecto
    //A cada sound añadimoslle unha referencia a ese audio source
    foreach (Sound s in sounds){
        //añadimoslle ao audiosource os parametros para poder modificalos dende o inspector

    //si o son ten obxecto parent añadimolo como child ali, si non como child do propio audiomanager
       s.updateVariables(this);

        
    }

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

        createAudioSource();

        
    }

    void Start(){

        Play("Test");

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

using UnityEngine;
using System;
using UnityEngine.Audio;

//AudioManager para que sexa mais facil usar audios no xogo

public class AudioManager : MonoBehaviour
{

//Audios que vai xestionar o manager

    
    public Sound[] sounds;

    //Singleton para limitar el numero de AudioManagers a 1
    public static AudioManager instance;

void createAudioSource(){
    //A todos os audios no manager, añadimoslle un audio source no propio obxecto
    //A cada sound añadimoslle unha referencia a ese audio source
    foreach (Sound s in sounds){
        //añadimoslle ao audiosource os parametros para poder modificalos dende o inspector
        s.source = gameObject.AddComponent<AudioSource>();
        s.source.clip = s.clip;
        s.source.volume = s.volume;
        s.source.pitch = s.pitch;
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

        //evitar a volver crear o obxecto nos cambios de escena
        DontDestroyOnLoad(gameObject);

        createAudioSource();

        
    }

    void Start(){
        Play("Theme");
    }



//reproducimos o sonido que queiramos
    public void Play (string name){
        //buscamos o audio no array
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s==null){
            Debug.LogWarning("Audio: "+name+" non existe!");
            return;
        }

        //reproducimolo
        s.source.Play();
    }
}

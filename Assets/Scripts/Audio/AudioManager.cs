﻿using UnityEngine;
using System;
using UnityEngine.Audio;
using System.Collections.Generic;

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



void createAudioSource(Sound s, GameObject parent, int audioID){
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

            AudioSource aux = parent.AddComponent<AudioSource>();
            
            aux.spatialBlend = s.spatialBlend;
            aux.clip = s.clip;
            aux.volume = s.volume;
            aux.pitch = s.pitch;
            aux.outputAudioMixerGroup = s.outputMixer;
            aux.playOnAwake = s.playOnAwake;
            aux.loop = s.loop;

            //añadimolo ao diccionario de audios
            try {
                s.source.Add(audioID, aux);
            } catch {
                Debug.LogError("The audioID "+ audioID + " already exists!");
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

        //createAudioSource();
         
        
    }

    void Start(){

        

    }


    private Sound searchSound(string name){
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s==null)
             Debug.LogError("Audio: "+name+" non existe!");;
        return s;
    }

//comprobamos si o audio e validor pa reproducir
    bool checkIfValid(Sound s, GameObject gObj, int audioID){

        if (s==null){
            Debug.LogWarning("Son non válida!");
            return false;
        }
        
        if (s.source == null)
            s.source = new Dictionary<int, AudioSource>();

        if (!s.source.ContainsKey(audioID))
            createAudioSource(s, gObj, audioID);

        return true;
    }

    bool checkIfValid(Sound s, Vector3 position, int audioID){

        if (s==null){
            Debug.LogWarning("Audio: "+name+" non existe!");
            return false;
        }
        
        if (s.source == null)
            s.source = new Dictionary<int, AudioSource>();

        if (!s.source.ContainsKey(audioID)){
            GameObject gObj = new GameObject("Sound");
            gObj.transform.position = position;
            createAudioSource(s, gObj, audioID);
        }
        return true;
    }


//reproducimos o sonido que queiramos

//Recomendase usar oneshot para sons que se van reproducir varias veces como pasos ou disparos
//Si hai outra chamada a oneshot o audio previo non se acaba de golpe, sin oneshot si

    private AudioSource getSourceByID(Sound s, int audioID){

        AudioSource aux;
        if (s.source !=null && s.source.TryGetValue(audioID,out aux)){
            return aux;
        } else {
            return null;
        }  

    }

    public void Play (string name, bool oneShot, int audioID){
        //buscamos o audio no array
        Sound s = searchSound(name);


        //creamos audiosource
        if( !(checkIfValid(s, gameObject, audioID)) )
            return; //si non e valido, salimos
        
        AudioSource aux = getSourceByID(s, audioID);
        if (aux != null){
            //reproducimolo
            if (oneShot){
                aux.PlayOneShot(s.clip);
            } else {
                aux.Play();
            }
        } else {
            return;
        }    

    }



    public void Play(string name, GameObject gObj, bool oneShot, int audioID){
        Sound s = searchSound(name);

        if( !(checkIfValid(s, gObj, audioID)) )
            return;

        AudioSource aux = getSourceByID(s, audioID);
        if (aux != null){
            //reproducimolo
            if (oneShot){
                aux.PlayOneShot(s.clip);
               
            } else {
                aux.Play();
            }
        } else {
            return;
        }    
    }

    public void Play(string name, Vector3 position, bool oneShot, int audioID){
        Sound s = searchSound(name);

        if( !(checkIfValid(s,position,audioID)) )
            return;
            
       AudioSource aux = getSourceByID(s, audioID);
        if (aux != null){
            //reproducimolo
            if (oneShot){
                aux.PlayOneShot(s.clip);
            } else {
                aux.Play();
            }
        } else {
            return;
        }   

        Destroy(aux.transform.gameObject,s.clip.length); 
        s.source.Remove(audioID);
    }


//Os audios de OneShot non se poden parar
    public void Stop (string name, int audioID){
        Sound s = searchSound(name);

        if (s==null){
            Debug.LogWarning("Audio: "+name+" non existe!");
            return;
        }

        AudioSource aux = getSourceByID(s, audioID);
        if (aux != null){
            aux.Stop();
        } else {
            return;
        }

    }

    public void UnPause(string name, int audioID){
        Sound s = searchSound(name);

        if (s==null){
            Debug.LogWarning("Audio: "+name+" non existe!");
            return;
        }

        AudioSource aux = getSourceByID(s, audioID);
        if (aux != null){
            aux.UnPause();
        } else {
            return;
        }


    }

    public void Pause(string name, int audioID){

        Sound s = searchSound(name);
        if (s==null){
            Debug.LogWarning("Audio: "+name+" non existe!");
            return;
        }

        AudioSource aux = getSourceByID(s, audioID);
        if (aux != null){
            aux.Pause();
        } else {
            return;
        }


    }

}

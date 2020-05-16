using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Observer : MonoBehaviour
{
    public abstract void OnNotify(NotificationType notificationType);
}

public class Subject : MonoBehaviour
{
 
 private List<Observer> _observers = new List<Observer>();

 public void RegisterObserver(Observer observer){
     _observers.Add(observer);
 }

 public void Notify(NotificationType notificationType){
     foreach(var observer in _observers)
        observer.OnNotify(notificationType);
 }

}

public enum NotificationType
{
    Paused,
    UnPaused
}

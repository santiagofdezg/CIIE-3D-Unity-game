using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    
    public Slider slider;
    public Gradient gradient;
    //o fill da barra
    public Image fill;


    public void SetHealth(int health){
        slider.value = health;

        //cambiamos o fill en funcion do valor do slider entre 0,1
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void SetMaxHealth(int health){
        slider.maxValue = health;
        slider.value = health;
        
        //poñemos o gradiente a 1, pa ter toda a barra de cores
       fill.color = gradient.Evaluate(1f); //devolver o color nese valor
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    // Start is called before the first frame update
   
    public string name;

    [TextArea(1,10)]
    public string[] senteces;

    
}

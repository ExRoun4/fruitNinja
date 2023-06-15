using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public Transform[] rotatingTexts;

    void Update(){
        foreach(Transform rotText in rotatingTexts){
            rotText.Rotate(0, 0, -0.3f);
        }
    }
}

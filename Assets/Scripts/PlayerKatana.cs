using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKatana : MonoBehaviour
{
    ClassicGameManager CGM;
    void Start(){
        if(GameObject.Find("<GameManager>") != null){
            CGM = GameObject.Find("<GameManager>").GetComponent<ClassicGameManager>();
        }
    }

    void Update(){
        if(CGM != null){
            if(CGM.lives <= 0){
                return;
            }
        }
        if(Input.touchCount > 0){
            transform.GetChild(0).gameObject.SetActive(true);
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            transform.GetChild(0).position = pos;
        } else {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}

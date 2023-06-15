using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    void Update(){
        if(transform.position != new Vector3(0, 0, -10)){
            transform.position = Vector3.Lerp(transform.position, new Vector3(0, 0, -10), 3 * Time.deltaTime);
        }
    }

    public void shake(){
        StartCoroutine(Shake());
    }
    IEnumerator Shake(){
        for(int i = 0; i < 13; i++){
            transform.position = new Vector3(Random.Range(-1, 1) * 0.1f, Random.Range(-1, 1) * 0.1f, -10);
            yield return new WaitForSeconds(0.03f);
        }
        transform.position = new Vector3(0, 0, -10);
    }
}

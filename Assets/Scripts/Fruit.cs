using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    ClassicGameManager CGM;
    Rigidbody rb;
    AudioSource source;

    public GameObject[] slices;
    public GameObject burst;
    
    public bool bomb;

    public AudioClip cutSFX;

    void Start(){
        CGM = GameObject.Find("<GameManager>").GetComponent<ClassicGameManager>();
        source = GameObject.FindWithTag("MainCamera").GetComponent<AudioSource>();
        
        rb = GetComponent<Rigidbody>();
        
        rb.AddForce(transform.up * Random.Range(13, 16), ForceMode.Impulse);
        rb.AddForce(transform.right * Random.Range(-3, 3), ForceMode.Impulse);

        int rand = Random.Range(-3, 3);
        if(rand == 0){
            rb.AddTorque(transform.forward * 1, ForceMode.Impulse);
            rb.AddTorque(transform.right * 1, ForceMode.Impulse);
        } else {
            rb.AddTorque(transform.forward * rand, ForceMode.Impulse);
            rb.AddTorque(transform.right * rand, ForceMode.Impulse);
        }
    }

    void Update(){
        if(!bomb){
            if(slices[1].transform.GetChild(0).position.y < -13 || slices[1].transform.GetChild(1).position.y < -13){
                Destroy(gameObject);
            }

            if(transform.position.y < -8f){
                if(rb.isKinematic == false){
                    if(CGM.lives > 0){
                        CGM.lives -= 1;
                        Destroy(gameObject);
                    }
                }
            }
        } else {
            if(transform.position.y <= -9){
                Destroy(gameObject);
            }
        }
    }

    void OnDisable(){
        if(bomb){
            GetComponent<AudioSource>().Stop();
        }
    }

    void OnTriggerEnter(Collider col){
        if(col.gameObject.name == "*katana"){
            if(!bomb){
                if(slices[0].activeSelf){
                    rb.isKinematic = true;

                    slices[0].SetActive(false);
                    slices[1].SetActive(true);
                    Instantiate(burst, transform.position, Quaternion.identity);

                    slices[1].transform.GetChild(0).GetComponent<Rigidbody>().AddForce(transform.up * 3, ForceMode.Impulse);
                    slices[1].transform.GetChild(0).GetComponent<Rigidbody>().AddTorque(transform.right * 3, ForceMode.Impulse);
                    slices[1].transform.GetChild(1).GetComponent<Rigidbody>().AddForce(-transform.up * 2, ForceMode.Impulse);
                    slices[1].transform.GetChild(1).GetComponent<Rigidbody>().AddTorque(transform.right * 3, ForceMode.Impulse);

                    source.PlayOneShot(cutSFX, 1f);
                    
                    CGM.score += 1;
                }
            } else {
                GameObject.FindWithTag("MainCamera").GetComponent<CameraShake>().shake();
                Destroy(gameObject);
                Instantiate(burst, transform.position, Quaternion.identity);
                source.PlayOneShot(cutSFX, 1f);
                CGM.lives -= 1;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFruit : MonoBehaviour
{
    AudioSource source;

    public Animator anim;

    public GameObject[] slices;
    public GameObject burst;

    public AudioClip cutSFX;
    
    void Start(){
        source = GameObject.FindWithTag("MainCamera").GetComponent<AudioSource>();
    }

    void Update(){
        if(slices[0].activeSelf){
            transform.Rotate(0, 0.25f, 0.25f);
        }
    }

    void OnTriggerStay(Collider col){
        if(col.transform.parent.GetComponent<PlayerKatana>() != null){
            if(slices[0].activeSelf){
                slices[0].SetActive(false);
                slices[1].SetActive(true);
                
                Instantiate(burst, transform.position, Quaternion.identity);
                slices[1].transform.GetChild(0).GetComponent<Rigidbody>().AddForce(transform.up * 3, ForceMode.Impulse);
                slices[1].transform.GetChild(0).GetComponent<Rigidbody>().AddTorque(transform.right * 3, ForceMode.Impulse);
                slices[1].transform.GetChild(1).GetComponent<Rigidbody>().AddForce(-transform.up * 2, ForceMode.Impulse);
                slices[1].transform.GetChild(1).GetComponent<Rigidbody>().AddTorque(transform.right * 3, ForceMode.Impulse);

                source.PlayOneShot(cutSFX, 1f);

                anim.SetTrigger("out");
                transform.parent.GetComponent<Animator>().SetTrigger("out");
                transform.parent = null;
                StartCoroutine(play());
            }
        }
    }

    IEnumerator play(){
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(1);
    }

    public void Exit(){
        Application.Quit();
    }
}

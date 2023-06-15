using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ClassicGameManager : MonoBehaviour
{
    AudioSource source;

    [Header("Game")]
    public Animator anim;
    public GameObject[] fruitPrefabs;

    [Header("VISUAL")]
    public int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;
    public int lives = 3;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI endInfoText;

    public AudioClip throwSFX;

    void Start(){
        source = GameObject.FindWithTag("MainCamera").GetComponent<AudioSource>();

        StartCoroutine(spawnFruits());
        bestScoreText.text = "High score: "+PlayerPrefs.GetInt("HighScore", 0);
    }

    void Update(){
        //visual
        scoreText.text = ""+score;
        if(lives == 3){
            livesText.text = "<color=white>X X X";
        } else if(lives == 2){
            livesText.text = "<color=red>X <color=white>X X";
        } else if(lives == 1){
            livesText.text = "<color=red>X X <color=white>X";
        } else {
            if(livesText.text != "<color=red>X X X"){
                livesText.text = "<color=red>X X X";
                anim.SetTrigger("gameover");
                
                if(score > PlayerPrefs.GetInt("HighScore", 0)){
                    PlayerPrefs.SetInt("HighScore", score);
                    endInfoText.text = "NEW HIGH SCORE: "+score;
                } else {
                    endInfoText.text = "Score: "+score+"\nHigh score: "+PlayerPrefs.GetInt("HighScore", 0);
                }
            }
        }
    }

    IEnumerator spawnFruits(){
        yield return new WaitForSeconds(Random.Range(1, 3));
        if(lives > 0){
            int rand = 0;
            if(score < 50){
                rand = Random.Range(1, 3);
            } else if(score >= 50 && score < 85){
                rand = Random.Range(1, 4);
            } else if(score >= 85 && score < 120){
                rand = Random.Range(1, 5);
            } else if(score >= 120){
                rand = Random.Range(1, 6);
            }

            for(int i = 0; i < rand; i++){
                Instantiate(fruitPrefabs[Random.Range(0, fruitPrefabs.Length)], new Vector2(Random.Range(-5, 5), -7), Quaternion.identity);
                source.PlayOneShot(throwSFX, 1f);
                yield return new WaitForSeconds(0.55f);
            }
            StartCoroutine(spawnFruits());
        }
    }

    public void backToMenu(){
        StartCoroutine(exit());
    }

    IEnumerator exit(){
        anim.SetTrigger("out");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(0);
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public delegate void Powerup();

public class GameManager : MonoBehaviour{
    public Text pointsText;
    public Text timeText;
    public GameObject playBtn;
    public GenerateObjects ceiling;
    public Movement playerMvt;

    private int points;
    private int timeElapse;
    private bool freeze = false;
    
    private static Object _lock = new Object();
    private static GameManager _instance = null;
    public static GameManager instance {
        get {
            lock(_lock){
                if ( _instance == null ){
                    _instance = GameObject.Find("GameManager").GetComponent<GameManager>();
                }
            }
            return _instance;
        }
    }
    
    private Dictionary<string,Powerup> powerups = new Dictionary<string,Powerup>();
    private SoundManager sM;

    void Awake(){
        powerups.Add("Red",Speed);
        powerups.Add("Yellow",Normal);
        powerups.Add("Green",Slow);
        powerups.Add("Blue",Freeze);
        powerups.Add("Purple",Switch);

        sM = GetComponent<SoundManager>();
    }

    public void PlaySound(string n){
        sM.Play(n);
    }

    public void AddPoints(int x, string color){
        points += x;
        pointsText.text = "Score: " + points;

        if ( points <= 0 ){
            EndGame();
        }

        if ( color.Contains("Instance") ){
            color = color.Split(' ')[0];
        }

        if ( color != "" ) {
            playerMvt.SetColor(color);
            if ( powerups.ContainsKey(color) ) powerups[color]();
        }
    }

    public void StartGame(){
        points = 10;
        timeElapse = 0;
        StartCoroutine("TimeStart");
        ceiling.StartCoroutine("Generate");
        playBtn.SetActive(false);
        timeText.text = timeElapse + "s";
        pointsText.text = "Score: " + points;
    }

    public void EndGame(){
        StopCoroutine("TimeStart");
        ceiling.StopCoroutine("Generate");
        playBtn.SetActive(true);
        playerMvt.speed = 1f;
    }

    private void Slow(){
        playerMvt.speed /= 2f;
    }

    private void Speed(){
        playerMvt.speed *= 2f;
    }

    private void Freeze(){
        if ( !freeze ){
            freeze = true;
            StartCoroutine("Frozen");
        }
    }

    private void Switch(){
        lock(_lock){
            List<string> keys = new List<string>();
            List<Powerup> ups = new List<Powerup>();
            Dictionary<string,Powerup> temp = new Dictionary<string,Powerup>();

            foreach (KeyValuePair<string, Powerup> entry in powerups){
                keys.Add(entry.Key);
                ups.Add(entry.Value);
            }

            int count = keys.Count;
            for (int i = 0; i < count; i++){
                string k = keys[Random.Range(0,keys.Count)];
                Powerup p = ups[Random.Range(0,ups.Count)];
                temp.Add(k,p);

                keys.Remove(k);
                ups.Remove(p);
            }

            powerups = temp;
        }
    }

    private void Normal(){
        playerMvt.speed = 1f;
    }

    private IEnumerator TimeStart(){
        while(true){
            yield return new WaitForSeconds(1f);
            AddPoints(-1,"");
            timeElapse += 1;
            timeText.text = timeElapse + "s";
        }
    }

    private IEnumerator Frozen(){
        playerMvt.enabled = false;
        yield return new WaitForSeconds(3f);
        freeze = false;
        playerMvt.enabled = true;
    }
}

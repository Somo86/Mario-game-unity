using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamePlayManager : MonoBehaviour
{
    public MarioController marioController;
    public Text Score;
    public Text Coins;
    public Text Timer;
    public Text InfoText;
    
    //******* SOUNDS ******************//
    AudioClip clipDead;
    AudioClip winGame;
    private AudioSource mainAudioSource;
    private AudioSource effectsAudioSource;

    private bool clipDeadAlreadyPlayed = false;
    private bool gameEnabled = true;

    private AudioSource[] audios;
    private float timer = 0.0f;
    private int GameTries = 1;
    private int InitialTime = 350;
    private string StringFormat = "000000";

    void Awake() {
        audios = GetComponents<AudioSource>();
        mainAudioSource = audios[1];
        effectsAudioSource = audios[0];
        clipDead = Resources.Load<AudioClip>("Sounds/smb_mariodie");
        winGame = Resources.Load<AudioClip>("Sounds/smb_stage_clear");

        Score.text = CounterManager.getScore().ToString(StringFormat);
        Coins.text = CounterManager.getCoins().ToString();
        Timer.text = InitialTime.ToString(StringFormat);

        marioController.OnKilled += RestartLevel;
    }
    void Start()
    {
        InfoText.text = "GO MARIO!";
        StartCoroutine(CleanText(InfoText));
    }

    private void Update() {
        if(gameEnabled)
        {
            timer += Time.deltaTime;
            var seconds = (int)(timer % 60);
            var time = timeLeft(seconds);

            if(isRunOutTime(time))
            {
                RestartLevel();
            }
            else
            {
                Timer.text = time.ToString(StringFormat); 
                Score.text = CounterManager.getScore().ToString(StringFormat);
                Coins.text = CounterManager.getCoins().ToString();
            }
        }
    }

    public void RestartLevel()
    {
        InfoText.text = "GAME OVER MARIO!";
        marioController.allowMovement = false;
        gameEnabled = false;
        
        // to not play clip several times if some Toad kill Mario once he is dead
        if(!clipDeadAlreadyPlayed)
            effectsAudioSource.PlayOneShot(clipDead);
        clipDeadAlreadyPlayed = true;
        mainAudioSource.Stop(); //-> Stop main Music

        StartCoroutine(Restart());  
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(4);
        CounterManager.restartValues();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void EndGame()
    {
        InfoText.text = "THANKS MARIO!";
        marioController.allowMovement = false;
        gameEnabled = false;

        effectsAudioSource.PlayOneShot(winGame);
        mainAudioSource.Stop(); //-> Stop main Music
    }

    private int timeLeft(int seconds)
    {
        return InitialTime - seconds;
    }

    private bool isRunOutTime(int time)
    {
        return time <= 0;
    }

    IEnumerator CleanText(Text textBox)
    {
        yield return new WaitForSeconds(4);
        textBox.text = "";
    }

}

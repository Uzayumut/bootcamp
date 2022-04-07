using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class Button : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject joystick;
    public GameObject fireBtn;
    public GameObject pauseBtn;
    public GameObject nextLevel;
    public GameObject restartPanel;
    public int levelid;

    bool voiceState = false;
    public GameObject soundControl;
    // Start is called before the first frame update
    void Start()
    {
        levelid=SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Levelid" +levelid);
       // GameObject.DontDestroyOnLoad(this.gameObject);
        pausePanel.SetActive(false);
        if (PlayerPrefs.GetInt("voiceState") == 0)
        {
            //ses kapalı
            GetComponent<AudioSource>().Stop();
            soundControl.SetActive(true);
            voiceState = false;
        }
        else
        {
            //ses açık
            GetComponent<AudioSource>().Play();
            soundControl.SetActive(false);
            voiceState = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    public void Pause()
    {
        joystick.SetActive(false);
        fireBtn.SetActive(false);
        pauseBtn.SetActive(false);
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void Continue()
    {
        joystick.SetActive(true);
        fireBtn.SetActive(true);
        pauseBtn.SetActive(true);
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void VoiceSwitch()
    {
        if (voiceState == true)
        {
            //ses kapa
            GetComponent<AudioSource>().Stop();
            voiceState = false;
            soundControl.SetActive(true);
            PlayerPrefs.SetInt("voiceState", 0);
        }
        else
        {
            //ses aç
            GetComponent<AudioSource>().Play();
            voiceState = true;
            soundControl.SetActive(false);
            PlayerPrefs.SetInt("voiceState", 1);
        }
    }
    public void NextLevel()
    {
        
        nextLevel.SetActive(false);
        SceneManager.LoadScene(levelid+1);
    }
    public void Restart()
    {
        
        restartPanel.SetActive(false);

        SceneManager.LoadScene(levelid);
    }
}

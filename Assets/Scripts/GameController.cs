using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private GameObject crystal;
    private TMP_Text crystalText;
    
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        crystal = GameObject.Find("CrystalTMP");
        Debug.Log("oBJE ADI = " + crystal.name);
        crystalText=crystal.GetComponent<TextMeshPro>();
        crystalText.text = PlayerPrefs.GetInt("crystal").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        crystalText.text = PlayerPrefs.GetInt("crystal").ToString();
    }
}

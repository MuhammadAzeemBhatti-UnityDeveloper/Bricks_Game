using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using System.IO;
public class MainMenuScript : MonoBehaviour
{
    public string path;
    public static MainMenuScript manager;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI playerNameText;
    public string playerName;
    public HighScoreClass highscore;
    private void Awake()
    {
        if(manager==null)
        {
            manager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        path = Application.persistentDataPath + "/highscore.json";
        LocalLoad();
        highScoreText.text = "BestScore: " + highscore.playerName + " : " + highscore.score;
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void StartGame()
    {
        playerName = playerNameText.text;
        SceneManager.LoadScene("main");
    }
    public void GameOver(int score)
    {
        if(score>highscore.score)
        {
            highscore.score = score;
            highscore.playerName = playerName;
            LocalSave();
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void LocalSave()
    {
        string json = JsonUtility.ToJson(highscore);
        File.WriteAllText(path, json);
    }
    public void LocalLoad()
    {
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            highscore = JsonUtility.FromJson<HighScoreClass>(json);
        }
    }
}
[Serializable]
public class HighScoreClass
{
    public string playerName;
    public int score;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;
public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int difficulty;
    public int score;
    public int record;
    public TextMeshProUGUI DifficultyText;
    public TextMeshProUGUI RecordText;
    public TextMeshProUGUI ScoreText;
    private PlayerData playerData;
    private void Awake()
    {
        if (GameObject.Find("DifficultyManager") == null)
        {
            SceneManager.LoadScene(0);
        }

    }
    private void Start()
    {
        score = 0;
        difficulty = GameObject.Find("DifficultyManager").GetComponent<DifficultyManager>().difficulty;
        playerData = new PlayerData();
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            playerData = JsonUtility.FromJson<PlayerData>(json);
            if (difficulty == 1)
            {
                record = playerData.easyScore;
            }
            else if (difficulty == 2)
            {
                record = playerData.hardScore;
            }
        }
        else
        {
            playerData.easyScore = 0;
            playerData.hardScore = 0;
            string json = JsonUtility.ToJson(playerData);
            File.WriteAllText(path, json);
        }

        if (difficulty == 1)//easy
        {
            DifficultyText.text = "Difficulty:Easy";
        }
        else if (difficulty == 2)//hard
        {
            DifficultyText.text = "Difficulty:Hard";
        }
        ScoreText.text = "Score:" + score;
        RecordText.text = "Record:" + record;
    }
    public void GameOver()
    {

        SaveScore();
        Destroy(GameObject.Find("DifficultyManager"));
        SceneManager.LoadScene(0);
    }
    public void SaveScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (difficulty == 1)
        {
            playerData.easyScore = playerData.easyScore > score ?playerData.easyScore : score;
        }
        else if (difficulty == 2)
        {
            playerData.hardScore = playerData.hardScore > score? playerData.hardScore : score;
        }
        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText(path, json);
    }
    public void AddScore()
    {
        score++;
        ScoreText.text = "Score:" + score;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;
public class UserMenuManager : MonoBehaviour
{
    public GameObject HelpContainer;
    public GameObject UserMenuContainer;
    public GameObject DifficultyContainer;
    public GameObject DifficultyManager;
    public TextMeshProUGUI EasyRecordText;
    public TextMeshProUGUI HardRecordText;
    private PlayerData playerData;

    // Update is called once per frame
    private void Awake()
    {
        DontDestroyOnLoad(DifficultyManager);
        playerData = new PlayerData();
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            playerData = JsonUtility.FromJson<PlayerData>(json);
        }
        else
        {
            playerData.easyScore = 0;
            playerData.hardScore = 0;
            string json = JsonUtility.ToJson(playerData);
            File.WriteAllText(path, json);
        }
    }
    private void Start()
    {
        EasyRecordText.text = "Easy Record:" + playerData.easyScore;
        HardRecordText.text = "Hard Record:" + playerData.hardScore;
    }
    public void StartGame(int difficulty)
    {
        DifficultyManager.GetComponent<DifficultyManager>().SetDifficulty(difficulty);
        SceneManager.LoadScene("Prototype 4");
    }
    public void ChooseDifficulty()
    {
        UserMenuContainer.SetActive(false);
        DifficultyContainer.SetActive(true);
    }
    public void BackToUserMenu()
    {
        UserMenuContainer.SetActive(true);
        DifficultyContainer.SetActive(false);
    }
    public void ShowHelpText()
    {
        UserMenuContainer.SetActive(false);
        HelpContainer.SetActive(true);
    }
    public void CloseHelpText()
    {
        UserMenuContainer.SetActive(true);
        HelpContainer.SetActive(false);
    }
}

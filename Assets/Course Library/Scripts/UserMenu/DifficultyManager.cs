using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultyManager : MonoBehaviour
{
    public int difficulty;

    public void SetDifficulty(int difficulty)
    {
        this.difficulty = difficulty;
    }
}

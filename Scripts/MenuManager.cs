using System;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static Action<int> OnUpdateHighscore;


    private void Start()
    {
        int highscore = PlayerPrefs.GetInt("Highscore");
        OnUpdateHighscore?.Invoke(highscore);
    }

}

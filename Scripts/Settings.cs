using System;
using TMPro;
using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _soundOnOffText;
    private bool _isSoundOn;



    public static Action<bool> OnSound;
    public static Action<int> OnPlaySound;



    private void Start()
    {
        int sound = PlayerPrefs.GetInt("Sound", 1);
        if (sound == 1)
        {
            _isSoundOn = true;
            _soundOnOffText.text = "SOUND ON";
        }
        else if (sound == 0)
        {
            _isSoundOn = false;
            _soundOnOffText.text = "SOUND OFF";
        }

        OnSound?.Invoke(_isSoundOn);
    }


    public void SoundOnOff()
    {
        _isSoundOn = !_isSoundOn;
        OnSound?.Invoke(_isSoundOn);
        OnPlaySound?.Invoke(0);

        if (_isSoundOn)
        {
            PlayerPrefs.SetInt("Sound", 1);
            _soundOnOffText.text = "SOUND ON";
        }
        else
        {
            PlayerPrefs.SetInt("Sound", 0);
            _soundOnOffText.text = "SOUND OFF";
        }
    }
}

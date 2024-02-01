using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static List<AudioSource> _poolList = new List<AudioSource>();
    private static SoundManager _instance;
    [SerializeField] private AudioSource _audioPref;
    [SerializeField] private AudioClip[] _sounds;
    [SerializeField] private int _poolCount = 10;
    private bool _isSoundOn = true;



    private void OnEnable()
    {
        Settings.OnSound += SoundOnOff;
        Settings.OnPlaySound += PlaySound;
        SoundPlay.OnPlaySound += PlaySound;
        PlayerController.OnPlaySound += PlaySound;
    }

    private void OnDisable()
    {
        Settings.OnSound -= SoundOnOff;
        Settings.OnPlaySound -= PlaySound;
        SoundPlay.OnPlaySound -= PlaySound;
        PlayerController.OnPlaySound -= PlaySound;
    }



    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);

            for (int i = 0; i < _poolCount; i++)
                SpawnElement();
        }
        else Destroy(gameObject);
    }



    //создаем новый элемент пула
    private void SpawnElement()
    {
        AudioSource audio = Instantiate(_audioPref, transform.position, Quaternion.identity, transform);
        audio.gameObject.SetActive(false);
        DontDestroyOnLoad(audio.gameObject);
        _poolList.Add(audio);
    }

    private void PlaySound(int soundID)
    {
        if (!_isSoundOn) return;

        int id = GetFreeElementID();
        int iter = 100;

        //создаем новый элемент пула, если все занято
        while (id == -1 && iter > 0)
        {
            SpawnElement();
            id = GetFreeElementID();
            iter--;
        }

        if (iter == 0) Debug.LogError("Iter in " + gameObject.name + " equals zero!");

        _poolList[id].gameObject.SetActive(true);
        _poolList[id].clip = _sounds[soundID];
        _poolList[id].Play();
        StartCoroutine(DisactiveSound(_poolList[id].gameObject, _poolList[id].clip.length));
    }

    private IEnumerator DisactiveSound(GameObject sound, float delay)
    {
        yield return new WaitForSeconds(delay);
        sound.SetActive(false);
    }

    private void SoundOnOff(bool isSoundOn) =>
        _isSoundOn = isSoundOn;

    //поиск свободного элемента
    private int GetFreeElementID()
    {
        for (int i = 0; i < _poolList.Count; i++)
        {
            if (!_poolList[i].gameObject.activeSelf)
                return i;
        }
        return -1;
    }
}

using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private List<GameObject> _poolList = new List<GameObject>();
    [SerializeField] private Movement _obstaclePref;
    [SerializeField] private int _poolCount;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private int _randomY = 2;
    [SerializeField] private float _spawnDelay = 2;
    private float _timer;
    private bool _isStart;
    private bool _isPause;



    private void OnEnable()
    {
        GameManager.OnStartGame += StartGame;
        GameManager.OnPause += Pause;
    }

    private void OnDisable()
    {
        GameManager.OnStartGame -= StartGame;
        GameManager.OnPause -= Pause;
    }



    private void Awake()
    {
        for (int i = 0; i < _poolCount; i++)
            SpawnElement();

        _timer = _spawnDelay;
    }

    private void Update()
    {
        if (!_isStart || _isPause) return;

        if (_timer < _spawnDelay) _timer += Time.deltaTime;
        else
        {
            _timer = 0;
            Spawn();
        }
    }


    //создаем новый элемент пула
    private void SpawnElement()
    {
        int rY = Random.Range(-_randomY, _randomY);
        float y = _spawnPoint.position.y + rY;
        GameObject obst = Instantiate(_obstaclePref.gameObject, new Vector2(_spawnPoint.position.x, y), Quaternion.identity);
        obst.SetActive(false);
        _poolList.Add(obst);
    }

    private void StartGame() => _isStart = true;
    private void Pause(bool isPause) => _isPause = isPause;

    private void Spawn()
    {
        GameObject obst = GetFreeElement();
        int iter = 100;

        //создаем новый элемент пула, если все занято
        while (obst == null && iter > 0)
        {
            SpawnElement();
            obst = GetFreeElement();
            iter--;
        }

        if (iter == 0) Debug.LogError("Iter in " + gameObject.name + " equals zero!");

        obst.transform.position = _spawnPoint.position;
        int rY = Random.Range(-_randomY, _randomY);
        float y = _spawnPoint.position.y + rY;
        obst.transform.position = new Vector2(_spawnPoint.position.x, y);
        obst.SetActive(true);
    }

    //поиск свободного элемента
    private GameObject GetFreeElement()
    {
        for (int i = 0; i < _poolList.Count; i++)
        {
            if (!_poolList[i].activeSelf)
                return _poolList[i];
        }
        return null;
    }
}

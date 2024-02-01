using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private float _force;
    private bool _isGameOver;



    private void OnEnable()
    {
        InputManager.OnTouch += Jump;
        GameManager.OnPause += Pause;
        GameManager.OnStartGame += StartGame;
    }

    public void OnDisable()
    {
        InputManager.OnTouch -= Jump;
        GameManager.OnPause -= Pause;
        GameManager.OnStartGame -= StartGame;
    }


    public static Action OnAddScore;
    public static Action<bool> OnGameOver;
    public static Action<int> OnPlaySound;
    


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.bodyType = RigidbodyType2D.Static;
    }


    #region Void Actions
    private void Jump()
    {
        if (_rb.bodyType == RigidbodyType2D.Static || _isGameOver) return;
        _rb.velocity = Vector2.up * _force;
    }

    private void Pause(bool isPause)
    {
        if (_isGameOver) return;
        if (_rb == null) _rb = GetComponent<Rigidbody2D>();
        if(isPause) _rb.bodyType = RigidbodyType2D.Static;
        else _rb.bodyType = RigidbodyType2D.Dynamic;
    }

    private void StartGame()
    {
        if (_rb == null) _rb = GetComponent<Rigidbody2D>();
        _rb.bodyType = RigidbodyType2D.Dynamic;
    }
    #endregion


    #region Collisions and Triggers
    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (_isGameOver) return;
        OnAddScore?.Invoke();
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (_isGameOver) return;
        OnPlaySound?.Invoke(1);
        _isGameOver = true;
        OnGameOver?.Invoke(_isGameOver);
    }
    #endregion
}

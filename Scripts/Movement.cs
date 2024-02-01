using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    private bool _isCanMove = true;



    private void OnEnable() =>
         GameManager.OnPause += Pause;

    private void OnDisable() =>
        GameManager.OnPause -= Pause;



    private void FixedUpdate()
    {
        if (_isCanMove)
            transform.Translate(Vector2.left * _moveSpeed * Time.deltaTime);
    }



    private void Pause(bool isPause) =>
        _isCanMove = !isPause;


    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.name == "EndPoint")
            gameObject.SetActive(false);
    }
}

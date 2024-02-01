using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static Action OnTouch;


    public void Touch() =>
        OnTouch?.Invoke();
}

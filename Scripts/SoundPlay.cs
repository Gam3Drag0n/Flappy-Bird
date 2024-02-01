using System;
using UnityEngine;

public class SoundPlay : MonoBehaviour
{
    public static Action<int> OnPlaySound;


    public void PlaySound(int id)=>
        OnPlaySound?.Invoke(id);
}

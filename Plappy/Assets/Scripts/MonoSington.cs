using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSington<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T instance { get; private set; }
    private void Awake() => instance = FindObjectOfType(typeof(T)) as T;
}

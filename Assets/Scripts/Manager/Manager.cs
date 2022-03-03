using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T _instance { get; set; }
    private static object _lock = new object();

    /// <summary>
    /// Access singleton instance through this propriety.
    /// </summary>
    public static T instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (T)FindObjectOfType(typeof(T));

                if (_instance == null)
                {
                    Debug.Log("New " + typeof(T).ToString());
                    GameObject singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<T>();
                    singletonObject.name = typeof(T).ToString() + " (Singleton)";
                }
            }

            return _instance;
        }
    }

    private void OnDestroy()
    {
        if (_instance == this)
            return;
    }

    private void Awake()
    {
        _instance = null;
        xAwake();
    }

    protected virtual void xAwake() { }

    public virtual void Init() { }
}
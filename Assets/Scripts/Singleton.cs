
using UnityEngine;

// <T> can be any type
public class Singleton<T> : MonoBehaviour where T : Component
{
    // The instance is accessible only by the getter.
    private static T instance;
    public static bool isQuitting;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                // Make sure there's not other instances of the same type in memory
                instance = FindObjectOfType<T>();

                if (instance == null && !isQuitting)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).Name;
                    instance = obj.AddComponent<T>();
                }
            }
            return instance;
        }
    }

    // Virtual Awake() can be overridden in a derived class
    public virtual void Awake()
    {
        if (instance == null)
        {
            // If null, this instance is now the Singleton instance 
            instance = this as T;

            // Make sure instance will persist in memory across every scene.
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            // Destroy current instance because it must be a duplicate
            Destroy(gameObject);
        }
    }

    public virtual void OnDestroy()
    {
        isQuitting = true;
    }
}
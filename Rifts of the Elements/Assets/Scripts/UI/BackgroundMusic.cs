using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            // Ensure the GameObject is a root object before calling DontDestroyOnLoad
            if (transform.parent == null)
            {
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Debug.LogWarning("MusicManager is not a root GameObject. DontDestroyOnLoad will not work.");
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

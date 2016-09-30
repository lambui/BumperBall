using UnityEngine;

public class selfDestroy : MonoBehaviour
{
    public float liveDuration;

    void Update()
    {
        liveDuration -= Time.deltaTime;
        if(liveDuration <= 0f)
            Destroy(gameObject);
    }
}
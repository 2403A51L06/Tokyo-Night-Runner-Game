using UnityEngine;

public class Obstacle : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        RunnerController runner = other.GetComponent<RunnerController>();
        if (runner != null) runner.Die();
    }
}

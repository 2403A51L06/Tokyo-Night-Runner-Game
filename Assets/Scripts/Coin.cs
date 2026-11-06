using UnityEngine;

public class Coin : MonoBehaviour
{
    void Update() { transform.Rotate(0, 180f * Time.deltaTime, 0, Space.World); }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<RunnerController>() != null)
        {
            GameManager.Instance.AddCoin();
            Destroy(gameObject);
        }
    }
}

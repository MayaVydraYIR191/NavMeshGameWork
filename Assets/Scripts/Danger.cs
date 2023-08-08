using UnityEngine;
using Zenject;

public class Danger : MonoBehaviour
{
    private int dangerHealth;
    [SerializeField] private GameObject pac;
    void Start()
    {
        dangerHealth = 5;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            pac.SetActive(true);
            dangerHealth -= 1;
        }

        if (dangerHealth == 0)
        {
            gameObject.SetActive(false);
        }
    }
}

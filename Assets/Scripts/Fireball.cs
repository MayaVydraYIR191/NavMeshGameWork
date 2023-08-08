using UnityEngine;

public class Fireball : MonoBehaviour
{
    private static float speed = 10f;
    public float lifeTime = 2;

    void Update()
    {
        transform.Translate(Vector3.forward *speed*  Time.deltaTime);
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
        {
            Destroy(gameObject);
        }
    }

}

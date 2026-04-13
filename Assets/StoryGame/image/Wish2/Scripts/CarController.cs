using UnityEngine;

public class CarController : MonoBehaviour
{
    public float laneSpeed = 500f;
    public float xLimit = 300f;

    public DeliveryLevelManager levelManager;

    public void Move(float direction)
    {
        if (!AudioManager.instance.sfxSource.isPlaying)
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.carSound);
        }

        Vector3 pos = transform.localPosition;
        pos.x += direction * laneSpeed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, -xLimit, xLimit);
        transform.localPosition = pos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.carCrash);
            Debug.Log("Физическое столкновение с: " + collision.name);
            levelManager.GameOver(false);
        }
    }
}
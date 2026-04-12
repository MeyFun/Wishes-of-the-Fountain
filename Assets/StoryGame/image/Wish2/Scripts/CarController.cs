using UnityEngine;

public class CarController : MonoBehaviour
{
    public float laneSpeed = 500f; // Скорость перемещения
    public float xLimit = 300f;    // Ограничение по бокам (чтобы не уехать за экран)

    public DeliveryLevelManager levelManager;

    public void Move(float direction)
    {
        Vector3 pos = transform.localPosition;
        pos.x += direction * laneSpeed * Time.deltaTime;

        // Ограничиваем движение влево и вправо
        pos.x = Mathf.Clamp(pos.x, -xLimit, xLimit);
        transform.localPosition = pos;
    }

    // Обработка столкновения
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Проверяем, что врезались именно в препятствие
        if (collision.CompareTag("Obstacle"))
        {
            Debug.Log("Физическое столкновение с: " + collision.name);
            levelManager.GameOver(false);
        }
    }
}
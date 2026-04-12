using UnityEngine;
using UnityEngine.Playables;

public class SkipTimeline : MonoBehaviour
{
    private PlayableDirector director;

    void Awake()
    {
        director = GetComponent<PlayableDirector>();
    }

    void Update()
    {
        // Проверяем, играет ли таймлайн в данный момент
        // state == PlayState.Playing гарантирует, что мы не скипнем то, что не запущено
        if (director != null && director.state == PlayState.Playing)
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                SkipToEnd();
            }
        }
    }

    public void SkipToEnd()
    {
        if (director == null) return;

        // 1. Устанавливаем время на самый конец
        // Вычитаем крошечное значение (0.01), чтобы сработали последние события (Signals)
        director.time = director.duration - 0.01;

        // 2. Заставляем таймлайн мгновенно применить изменения
        director.Evaluate();

        // 3. Останавливаем, если он не остановился сам
        director.Stop();

        Debug.Log("Таймлайн перемотан до финала.");
    }
}
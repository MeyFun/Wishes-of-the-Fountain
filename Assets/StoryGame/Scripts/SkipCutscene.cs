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

        AudioManager.instance.PlaySFX(AudioManager.instance.buttonSound);
        director.time = director.duration - 0.01;
        director.Evaluate();

        director.Stop();

        Debug.Log("Таймлайн перемотан до финала.");
    }
}
using UnityEngine;

public partial class MusicTrigger : MonoBehaviour
{
    public enum MusicType { MainMenu, Animation, Selection, Wish1, Wish2, Wish3 }
    public MusicType typeToPlay;

    private void Start()
    {
        if (typeToPlay == MusicType.MainMenu) AudioManager.instance.PlayMusic(AudioManager.instance.mainMenuMusic);
    }

    private void OnEnable()
    {
        if (AudioManager.instance == null) return;

        switch (typeToPlay)
        {
            case MusicType.MainMenu:
                AudioManager.instance.PlayMusic(AudioManager.instance.mainMenuMusic);
                break;
            case MusicType.Animation:
                AudioManager.instance.PlayMusic(AudioManager.instance.animationMusic);
                break;
            case MusicType.Selection:
                AudioManager.instance.PlayMusic(AudioManager.instance.selectionMusic);
                break;
            case MusicType.Wish1:
                AudioManager.instance.PlayMusic(AudioManager.instance.wish1LevelMusic);
                break;
            case MusicType.Wish2:
                AudioManager.instance.PlayMusic(AudioManager.instance.wish2LevelMusic);
                break;
            case MusicType.Wish3:
                AudioManager.instance.PlayMusic(AudioManager.instance.wish3LevelMusic);
                break;
        }
    }
}
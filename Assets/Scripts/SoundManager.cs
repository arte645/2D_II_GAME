using UnityEngine;

public class SoundManager : Loader <SoundManager>
{
    [SerializeField]
    AudioClip lazer;
    [SerializeField]
    AudioClip fireball;
    [SerializeField]
    AudioClip error;
    [SerializeField]
    AudioClip death;
    [SerializeField]
    AudioClip click;
    [SerializeField]
    AudioClip afterYou;
    [SerializeField]
    AudioClip newgame;
    [SerializeField]
    AudioClip build;
    [SerializeField]
    AudioClip gameover;

    public AudioClip Lazer{
        get{ return lazer; }
    }
    public AudioClip Fireball{
        get{ return fireball; }
    }
    public AudioClip Error{
        get{ return Error; }
    }
    public AudioClip Death{
        get{ return death; }
    }
    public AudioClip Click{
        get{ return click; }
    }
    public AudioClip AfterYou{
        get{ return afterYou; }
    }
    public AudioClip Newgame{
        get{ return newgame; }
    }
    public AudioClip Build{
        get{ return build; }
    }
    public AudioClip GameOver{
        get{ return gameover; }
    }
}

using UnityEngine;

public class SoundManager : Loader<SoundManager>
{
    [SerializeField]
    private readonly AudioClip lazer;
    [SerializeField]
    private readonly AudioClip fireball;
    [SerializeField]
    private readonly AudioClip death;
    [SerializeField]
    private readonly AudioClip click;
    [SerializeField]
    private readonly AudioClip afterYou;
    [SerializeField]
    private readonly AudioClip newgame;
    [SerializeField]
    private readonly AudioClip build;
    [SerializeField]
    private readonly AudioClip gameover;

    public AudioClip Lazer => lazer;
    public AudioClip Fireball => fireball;
    public AudioClip Error => Error;
    public AudioClip Death => death;
    public AudioClip Click => click;
    public AudioClip AfterYou => afterYou;
    public AudioClip Newgame => newgame;
    public AudioClip Build => build;
    public AudioClip GameOver => gameover;
}

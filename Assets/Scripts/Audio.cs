using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName="New Audio", menuName= "AudioManager/Audio")]
public class Audio : ScriptableObject
{
    [SerializeField]
    private new string name;

    [SerializeField]
    private AudioClip clip;


    [SerializeField]
    [Range(0f, 1f)]
    private float volume;

    [SerializeField]
    [Range(0.5f, 1.5f)]
    private float pitch = 1f;

    [SerializeField]
    [Range(0, 0.5f)]
    private float randomVolume = .1f;

    [SerializeField]
    [Range(0, 0.5f)]
    private float randomPitch = .1f;

    [SerializeField]
    public AudioSource source;

    [SerializeField]
    private bool loop = false;

    [SerializeField]
    private bool playOnAwake;

    public void Play()
    {
        source.volume = volume * (1 + Random.Range(-randomVolume / 2f, randomVolume / 2f));
        source.pitch = pitch * (1 + Random.Range(-randomPitch / 2f, randomPitch / 2f));
        source.Play();
    }

    public void Play(AudioSource newSource) {
        newSource.volume = volume * (1 + Random.Range(-randomVolume / 2f, randomVolume / 2f));
        newSource.pitch = pitch * (1 + Random.Range(-randomPitch / 2f, randomPitch / 2f));
        
        newSource.Play();
    }
        
    public void PlayDelayed(float delay)
    {
        source.PlayDelayed(delay);
    }

    public void PlayOnce()
    {
        source.PlayOneShot(clip);
    }


    public void Pause()
    {
        source.Pause();
    }

    public void Resume()
    {
        source.UnPause();
    }

    public void Stop()
    {
        source.Stop();
    }



    public AudioClip audioClip
    {
        get => clip;
        set => clip = value;
    }

    public string audioName
    {
        get => name;
        set => base.name = value;
    }

    public bool audioLoop
    {
        get { return loop; }
        set { loop = value; }
    }


    public float audioVolume
    {
        get => volume;
        set => volume = value;
    }

    public bool PlayOnAwake
    {
        get => playOnAwake;
        set => playOnAwake = value;
    }

}
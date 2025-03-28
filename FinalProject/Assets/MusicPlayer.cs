using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    private List<AudioClip> musicTracks = new List<AudioClip>();
    private int currentTrackIndex = 0;
    private bool songHasEnded = false;

    private Dictionary<string, float> trackSpeeds = new Dictionary<string, float>();

    void Start()
    {
        AudioClip[] clips = Resources.LoadAll<AudioClip>("Audio/Music");
        if (clips.Length == 0)
        {
            Debug.LogError("No audio files found in Resources/Audio/Music!");
            return;
        }

        musicTracks.AddRange(clips);

        PlayRandom(); // Start with a random track
    }

    void Update()
    {
        if (!audioSource.isPlaying && audioSource.time == 0 && !songHasEnded)
        {
            songHasEnded = true;
            PlayNext(); // Play next track when current song ends
        }

        if (Input.GetKeyDown(KeyCode.N)) // Next Track
        {
            PlayNext();
        }
        else if (Input.GetKeyDown(KeyCode.P)) // Previous Track
        {
            PlayPrevious();
        }
        else if (Input.GetKeyDown(KeyCode.R)) // Random Track
        {
            PlayRandom();
        }
    }

    void PlayCurrentTrack()
    {
        if (musicTracks.Count > 0)
        {
            audioSource.clip = musicTracks[currentTrackIndex];
            audioSource.Play();
            songHasEnded = false;
        }
    }

    public void PlayNext()
    {
        currentTrackIndex = (currentTrackIndex + 1) % musicTracks.Count;
        PlayCurrentTrack();
    }

    public void PlayPrevious()
    {
        currentTrackIndex = (currentTrackIndex - 1 + musicTracks.Count) % musicTracks.Count;
        PlayCurrentTrack();
    }

    public void PlayRandom()
    {
        currentTrackIndex = Random.Range(0, musicTracks.Count);
        PlayCurrentTrack();
    }
}

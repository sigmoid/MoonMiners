using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerController : MonoBehaviour {

    public static bool exists;

    public AudioSource musicSource;
    public AudioSource[] sfxSources;
    public string[] clipNames;
    public AudioClip[] clips;

    private Dictionary<string, AudioClip> namesToClips;
    private LinkedList<int> audioSourceQueue;
    private Dictionary<string, int> audioClipNamesToSource;
    private Dictionary<int, string> audioSourceToClipNames;

    // Use this for initialization
    void Start () {
        if (!exists) {
            DontDestroyOnLoad(this.gameObject);
            exists = true;
            audioSourceQueue = new LinkedList<int>();
		    for (int i = 0; i < sfxSources.Length; i++) {
                audioSourceQueue.AddLast(i);
            }
            namesToClips = new Dictionary<string, AudioClip>();
            for (int i = 0; i < clipNames.Length; i++) {
                namesToClips.Add(clipNames[i], clips[i]);
            }
            audioClipNamesToSource = new Dictionary<string, int>();
            audioSourceToClipNames = new Dictionary<int, string>();
        } else {
            Destroy(this.gameObject);
        }
    }
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < sfxSources.Length; i++) {
            if (!sfxSources[i].isPlaying) {
                audioSourceQueue.Remove(i);
                audioSourceQueue.AddFirst(i);
            }
        }
    }

    public void PlayMusicClip(string clipName) {
        musicSource.clip = namesToClips[clipName];
        musicSource.loop = true;
        musicSource.Play();
    }

    public void StopMusicClip() {
        if (musicSource.isPlaying) {
            musicSource.Stop();
        }
    }

    public void PlaySFXClip(string clipName) {
        int nextSource = audioSourceQueue.First.Value;
        if (sfxSources[nextSource].isPlaying) {
            StopSFXClip(nextSource);
        }
        audioSourceQueue.RemoveFirst();
        audioClipNamesToSource.Add(clipName, nextSource);
        audioSourceToClipNames.Add(nextSource, clipName);
        sfxSources[nextSource].clip = namesToClips[clipName];
        sfxSources[nextSource].loop = false;
        sfxSources[nextSource].Play();
        audioSourceQueue.AddLast(nextSource);
    }

    public void StopSFXClip(string clipName) {
        int sourceId = audioClipNamesToSource[clipName];
        audioSourceToClipNames.Remove(sourceId);
        audioClipNamesToSource.Remove(name);
        audioSourceQueue.Remove(sourceId);
        audioSourceQueue.AddFirst(sourceId);
        sfxSources[sourceId].Stop();
    }

    public void StopSFXClip(int sourceId) {
        string name = audioSourceToClipNames[sourceId];
        audioSourceToClipNames.Remove(sourceId);
        audioClipNamesToSource.Remove(name);
        audioSourceQueue.Remove(sourceId);
        audioSourceQueue.AddFirst(sourceId);
        sfxSources[sourceId].Stop();
    }

}

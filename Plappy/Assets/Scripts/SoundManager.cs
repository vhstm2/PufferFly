using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Clip
{
    public string clipName;
    public AudioClip clip;
}

public class SoundManager : MonoBehaviour {
    
    [SerializeField] private AudioSource[] sources;
    [SerializeField] private Clip[] clips;

    private Dictionary<string , AudioClip> soundDIc =
    new Dictionary<string, AudioClip>();  

    private void Awake() 
    {
        foreach (var item in clips)
        {
            soundDIc.Add(item.clipName , item.clip);
        }   
    }

    public void SoundOneShot(string ClipName)
    {
        foreach (var item in sources)
        {
            if(item.isPlaying) continue;

            {
                // AudioClip clipTemp;
                // if(soundDIc.TryGetValue(clips[0].clipName, out clipTemp))
                //     item.PlayOneShot(clipTemp);
            }

            {
                //if(soundDIc.ContainsValue(soundDIc[ClipName])) item.PlayOneShot(soundDIc[ClipName]);
            }

            {
                if(soundDIc.ContainsKey(ClipName))
                {
                    item.PlayOneShot(soundDIc[ClipName]);
                }
            }
            return;
        }
    }

}

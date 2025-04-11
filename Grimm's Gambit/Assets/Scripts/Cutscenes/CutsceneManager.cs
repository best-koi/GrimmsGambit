using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField]
    protected VideoPlayer videoPlayer;//The cutscene VideoPlayer component

    [SerializeField]
    protected GameObject cutsceneUI;//The UI object that contains the cutscene and background panel

//Subscribes to an end event
    protected void Start(){
        videoPlayer.loopPointReached += OnLoopPointReached; 
    }



//Used to advance cutscene
    public virtual void SkipCutscene(){
        cutsceneUI.SetActive(false);
    }

//Hides UI when cutscene is over
    public virtual void OnLoopPointReached(VideoPlayer vp)
    {
        // Play the particle effect when the video reaches the end.  
        SkipCutscene();
    }
}

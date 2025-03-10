using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField]
    private VideoPlayer videoPlayer;//The cutscene VideoPlayer component

    [SerializeField]
    private GameObject cutsceneUI;//The UI object that contains the cutscene and background panel

//Subscribes to an end event
    private void Start(){
        videoPlayer.loopPointReached += OnLoopPointReached; 
    }



//Used to advance cutscene
    public void SkipCutscene(){
        cutsceneUI.SetActive(false);
    }

//Hides UI when cutscene is over
    void OnLoopPointReached(VideoPlayer vp)
    {
        // Play the particle effect when the video reaches the end.  
        SkipCutscene();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class BossCutsceneManager : CutsceneManager
{
    [SerializeField]
    private BossDialogueHandler dialogueHandler; 

    [SerializeField]
    private AudioSource audioPlayer;

     protected override void Start(){
        base.Start();
        audioPlayer.Stop();
    }

    public override void SkipCutscene(){
        cutsceneUI.SetActive(false);
        videoPlayer.Stop();
        audioPlayer.Play();
        
        dialogueHandler.StartDialogue(); 
    }

//Hides UI when cutscene is over
    public override void OnLoopPointReached(VideoPlayer vp)
    {
        // Play the particle effect when the video reaches the end.  
        SkipCutscene();
    }
}

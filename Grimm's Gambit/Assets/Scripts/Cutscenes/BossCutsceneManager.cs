using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class BossCutsceneManager : CutsceneManager
{
    [SerializeField]
    private BossDialogueHandler dialogueHandler; 
    public override void SkipCutscene(){
        cutsceneUI.SetActive(false);
        videoPlayer.Stop();
        dialogueHandler.StartDialogue(); 
    }

//Hides UI when cutscene is over
    public override void OnLoopPointReached(VideoPlayer vp)
    {
        // Play the particle effect when the video reaches the end.  
        SkipCutscene();
    }
}

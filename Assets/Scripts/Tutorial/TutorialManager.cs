using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TutorialManager :
MonoBehaviour
{
    public static TutorialManager
        Instance;
    public bool IsRunning
    {
        get;
        private set;
    }

    public TutorialStep
        CurrentStep
    {
        get;
        private set;
    }
    public VideoClip CurrentClip;
    public List<VideoClip> clips = new List<VideoClip>();
    const string TutorialKey =
        "TutorialCompleted";

    void Awake()
    {
        Instance = this;
    }

    public bool IsCompleted()
    {
        return
            PlayerPrefs.GetInt(
                TutorialKey,
                0) == 1;
    }
    public void LoadStep(
    TutorialStep step)
    {
        CurrentStep = step;
        if (step == TutorialStep.Finished)
        {
            TutorialUI.Instance.Hide();
            return;
        }
        IsRunning = true;
        ShowCurrentStep();
    }
    public void StartTutorial()
    {
        CurrentStep =
            TutorialStep.UseGenerator;
        IsRunning = true;

        ShowCurrentStep();
    }

    public void CompleteCurrentStep()
    {
        switch (CurrentStep)
        {
            case TutorialStep.UseGenerator:
                CurrentStep =
                    TutorialStep.PickupItem;
                CurrentClip = clips[0];
                break;

            case TutorialStep.PickupItem:
                CurrentStep =
                    TutorialStep.PlaceItem;
                CurrentClip = clips[1];
                break;

            case TutorialStep.PlaceItem:
                CurrentStep =
                    TutorialStep.MergeItems;
                CurrentClip = clips[2];
                break;

            case TutorialStep.MergeItems:
                CurrentStep =
                    TutorialStep.CompleteOrder;
                CurrentClip = clips[3];
                break;

            case TutorialStep.CompleteOrder:
                CurrentStep =
                    TutorialStep.EnterPortal;
                CurrentClip = clips[4];
                break;

            case TutorialStep.EnterPortal:
                CurrentStep =
                    TutorialStep.UnlockNode;
                CurrentClip = clips[5];
                break;

            case TutorialStep.UnlockNode:
                CurrentStep =
                    TutorialStep.ReturnToCore;
                CurrentClip = clips[6];
                break;

            case TutorialStep.ReturnToCore:
                FinishTutorial();
                return;
        }

        ShowCurrentStep();
    }

    void FinishTutorial()
    {
        CurrentStep =
            TutorialStep.Finished;
        CurrentClip = null;

        PlayerPrefs.SetInt(
            TutorialKey,
            1);

        PlayerPrefs.Save();
        IsRunning = false;

        TutorialUI.Instance.Hide();
    }

    void ShowCurrentStep()
    {
        TutorialUI.Instance.Show(
            CurrentStep);
    }
    public void Notify(
    TutorialStep action)
    {
        if (!IsRunning)
            return;

        if (action != CurrentStep)
            return;

        CompleteCurrentStep();
    }
}
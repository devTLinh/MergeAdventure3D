using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class TutorialUI :
MonoBehaviour
{
    public static TutorialUI
        Instance;

    [SerializeField]
    GameObject panel;

    [SerializeField]
    Text titleText;

    [SerializeField]
    Text descriptionText;
    [SerializeField] VideoPlayer player;

    void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        if(panel.activeSelf && Input.GetKeyDown(KeyCode.T))
        {
            Hide();
        }
    }
    public void Show(
        TutorialStep step)
    {
        panel.SetActive(true);

        switch (step)
        {
            case TutorialStep.UseGenerator:

                titleText.text =
                    "Generator";

                descriptionText.text =
                    "Click the generator to create new items.";

                break;

            case TutorialStep.PickupItem:

                titleText.text =
                    "Pickup";

                descriptionText.text =
                    "Left-click an item to pick it up.";

                break;

            case TutorialStep.PlaceItem:

                titleText.text =
                    "Place";

                descriptionText.text =
                    "Place the item into an empty slot.";

                break;

            case TutorialStep.MergeItems:

                titleText.text =
                    "Merge";

                descriptionText.text =
                    "Merge two identical items to create a higher-level item.";

                break;

            case TutorialStep.CompleteOrder:

                titleText.text =
                    "Order";

                descriptionText.text =
                    "Deliver required items to earn energy rewards.";

                break;

            case TutorialStep.EnterPortal:

                titleText.text =
                    "Portal";

                descriptionText.text =
                    "Use the portal to travel to the exploration area.";

                break;

            case TutorialStep.UnlockNode:

                titleText.text =
                    "Exploration";

                descriptionText.text =
                    "Spend exploration energy to unlock a new area.";

                break;

            case TutorialStep.ReturnToCore:

                titleText.text =
                    "Return";

                descriptionText.text =
                    "Press SPACE to return to the merge board.";

                break;
        }
        if(TutorialManager.Instance.CurrentClip != null) {
            PlayVideo(TutorialManager.Instance.CurrentClip);
        }
    }
    private void PlayVideo(VideoClip clip)
    {
        player.Stop();

        player.clip =  clip;

        player.Play();
    }

    public void Hide()
    {
        panel.SetActive(false);
    }
}
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.EventSystems;

public class Clickable : MonoBehaviour, IPointerClickHandler
{
    public MMF_Player clickFeedback;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!clickFeedback.HasFeedbackStillPlaying())
        {
            clickFeedback.PlayFeedbacks();
        }
    }
}
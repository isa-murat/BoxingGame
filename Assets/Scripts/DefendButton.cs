using UnityEngine;
using UnityEngine.EventSystems;

public class DefendButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public FighterController fighter;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (fighter != null)
        {
            fighter.StartDefend();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (fighter != null)
        {
            fighter.StopDefend();
        }
    }
}

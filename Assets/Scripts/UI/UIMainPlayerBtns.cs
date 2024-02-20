using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIMainPlayerBtns : MonoBehaviour
{
    private Player Player => GameSingleton.Instance.player;
    
    [SerializeField] private Button attackBtn;
    [SerializeField] private EventTrigger shieldEventTrigger;
    
    private void Start()
    {
        InitButtons();
    }

    private void InitButtons()
    {
        attackBtn.onClick.AddListener(Player.playerAttacker.PerformAttack);

        EventTrigger.Entry pointerDownEvent = new EventTrigger.Entry()
        {
            eventID = EventTriggerType.PointerDown
        };
        pointerDownEvent.callback.AddListener(Player.playerAttacker.StartBlock);
        shieldEventTrigger.triggers.Add(pointerDownEvent);

        EventTrigger.Entry pointerUpEvent = new EventTrigger.Entry()
        {
            eventID = EventTriggerType.PointerUp
        };
        pointerUpEvent.callback.AddListener(Player.playerAttacker.EndBlock);
        shieldEventTrigger.triggers.Add(pointerUpEvent);
    }

}

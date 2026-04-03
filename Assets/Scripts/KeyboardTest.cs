using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardTest : MonoBehaviour
{
    public FighterController player1;
    public FighterController player2;

    void Update()
    {
        Keyboard kb = Keyboard.current;
        if (kb == null) return;

        // Player 1: A = saldiri, D = defans
        if (kb.aKey.wasPressedThisFrame)
        {
            player1.Attack();
        }

        if (kb.dKey.wasPressedThisFrame)
        {
            player1.StartDefend();
        }
        if (kb.dKey.wasReleasedThisFrame)
        {
            player1.StopDefend();
        }

        // Player 2: Sol ok = saldiri, Sag ok = defans
        if (kb.leftArrowKey.wasPressedThisFrame)
        {
            player2.Attack();
        }

        if (kb.rightArrowKey.wasPressedThisFrame)
        {
            player2.StartDefend();
        }
        if (kb.rightArrowKey.wasReleasedThisFrame)
        {
            player2.StopDefend();
        }
    }
}

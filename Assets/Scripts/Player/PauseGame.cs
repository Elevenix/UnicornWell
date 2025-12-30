using UnityEngine;
using UnityEngine.InputSystem;

public class PauseGame : MonoBehaviour
{
    public void OnPause(InputAction.CallbackContext value)
    {
        if (value.phase == InputActionPhase.Started)
            LevelManager.Instance.SetPauseGame();
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

public class RestartGame : MonoBehaviour
{
    public void OnRestart(InputAction.CallbackContext value)
    {
        if (value.phase == InputActionPhase.Started)
            LevelManager.Instance.RestartCurrentLevel();
    }
}

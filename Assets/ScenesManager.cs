using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class ScenesManager : MonoBehaviour
{
    private void Awake()
    {

    }
    public void GetReloadInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ReloadScene();
        }
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}

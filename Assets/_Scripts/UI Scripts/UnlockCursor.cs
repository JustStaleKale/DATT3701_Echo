using UnityEngine;

public class UnlockCursor : MonoBehaviour
{

    private void Awake()
    {
        UnlockCursorFunc();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void UnlockCursorFunc()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}

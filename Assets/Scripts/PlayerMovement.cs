using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private PlayerControls playerControls;

    public void InitialSetup()
    {
        playerControls = new PlayerControls();
        playerControls.Gameplay.Enable();

        playerControls.Gameplay.Up.performed += ctx => Move();
        playerControls.Gameplay.UpperRight.performed += ctx => Move();
        playerControls.Gameplay.LowerRight.performed += ctx => Move();
        playerControls.Gameplay.Down.performed += ctx => Move();
        playerControls.Gameplay.UpperLeft.performed += ctx => Move();
        playerControls.Gameplay.LowerLeft.performed += ctx => Move();
    }

    private void Move()
    {
        Debug.Log("Move Player");
    }

    void OnDisable()
    {
        playerControls.Gameplay.Disable();

        playerControls.Gameplay.Up.performed -= ctx => Move();
        playerControls.Gameplay.UpperRight.performed -= ctx => Move();
        playerControls.Gameplay.LowerRight.performed -= ctx => Move();
        playerControls.Gameplay.Down.performed -= ctx => Move();
        playerControls.Gameplay.UpperLeft.performed -= ctx => Move();
        playerControls.Gameplay.LowerLeft.performed -= ctx => Move();
    }
}

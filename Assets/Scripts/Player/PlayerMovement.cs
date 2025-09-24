using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float moveTime = 0.3f;
    [SerializeField] private HexTileInfo currentHexTile;
    [SerializeField] private bool canMove = true;
    private bool firstMoveDone = false;
    public bool FirstMoveDone => firstMoveDone;

    private bool specialMoveActive = false;

    private PlayerControls playerControls;
    private HexGridManager hexGridManager;
    private HexColorManager hexColorManager;
    private Coroutine standingCoroutine;
    private Animator animator;
    private string currentAnimation = "";

    public void InitialSetup(HexGridManager hexGridManager, HexColorManager hexColorManager)
    {
        this.hexGridManager = hexGridManager;
        this.hexColorManager = hexColorManager;
        currentHexTile = hexGridManager.CurrentHexTile;
        playerControls = new PlayerControls();

        ChangeAnimationSpeed();

        ChangeAnimation("Idle");

        playerControls.Gameplay.Enable();

        playerControls.Gameplay.Up.performed += ctx => Move(new Vector2Int(0, 1));
        playerControls.Gameplay.UpperRight.performed += ctx => Move(new Vector2Int(1, 0));
        playerControls.Gameplay.LowerRight.performed += ctx => Move(new Vector2Int(1, -1));
        playerControls.Gameplay.Down.performed += ctx => Move(new Vector2Int(0, -1));
        playerControls.Gameplay.UpperLeft.performed += ctx => Move(new Vector2Int(-1, 0));
        playerControls.Gameplay.LowerLeft.performed += ctx => Move(new Vector2Int(-1, -1));
        playerControls.Gameplay.SpecialMove.performed += ctx => specialMoveActive = true;
    }

    public void ChangeAnimationSpeed()
    {
        animator = GetComponentInChildren<Animator>();
        
        AnimationClip clip = FindAnimationClip("PawnJump");
        if (clip != null) {
            float originalDuration = clip.length;
            float speedmultiplier = originalDuration / moveTime;
            animator.SetFloat("Speedmultiplier", speedmultiplier);
        } else {
            Debug.LogError("Jump animation clip not found!");
            // Set a default value if clip isn't found
            animator.SetFloat("Speedmultiplier", 1.0f);
        }
    }

    private void Move(Vector2Int direction)
    {
        if (!firstMoveDone) firstMoveDone = true;
        if (!canMove) return;

        canMove = false;
        if (specialMoveActive)
        {
            direction = SpecialMove(direction);
        }
        //even to odd Row Move
        else if (currentHexTile.hexCoordinates.y % 2 != 0 && direction.x != 0)
        {
            direction.y += 1;
        }

        //Move Player
        Vector3Int targetHexTile = currentHexTile.hexCoordinates + new Vector3Int(0, direction.x, direction.y);

        //Go to new Hex Grid
        if (targetHexTile.z > 9)
        {
            targetHexTile.z = targetHexTile.z - 10;
            targetHexTile.x += 1;
        }
        else if (targetHexTile.z < 0)
        {
            targetHexTile.z = targetHexTile.z + 10;
            targetHexTile.x -= 1;
        }


        HexTileInfo targetHex = hexGridManager.GetHexTileFromHexCoords(targetHexTile.x, targetHexTile.y, targetHexTile.z);
        if (targetHex == null)
        {
            canMove = true;
            return;
        }
        currentHexTile = targetHex;
        transform.LookAt(new Vector3(targetHex.worldPosition.x, transform.position.y, targetHex.worldPosition.z));
        StartCoroutine(MoveToHex(new Vector3(targetHex.worldPosition.x, transform.position.y, targetHex.worldPosition.z), moveTime));
        hexGridManager.CheckIfNewGridNeeded(targetHex);
        if (specialMoveActive) specialMoveActive = false;
        if (standingCoroutine != null)
        {
            StopCoroutine(standingCoroutine);
        }
        standingCoroutine = StartCoroutine(PlayerStandingOnHex());
    }

    private Vector2Int SpecialMove(Vector2Int direction)
    {
        
        if (direction == new Vector2Int(0, 1)) // Up
        {
            direction = new Vector2Int(0, 2);
        }
        else if (direction == new Vector2Int(1, 0)) // Upper Right
        {
            direction = new Vector2Int(2, 1);
        }
        else if (direction == new Vector2Int(1, -1)) // Lower Right
        {
            direction = new Vector2Int(2, -1);
        }
        else if (direction == new Vector2Int(0, -1)) // Down
        {
            direction = new Vector2Int(0, -2);
        }
        else if (direction == new Vector2Int(-1, 0)) // Upper Left
        {
            direction = new Vector2Int(-2, 1);
        }
        else if (direction == new Vector2Int(-1, -1)) // Lower Left
        {
            direction = new Vector2Int(-2, -1);
        }

        return direction;
    }

    public void ChangeAnimation(string newAnimation)
    {
        if (currentAnimation == newAnimation) return;
        if (newAnimation == "Idle") canMove = true;

        currentAnimation = newAnimation;
        animator.Play(newAnimation);

    }

    private void CheckAnimation()
    {

        if (canMove)
        {
            ChangeAnimation("Idle");
        }
    }

    private AnimationClip FindAnimationClip(string animationName)
    {
        
        foreach (AnimationClip  clip in animator.runtimeAnimatorController.animationClips)
        {
            Debug.Log("Clip found: " + clip.name);
            if (clip.name == animationName)
            {
                return clip;
            }
        }
        return null;
    }

    private IEnumerator PlayerStandingOnHex()
    {
        yield return new WaitForSeconds(3f);
        hexColorManager.PlayerStandingOnHexColor(currentHexTile);
        standingCoroutine = null;
    }


    private IEnumerator MoveToHex(Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = transform.position;
        float elapsed = 0f;

        ChangeAnimation("Jump");
        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        
    }

    void OnDisable()
    {
        playerControls.Gameplay.Disable();

        playerControls.Gameplay.Up.performed -= ctx => Move(new Vector2Int(0, 1));
        playerControls.Gameplay.UpperRight.performed -= ctx => Move(new Vector2Int(1, 0));
        playerControls.Gameplay.LowerRight.performed -= ctx => Move(new Vector2Int(1, -1));
        playerControls.Gameplay.Down.performed -= ctx => Move(new Vector2Int(0, -1));
        playerControls.Gameplay.UpperLeft.performed -= ctx => Move(new Vector2Int(-1, 0));
        playerControls.Gameplay.LowerLeft.performed -= ctx => Move(new Vector2Int(-1, -1));
        playerControls.Gameplay.SpecialMove.performed -= ctx => specialMoveActive = false;
    }

    private void ColorCheck()
    {
        HexTileInfo targetHex = currentHexTile;
        if (targetHex != null)
        {
            if (targetHex.hexTileRenderer.sharedMaterial.color.grayscale > 0.6f && !targetHex.hexTileGameObject.GetComponent<HexTile>().IsSafe)
            {
                Debug.Log("Player on White Hex - Game Over" + targetHex.hexTileRenderer.sharedMaterial.color.grayscale + " IsSafe: " + targetHex.hexTileGameObject.GetComponent<HexTile>().IsSafe);
                Destroy(gameObject);
            }
        }
    }

    void FixedUpdate()
    {
        CheckAnimation();

        if (canMove)
        {
            ColorCheck();
        }
    }
}

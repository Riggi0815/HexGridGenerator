using System;
using UnityEngine;

public class GameInitiator : MonoBehaviour
{
    [SerializeField] private HexGridManager hexGridManager;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private CameraFollow cameraFollow;
    private HexColorManager hexColorManager;
    private GameObject player;

    void Start()
    {
        Debug.Log("Initiate Game");
        InstantiateObjects();
        InitializeObjects();
        SetReferences();
        SpawnObjects();
    }

    private void InstantiateObjects()
    {
        Debug.Log("Instantiate GameObjects");
        hexGridManager = Instantiate(hexGridManager);
        playerManager = Instantiate(playerManager);
        cameraFollow = Instantiate(cameraFollow);
    }

    private void InitializeObjects()
    {
        Debug.Log("Initialize GameObjects");
        hexColorManager = hexGridManager.Initialize();
        cameraFollow.Initialize();
    }

    private void SetReferences()
    {
        Debug.Log("Function to Set needed References");
        playerManager.SetReferences(hexGridManager);
        
    }

    private void SpawnObjects()
    {
        Debug.Log("Spawn the GameObjects");
        hexGridManager.SpawnHexGrid();
        player = playerManager.SpawnPlayer();
        cameraFollow.SetReferences(player);
    }

    }

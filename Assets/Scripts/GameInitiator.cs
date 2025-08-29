using System;
using UnityEngine;

public class GameInitiator : MonoBehaviour
{
    [SerializeField] private HexGridManager hexGridManager;
    [SerializeField] private PlayerManager playerManager;
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
    }

    private void InitializeObjects()
    {
        Debug.Log("Initialize GameObjects");
        hexGridManager.Initialize();
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
        playerManager.SpawnPlayer();
    }

    }

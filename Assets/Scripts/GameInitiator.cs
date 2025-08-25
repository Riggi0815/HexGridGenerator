using System;
using UnityEngine;

public class GameInitiator : MonoBehaviour
{
    [SerializeField] private HexGridManager hexGridManager;
    [SerializeField] private PlayerManager playerManager;
    void Start()
    {
        InstantiateObjects();
        InitializeObjects();
        SpawnObjects();
    }

    private void InstantiateObjects()
    {
        hexGridManager = Instantiate(hexGridManager, gameObject.transform);
        playerManager = Instantiate(playerManager, gameObject.transform);
    }

    private void InitializeObjects()
    {
        hexGridManager.Initialize();
    }

    private void SpawnObjects()
    {
        hexGridManager.SpawnHexGrid();
        playerManager.SpawnPlayer();
    }

    }

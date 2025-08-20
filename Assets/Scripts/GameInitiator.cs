using System;
using UnityEngine;

public class GameInitiator : MonoBehaviour
{
    [SerializeField] private HexGridWorldGenerator hexGridWorldGenerator;
    void Start()
    {
        InstantiateObjects();
        SpawnObjects();
    }

    private void InstantiateObjects()
    {
        hexGridWorldGenerator = Instantiate(hexGridWorldGenerator);
    }

    private void SpawnObjects()
    {
        hexGridWorldGenerator.GenerateStartGrid();
    }

    }

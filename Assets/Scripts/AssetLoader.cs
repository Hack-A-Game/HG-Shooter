﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetLoader
{
    private static AssetLoader _instance;
    private Dictionary<string, GameObject> _prefabs = new Dictionary<string, GameObject>();

    public static AssetLoader get()
    {
        if (_instance == null)
        {
            _instance = new AssetLoader();
        }

        return _instance;
    }

    private AssetLoader()
    {
        GameObject[] prefabs = Resources.LoadAll<GameObject>("Prefabs/");
        Debug.Log(prefabs.Length);

        foreach (GameObject gob in prefabs)
        {
            _prefabs.Add(gob.name, gob);
        }
    }

    public GameObject instantiate(string name)
    {
        // name in _prefabs?
        GameObject gob = _prefabs[name];
        return GameObject.Instantiate<GameObject>(gob);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetLoader
{
    private static AssetLoader _instance;
    private Dictionary<string, GameObject> _prefabs = new Dictionary<string, GameObject>();
    private Dictionary<string, Sprite> _sprites = new Dictionary<string, Sprite>();

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
        foreach (GameObject gob in prefabs)
        {
            _prefabs.Add(gob.name, gob);
        }

        Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites/");
        foreach (Sprite sprite in sprites)
        {
            _sprites.Add(sprite.name, sprite);
        }
    }

    public void FillAll(string prefabName, int number)
    {
        float x = -25;
        float y = -25;
        float dx = _sprites[prefabName + "_0"].bounds.size.x;
        float dy = _sprites[prefabName + "_0"].bounds.size.y;

        for (; y < 25; y += dy)
        {
            for (x = -25; x < 25; x += dx)
            {
                Sprite sprite = _sprites[prefabName + "_" + number];
                GameObject go = new GameObject("Filler");
                SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
                renderer.sprite = sprite;
                go.transform.position = new Vector3(x, y, 2);
            }
        }
    }

    public void LoadMap(int[][] map, string prefabName, float initial_x = 0, float initial_y = 0)
    {
        float x = initial_x;
        float y = initial_y;
        float dx = _sprites[prefabName + "_0"].bounds.size.x;
        float dy = _sprites[prefabName + "_0"].bounds.size.y;

        for (int i = 0; i < map.Length; ++i)
        {
            for (int j = 0; j < map[i].Length; ++j)
            {
                Sprite sprite = _sprites[prefabName + "_" + map[i][j]];
                GameObject go = new GameObject("Test");
                SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
                renderer.sprite = sprite;
                go.transform.position = new Vector3(x, y, 1);

                if (map[i][j] == 6 || map[i][j] == 261 || map[i][j] == 53 || map[i][j] == 177)
                {
                    go.AddComponent<BoxCollider2D>();
                    BoxCollider2D collider = go.GetComponent<BoxCollider2D>();

                    if (map[i][j] == 6)
                    {
                        collider.offset = new Vector2(0, -0.2f);
                        collider.size = new Vector2(sprite.bounds.size.x, sprite.bounds.size.y / 4.0f);
                    }
                    else if (map[i][j] == 177)
                    {
                        collider.offset = new Vector2(0, 0.22f);
                        collider.size = new Vector2(sprite.bounds.size.x, sprite.bounds.size.y / 4.0f);
                    }
                    else if (map[i][j] == 261)
                    {
                        collider.offset = new Vector2(0.22f, 0);
                        collider.size = new Vector2(sprite.bounds.size.x / 4.0f, sprite.bounds.size.y);
                    }
                    else if (map[i][j] == 53)
                    {
                        collider.offset = new Vector2(-0.22f, 0);
                        collider.size = new Vector2(sprite.bounds.size.x / 4.0f, sprite.bounds.size.y);
                    }
                }

                x += dx;
            }

            y -= dy;
            x = initial_x;
        }
    }

    public GameObject instantiate(string name)
    {
        // name in _prefabs?
        GameObject gob = _prefabs[name];
        return GameObject.Instantiate<GameObject>(gob);
    }
}

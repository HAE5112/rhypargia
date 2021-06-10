using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string sceneName;
    public Transform player;
    public float size;
    private new SpriteRenderer renderer;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(((Vector2)(player.position - transform.position)).sqrMagnitude < size * size)
        {
            renderer.color = Color.green;
            if (Input.GetKeyDown(KeyCode.Return))
                SceneManager.LoadScene(sceneName);
        }
        else
        {
            renderer.color = Color.clear;
        }
    }
}

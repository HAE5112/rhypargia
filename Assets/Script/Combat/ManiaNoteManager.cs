using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManiaNoteManager : NoteManager
{
    public int line;

    public Sprite[] note;

    private SpriteRenderer spriteRenderer;

    public override void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = note[line > 0 && line < 3 ? 1 : 0];
    }

    // Update is called once per frame
    public override void Update()
    {
        if (duration > 0)
            transform.localScale = new Vector3(1, (float)duration * speed, 1);
        else
            transform.localScale = new Vector3(1, 0.25f, 1);
        transform.localPosition = new Vector3(line - 1.5f, (float)(time - AudioManager.AudioTime) * speed + transform.localScale.y / 2);
    }
}

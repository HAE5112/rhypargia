using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public enum NoteType
    {
        Osu, Mania
    }
    public NoteType noteType;
    public double time;
    public float osuX;
    public float osuY;
    public int manL;

    public static float speed = 5;

    private SpriteRenderer circleRenderer;
    private SpriteRenderer ringRenderer;
    private Transform ringTransform;

    // Start is called before the first frame update
    void Start()
    {
        if (noteType == NoteType.Osu)
        {
            circleRenderer = GetComponent<SpriteRenderer>();
            ringTransform = transform.GetChild(0);
            ringRenderer = ringTransform.GetComponent<SpriteRenderer>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(noteType == NoteType.Osu)
        {
            transform.localPosition = new Vector3(osuX, osuY, 0);
            if (time - 5 / speed > AudioManager.AudioTime)
            {
                circleRenderer.color = ringRenderer.color = new Color(1, 1, 1, 0);
            }
            else if (time > AudioManager.AudioTime)
            {
                circleRenderer.color = ringRenderer.color = new Color(1, 1, 1, 1 - (float)(time - AudioManager.AudioTime) * speed / 5);
                ringTransform.localScale = new Vector3((float)(time - AudioManager.AudioTime) * speed + 1, (float)(time - AudioManager.AudioTime) * speed + 1, 1);
            }
            else
            {
                circleRenderer.color = ringRenderer.color = new Color(1, 1, 1, 0);
            }
        }
        else if(noteType == NoteType.Mania)
        {
            transform.localPosition = new Vector3(manL - 2, (float)(time - AudioManager.AudioTime) * speed);
        }
    }
}

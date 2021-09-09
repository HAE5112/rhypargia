using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public double time;
    public double duration;

    public enum NoteType
    {
        Osu, Mania
    }
    public NoteType noteType;

    public static float speed = 5;

    // Osu
    public Vector2 osu_start;
    private Vector2 osu_end;
    public float osu_direction;
    public float osu_size;
    public int osu_index;
    public int osu_style;
    [System.Serializable]
    public struct osu_NoteStyle
    {
        public Sprite circle;
        public Sprite approach;
    }
    public osu_NoteStyle[] osu_noteStyles;
    public Sprite[] osu_numbers;
    private SpriteRenderer osu_startRenderer;
    private Transform osu_approachTransform;
    private SpriteRenderer osu_approachRenderer;
    private SpriteRenderer osu_indexRenderer;
    private Transform osu_endTransform;
    private SpriteRenderer osu_endRenderer;
    private Transform osu_lineTransform;
    private SpriteRenderer osu_lineRenderer;

    // Mania
    public int mania_line;

    public Sprite[] mania_noteSprites;

    private SpriteRenderer mania_spriteRenderer;

    public void Start()
    {
        if(noteType == NoteType.Osu)
        {
            osu_startRenderer = GetComponent<SpriteRenderer>();
            osu_startRenderer = GetComponent<SpriteRenderer>();
            osu_approachTransform = transform.Find("Approach");
            osu_approachRenderer = osu_approachTransform.GetComponent<SpriteRenderer>();
            osu_indexRenderer = transform.Find("Index").GetComponent<SpriteRenderer>();
            osu_endTransform = transform.Find("End");
            osu_endRenderer = osu_endTransform.GetComponent<SpriteRenderer>();
            osu_lineTransform = transform.Find("Line");
            osu_lineRenderer = osu_lineTransform.GetComponent<SpriteRenderer>();
            osu_end = osu_start + (float)duration * speed * new Vector2(Mathf.Cos(Mathf.Deg2Rad * osu_direction), Mathf.Sin(Mathf.Deg2Rad * osu_direction));
            transform.position = osu_start;
            transform.localScale = new Vector3(osu_size, osu_size, 1);
            osu_endTransform.position = (Vector3)osu_end + new Vector3(0, 0, 1);
            osu_lineTransform.position = (Vector3)(osu_start + osu_end) / 2 + new Vector3(0, 0, 2);
            osu_lineTransform.localRotation = Quaternion.Euler(0, 0, osu_direction);
            osu_lineRenderer.size = new Vector2((osu_end - osu_start).magnitude / osu_size, 1);
            osu_startRenderer.sprite = osu_endRenderer.sprite = osu_noteStyles[osu_style].circle;
            osu_approachRenderer.sprite = osu_noteStyles[osu_style].approach;
            osu_indexRenderer.sprite = osu_numbers[osu_index];
        }
        else if(noteType == NoteType.Mania)
        {
            mania_spriteRenderer = GetComponent<SpriteRenderer>();
            mania_spriteRenderer.sprite = mania_noteSprites[mania_line > 0 && mania_line < 3 ? 1 : 0];
        }
    }

    public void Update()
    {
        if(noteType==NoteType.Osu)
        {
            if (time - 5 / speed > AudioManager.AudioTime)
            {
                osu_startRenderer.color = osu_endRenderer.color = osu_approachRenderer.color = osu_indexRenderer.color = osu_lineRenderer.color = new Color(1, 1, 1, 0);
            }
            else if (time > AudioManager.AudioTime)
            {
                osu_startRenderer.color = osu_endRenderer.color = osu_approachRenderer.color = osu_indexRenderer.color = osu_lineRenderer.color = new Color(1, 1, 1, 1 - (float)(time - AudioManager.AudioTime) * speed / 5);
                osu_approachTransform.localScale = new Vector3((float)(time - AudioManager.AudioTime) * speed + 1, (float)(time - AudioManager.AudioTime) * speed + 1, 1);
            }
            else if (time + duration > AudioManager.AudioTime)
            {
                osu_approachTransform.position = Vector3.Lerp((Vector3)osu_start+Vector3.forward, (Vector3)osu_end + Vector3.forward, (float)((AudioManager.AudioTime - time) / duration));
            }
            else
            {
                osu_startRenderer.color = osu_endRenderer.color = osu_approachRenderer.color = osu_indexRenderer.color = osu_lineRenderer.color = new Color(1, 1, 1, 0);
            }
        }
        else if(noteType == NoteType.Mania)
        {
            if (duration > 0)
                transform.localScale = new Vector3(1, (float)duration * speed, 1);
            else
                transform.localScale = new Vector3(1, 0.25f, 1);
            transform.localPosition = new Vector3(mania_line - 1.5f, (float)(time - AudioManager.AudioTime) * speed + transform.localScale.y / 2);
        }
    }
}

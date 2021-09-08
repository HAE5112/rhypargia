using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OsuNoteManager : NoteManager
{
    public Vector2 start;
    private Vector2 end;
    public float direction;
    public float size;
    public int index;
    public int style;

    [System.Serializable]
    public struct NoteStyle
    {
        public Sprite circle;
        public Sprite approach;
    }

    public NoteStyle[] noteStyles;
    public Sprite[] numbers;

    private SpriteRenderer startRenderer;
    private Transform approachTransform;
    private SpriteRenderer approachRenderer;
    private SpriteRenderer indexRenderer;
    private Transform endTransform;
    private SpriteRenderer endRenderer;
    private Transform lineTransform;
    private SpriteRenderer lineRenderer;

    // Start is called before the first frame update
    public override void Start()
    {
        startRenderer = GetComponent<SpriteRenderer>();
        startRenderer = GetComponent<SpriteRenderer>();
        approachTransform = transform.Find("Approach");
        approachRenderer = approachTransform.GetComponent<SpriteRenderer>();
        indexRenderer = transform.Find("Index").GetComponent<SpriteRenderer>();
        endTransform = transform.Find("End");
        endRenderer = endTransform.GetComponent<SpriteRenderer>();
        lineTransform = transform.Find("Line");
        lineRenderer = lineTransform.GetComponent<SpriteRenderer>();
        end = start + (float)duration * speed * new Vector2(Mathf.Cos(Mathf.Deg2Rad * direction), Mathf.Sin(Mathf.Deg2Rad * direction));
        transform.position = start;
        transform.localScale = new Vector3(size, size, 1);
        endTransform.position = (Vector3)end + new Vector3(0, 0, 1);
        lineTransform.position = (Vector3)(start + end) / 2 + new Vector3(0, 0, 2);
        lineTransform.localRotation = Quaternion.Euler(0, 0, direction);
        lineRenderer.size = new Vector2((end - start).magnitude / size, 1);
        startRenderer.sprite = endRenderer.sprite = noteStyles[style].circle;
        approachRenderer.sprite = noteStyles[style].approach;
        indexRenderer.sprite = numbers[index];
    }

    // Update is called once per frame
    public override void Update()
    {
        if (time - 5 / speed > AudioManager.AudioTime)
        {
            startRenderer.color = endRenderer.color = approachRenderer.color = indexRenderer.color = lineRenderer.color = new Color(1, 1, 1, 0);
        }
        else if (time > AudioManager.AudioTime)
        {
            startRenderer.color = endRenderer.color = approachRenderer.color = indexRenderer.color = lineRenderer.color = new Color(1, 1, 1, 1 - (float)(time - AudioManager.AudioTime) * speed / 5);
            approachTransform.localScale = new Vector3((float)(time - AudioManager.AudioTime) * speed + 1, (float)(time - AudioManager.AudioTime) * speed + 1, 1);
        }
        else if(time + duration > AudioManager.AudioTime)
        {
            approachTransform.position = Vector3.Lerp(Vector3.forward, (Vector3)(end - start) + Vector3.forward, (float)((AudioManager.AudioTime - time) / duration));
        }
        else
        {
            startRenderer.color = endRenderer.color = approachRenderer.color = indexRenderer.color = lineRenderer.color = new Color(1, 1, 1, 0);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;



public class BtnType : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public ButtonType currentType;
    public Transform buttonScale;
    private Vector3 defaultScale;
    
    public CanvasGroup mainGroup;
    public CanvasGroup OptionGroup;

    [SerializeField]
    private TextMeshProUGUI textVariable = null;


    private void Awake()
    {
        textVariable = GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        defaultScale = buttonScale.localScale;
    }
    public bool isSound=true;
    public void OnBtnClick()
    {
        switch (currentType)
        {
            case ButtonType.New:
                Debug.Log("새게임");
                break;
            case ButtonType.Continue:
                Debug.Log("이어하기");
                break;
            case ButtonType.Option:
                Debug.Log("옵션");
                CanvasGroupOn(OptionGroup);
                CanvasGroupOff(mainGroup);
                break;
            case ButtonType.Sound: 
                if(isSound==true)
                {
                    Debug.Log("Sound On");
                   
                }
                else
                {
                    Debug.Log("Sound Off");
                    
                }
                isSound = !isSound;
                break;
            case ButtonType.Back:
                Debug.Log("뒤로가기");
                CanvasGroupOn(mainGroup);
                CanvasGroupOff(OptionGroup);
                break;
            case ButtonType.Quit:
                Debug.Log("게임종료");
                Application.Quit();
                break;

        }
    }
    private void CanvasGroupOn(CanvasGroup cg)
    {
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }
    private void CanvasGroupOff(CanvasGroup cg)
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale * 1.2f;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale;
    }
}

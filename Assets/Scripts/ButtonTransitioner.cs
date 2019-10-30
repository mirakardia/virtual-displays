using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using UnityEngine;

[RequireComponent(typeof(Button))]
public class ButtonTransitioner : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public Color32 m_NormalColor = Color.white;
    public Color32 m_HoverColor = Color.grey;
    public Color32 m_DownlColor = Color.white;
    public QuestionnaireScript questionnaireScript;
    private Image m_Image = null;

    private void Awake()
    {
        m_Image = GetComponent<Image>();
    }
    public void OnPointerEnter(PointerEventData eventData) {
        print("Enter");
     
        m_Image.color = m_HoverColor;
    }
    public void OnPointerExit(PointerEventData eventData) {
        m_Image.color = m_NormalColor;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
     
        m_Image.color = m_DownlColor;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
      ///  m_Image.color = m_NormalColor;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        //GetComponent<Button>().onClick.Invoke();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonInicio : MonoBehaviour
{
    public Sprite normalSprite;
    public Sprite hoverSprite;

    private Image buttonImage;

    void Start()
    {
        buttonImage = GetComponent<Image>();
        buttonImage.sprite = normalSprite;
    }

    public void OnPointerEnter()
    {
        // Cambiar el sprite cuando el mouse entra
        buttonImage.sprite = hoverSprite;
    }

    public void OnPointerExit()
    {
        // Restaurar el sprite normal cuando el mouse sale
        buttonImage.sprite = normalSprite;
    }

    public void OnButtonClick()
    {
        // Acciones a realizar cuando se hace clic en el botón
        Debug.Log("Botón clickeado");
        // Aquí puedes agregar código adicional según tus necesidades
    }
}

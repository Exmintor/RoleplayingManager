using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityDesignerItem : MonoBehaviour
{
    private AbilityDesigner designer;

    public Ability AbilityRepresented { get; set; }

	// Use this for initialization
	void Start ()
    {
        designer = FindObjectOfType<AbilityDesigner>();
	}

    public void AbilityWasClicked()
    {
        designer.UpdateDesignerSelected(this, this.AbilityRepresented);
    }

    public void RefreshImage()
    {
        if(AbilityRepresented.AbilityImage != null)
        {
            Texture2D image = AbilityRepresented.AbilityImage;
            this.GetComponent<Image>().sprite = Sprite.Create(image, new Rect(0, 0, image.width, image.height), new Vector2(0.5f, 0.5f));
        }
        else
        {
            this.GetComponent<Image>().sprite = null;
        }
    }

    public void RefreshName()
    {
        string text = AbilityRepresented.Name;
        this.GetComponentInChildren<Text>().text = text;
    }
}

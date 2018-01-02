using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This should be attached to a Prefab.
/// The prefab represents an Ability and provides UI information about it.
/// </summary>
public class AbilityDesignerItem : MonoBehaviour
{
    // The Ability Viewer in this scene.
    // Ideally, there should only ever be one AbilityViewer in a scene.
    private AbilityViewer viewer;

    // A reference to the Ability that this Prefab points to.
    public Ability AbilityRepresented { get; set; }

	// Use this for initialization
	void Start ()
    {
        // Grab a reference to the Ability Viewer in the scene
        viewer = FindObjectOfType<AbilityViewer>();
	}

    /// <summary>
    /// This method should be called when the Ability is clicked.
    /// </summary>
    public virtual void AbilityWasClicked()
    {
        // Send a notification to the Ability Viewer
        viewer.UpdateDesignerSelected(this);
    }

    /// <summary>
    /// Refreshes the Image of the Prefab based on which image the Represented Ability points to.
    /// </summary>
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

    /// <summary>
    /// Refreshes the Text of the Prefab based on which Name the Represented Ability has.
    /// </summary>
    public void RefreshName()
    {
        string text = AbilityRepresented.Name;
        this.GetComponentInChildren<Text>().text = text;
    }
}

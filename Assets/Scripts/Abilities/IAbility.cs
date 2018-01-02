using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This interface defines the common functionalities shared by every Ability
/// </summary>
public interface IAbility
{
    // Every Ability should ideally have a unique ID
    int ID { get; }

    // Ideally no two abilities have the same name.
    string Name { get; }

    // The image that represents the Ability. A JPG or a PNG
    Texture2D AbilityImage { get; }

    // The text describing how the Ability functions
    string Description { get; }

    // Abilities have a cooldown time. This can be 0 if needed.
    float Cooldown { get; }

    // Every Ability should define their own Cast function to reflect their Description text.
    void Cast();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IAbility
{
    int ID { get; }

    string Name { get; }

    Texture2D AbilityImage { get; }

    string Description { get; }

    float Cooldown { get; }

    void Cast();
}

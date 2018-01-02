using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// An abstract base class that implements a more specific interface than IAbility.
/// It retains all the functionality of Ability, and adds more specific functionality.
/// </summary>
public abstract class ChanneledAbility : Ability, IChanneledAbility
{
    public float ChannelTime { get; protected set; }

    /// <summary>
    /// Every Ability should define their own Cast() method to match their Description text.
    /// </summary>
    public override abstract void Cast();
}

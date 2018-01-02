using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/// <summary>
/// A Generic Ability. It doesn't define any Cast() method.
/// Ultimately, no Ability should be a Generic Ability.
/// If an Ability is a GenericAbility, that means it
/// has not been implemented yet.
/// </summary>
public class GenericAbility : Ability
{
    public GenericAbility()
    {
        // Default values.
        // Can be overridden in Json after it has been saved,
        // or by using the Ability Designer.
        ID = 1;
        Name = "GenericAbility";
        ImageFilePath = "GenericAbility";
        Description = "This is a generic ability";
        Cooldown = 0.0f;
    }
	
    /// <summary>
    /// Cast() is not implemented for this Ability.
    /// </summary>
    public override void Cast()
    {
        throw new NotImplementedException();
    }
}

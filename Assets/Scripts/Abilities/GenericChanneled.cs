using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/// <summary>
/// A Generic Channeled Ability. It doesn't define any Cast() method.
/// TODO: Get rid of this class. It only serves as an example of how
/// the chain of inheritance works.
/// </summary>
public class GenericChanneled : ChanneledAbility
{
    public GenericChanneled()
    {
        // Default values.
        // Can be overridden in Json after it has been saved,
        // or by using the Ability Designer.
        ID = 100;
        Name = "GenericChanneled";
        ImageFilePath = "GenericChanneled";
        Description = "This is a generic channeled ability";
        Cooldown = 0.0f;
        ChannelTime = 0.0f;
    }

    public override void Cast()
    {
        throw new NotImplementedException();
    }
}

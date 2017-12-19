using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GenericChanneled : ChanneledAbility
{
    public GenericChanneled()
    {
        ID = 2;
        Name = "Test2";
        ImageFilePath = "Test2";
        Description = "This is a second ability";
        Cooldown = 2.0f;
        ChannelTime = 1.0f;
    }

    public override void Cast()
    {
        throw new NotImplementedException();
    }
}

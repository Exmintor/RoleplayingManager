using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GenericAbility : Ability
{
    public GenericAbility()
    {
        ID = 1;
        Name = "GenericAbility";
        ImageFilePath = "GenericAbility";
        Description = "This is a generic ability";
        Cooldown = 0.0f;
    }
	
    public override void Cast()
    {
        throw new NotImplementedException();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectile
{
    public Weapon.User user {get; set;}
    public int damage {get; set;}
}
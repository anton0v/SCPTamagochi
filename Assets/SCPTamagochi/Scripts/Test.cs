using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Test
{
    public string Name { get; private set; }
    public List<int> TagIdList { get; private set; }

    public Test(string name, List<int> list)
    {
        Name = name;
        TagIdList = list;
    }
}
using System.Collections.Generic;
using LGame.LBehaviour;
using LGame.LCommon;
using LGame.LDebug;
using LGame.LSource;
using LGame.LUI;
using UnityEngine;

/***
 * 
 * 
 * 框架内容测试文件
 * 
 */

public class LCTestFramework : LABehaviour
{

    public override void Awake()
    {

        Dictionary<string, string> dic = new Dictionary<string, string>();

        string value;
        dic.TryGetValue(string.Empty, out value);
        Debug.Log(value);

    }

}

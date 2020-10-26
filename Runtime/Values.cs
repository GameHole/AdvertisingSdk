using System.Collections.Generic;
using UnityEngine;
namespace MiniGameSDK
{
	public class Values:AScriptableObject
	{
        public string appid;
        public string rewardid;
        public string insterid;

        public override string filePath => "优量宝参数/value";
    }
}

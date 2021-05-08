namespace MiniGameSDK
{
    [System.Serializable]
    public class UmParameterBase
    {
        public string appid;
        public string channal;
        public bool debug;
    }
    public class UmParameter : AScriptableObject
    {
        public override string filePath => "友盟参数/Parameter";
        //public string appid;
        //public string channal;
        //public bool debug;
        public UmParameterBase android;
        public UmParameterBase ios;
    }
}

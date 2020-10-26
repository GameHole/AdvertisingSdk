using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Threading.Tasks;

namespace MiniGameSDK
{
    [InitializeOnLoad]
	public class Setting
	{
        static Setting()
        {
            if (!EditorPrefs.HasKey("ylh.init"))
            {
                Init();
                EditorPrefs.SetInt("ylh.init", 1);
            }
        }
        static async void Init()
        {
            await Task.Delay(500);
            Set();
        }
        [MenuItem("优量宝/设置参数")]
        static void Set()
        {
            AssetHelper.CreateAsset<LogParm>();
            AssetHelper.CreateAsset<Values>();
            SetXml();
            GradleHelper.SetImplementation("com.lovedise:permissiongen:0.0.6");
            GradleHelper.SetImplementation("com.android.support:support-v4:28.0.0");
            AssetDatabase.Refresh();
        }
        [MenuItem("优量宝/设置Log参数")]
        static void SetLog()
        {
            _SetLog();
            AssetDatabase.Refresh();
        }
        static void SetXml()
        {
            var dc = XmlHelper.GetAndroidManifest();
            if (dc.FindNode("/manifest/application/provider", "android:name", "android.support.v4.content.FileProvider") != null) return;
            var app = dc.SelectSingleNode("/manifest/application");
            var pdr = dc.CreateElement("provider");
            pdr.AppendAttribute("name", "android.support.v4.content.FileProvider")
                .AppendAttribute("authorities", "${applicationId}.fileprovider")
                .AppendAttribute("exported", "false")
                .AppendAttribute("grantUriPermissions", "true");
            var meta = dc.CreateElement("meta-data");
            meta.AppendAttribute("name", "android.support.FILE_PROVIDER_PATHS")
                .AppendAttribute("resource", "@xml/gdt_file_path");
            pdr.AppendChild(meta);
            app.AppendChild(pdr);
            dc.SetPermission("android.permission.INTERNET");
            dc.SetPermission("android.permission.READ_PHONE_STATE");
            dc.SetPermission("android.permission.ACCESS_NETWORK_STATE");
            dc.SetPermission("android.permission.WRITE_EXTERNAL_STORAGE");
            dc.SetPermission("android.permission.ACCESS_WIFI_STATE");
            dc.SetPermission("android.permission.ACCESS_COARSE_LOCATION");
            dc.SetPermission("android.permission.REQUEST_INSTALL_PACKAGES");
            dc.SetPermission("android.permission.GET_TASKS");
            dc.SetPermission("android.permission.ACCESS_FINE_LOCATION");
            dc.Save();
        }
        static void _SetLog()
        {
            var pram = AScriptableObject.Get<LogParm>();
            var dc = XmlHelper.GetAndroidManifest();
            var nd = dc.FindNode("/manifest/application/meta-data", "android:name", "WANNUOSILI_LOG_APPID");
            if (nd != null)
            {
                nd.Attributes["android:value"].Value = pram.logAppId;
                nd = dc.FindNode("/manifest/application/meta-data", "android:name", "WANNUOSILI_LOG_CHANNEL");
                nd.Attributes["android:value"].Value = pram.channal;
            }
            else
            {
                var app = dc.SelectSingleNode("/manifest/application");
                var meta = dc.CreateElement("meta-data");
                meta.AppendAttribute("name", "WANNUOSILI_LOG_APPID")
                    .AppendAttribute("value", pram.logAppId);
                app.AppendChild(meta);
                meta = dc.CreateElement("meta-data");
                meta.AppendAttribute("name", "WANNUOSILI_LOG_CHANNEL")
                    .AppendAttribute("value", pram.channal);
                app.AppendChild(meta);
            }
           
            dc.Save();
        }
    }
}

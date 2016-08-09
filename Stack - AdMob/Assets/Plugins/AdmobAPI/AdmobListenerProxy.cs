﻿using UnityEngine;
using System.Collections;
namespace admob
{
	public class AdmobListenerProxy : AndroidJavaProxy {
		private IAdmobListener listener;
		internal AdmobListenerProxy(IAdmobListener listener)
            : base("com.admob.plugin.IAdmobListener")
		{
			this.listener = listener;
		}
         void onAdmobEvent(string adtype,string eventName,string paramString){
         	 listener.onAdmobEvent(adtype,eventName,paramString);
         }
		string toString(){
			return "AdmobListenerProxy";
		}
	}
}

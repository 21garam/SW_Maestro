using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Equipment_ {
	public const int KIND_COUNT = 32; //32 fixed
	public static readonly int[] BODY = new int[KIND_COUNT];
	public static readonly int[] EYE = new int[KIND_COUNT];
	public static readonly int[] MOUTH = new int[KIND_COUNT];
	public static readonly int[] FIN = new int[KIND_COUNT];
	//public static bool[] BUYING_BODY = new bool[KIND_COUNT];
	//public static bool[] BUYING_EYE = new bool[KIND_COUNT];
	//public static bool[] BUYING_MOUTH = new bool[KIND_COUNT];
	//public static bool[] BUYING_FIN = new bool[KIND_COUNT];
	
	private static Dictionary<string, int> TAG_TO_ID = new Dictionary<string, int>();
	
	static Equipment_(){
		for(int i = 0; i < KIND_COUNT; i++){
			BODY[i] = i;
			EYE[i] = i<<8;
			MOUTH[i] = i<<16;
			FIN[i] = i <<24;
		}
		//0~255 0x0000 0000 0000 0000 ~ 0x0000 0000 0000 FFFF
		//256~  0x0000 0000 0000 0000 ~ 0x0000 0000 FFFF 0000
		//256~  0x0000 0000 0000 0000 ~ 0x0000 FFFF 0000 0000
		//for(int i = 0; i < (KIND_COUNT / 2); i++){
		//}
		/*
		Debug.Log("BODY");
		for(int i = 0; i < KIND_COUNT; i++){
			Debug.Log(BODY[i]);
		}
		Debug.Log("EYE");
		for(int i = 0; i < KIND_COUNT; i++){
			Debug.Log(EYE[i]>>8);
		}
		Debug.Log("MOUTH");
		for(int i = 0; i < KIND_COUNT; i++){
			Debug.Log(MOUTH[i]>>16);
		}
		Debug.Log("FIN");
		for(int i = 0; i < KIND_COUNT; i++){
			Debug.Log(FIN[i]>>24);
		}
		*/
		Initialize_TAG_TO_ID();
		Debug.Log("Item_ Singleton is called");
	}
	
	static private void Initialize_TAG_TO_ID(){
		TAG_TO_ID.Add("Default", 0);
		TAG_TO_ID.Add("cat", 1);
		TAG_TO_ID.Add("round", 2);
		TAG_TO_ID.Add("sharp", 3);
		//TAG_TO_ID.Add("Robot", 3);
	}

	public static int GetEquipmentValueFromID(int bodyID, int eyeID, int mouthID, int finID){
		bodyID = bodyID < 0 ? 0 : bodyID;
		eyeID = eyeID < 0 ? 0 : eyeID;
		mouthID = mouthID < 0 ? 0 : mouthID;
		finID = finID < 0 ? 0 : finID;
		return (BODY[bodyID] | EYE[eyeID] | MOUTH[mouthID] | FIN[finID]);
	}
	
	public static int GetEuipmentValueFromTag(string bodyTag, string eyeTag, string mouthTag, string finTag){
		int bodyID = TAG_TO_ID.ContainsKey(bodyTag) ? TAG_TO_ID[bodyTag] : 0;
		int eyeID = TAG_TO_ID.ContainsKey(eyeTag) ? TAG_TO_ID[eyeTag] : 0;
		int mouthID = TAG_TO_ID.ContainsKey(mouthTag) ? TAG_TO_ID[mouthTag] : 0;
		int finID = TAG_TO_ID.ContainsKey(finTag) ? TAG_TO_ID[finTag] : 0;
		return GetEquipmentValueFromID(bodyID, eyeID, mouthID, finID);
	}
	
	public static int GetBodyValue_FromTag(string tag){
		int id = TAG_TO_ID.ContainsKey(tag) ? TAG_TO_ID[tag] : 0;
		return BODY[id];
	}
	
	public static int GetEyeValue_FromTag(string tag){
		int id = TAG_TO_ID.ContainsKey(tag) ? TAG_TO_ID[tag] : 0;
		return EYE[id];
	}
	
	public static int GetMouthValue_FromTag(string tag){
		int id = TAG_TO_ID.ContainsKey(tag) ? TAG_TO_ID[tag] : 0;
		return MOUTH[id];
	}
	
	public static int GetFinValue_FromTag(string tag){
		int id = TAG_TO_ID.ContainsKey(tag) ? TAG_TO_ID[tag] : 0;
		return FIN[id];
	}
	
	public static int GetBodyValue_FromEquipment(int equipment){
		return equipment & 0x000000FF;
	}
	
	public static int GetEyeValue_FromEquipment(int equipment){
		return equipment & 0x0000FF00;
	}
	
	public static int GetMouthValue_FromEquipment(int equipment){
		return equipment & 0x00FF0000;
	}
	
	public static int GetFinValue_FromEquipment(int equipment){
		return equipment & 0x7F000000;
	}
	
	public static int[] GetBuyingEquipment_IDList(int buying_equipment){
		int[] buyingEquipment_IDList;
		int count = 0;
		for(int i = 0; i < KIND_COUNT-1; i++){
			if(((buying_equipment >> i) & 0x00000001) == 1)
				count++;
		}
		buyingEquipment_IDList = new int[count+1];
		buyingEquipment_IDList[0] = 0;
		count = 1;
		for(int i = 0; i < KIND_COUNT-1; i++){
			if(((buying_equipment >> i) & 0x00000001) == 1)
				buyingEquipment_IDList[count++] = i+1;
		}
		return buyingEquipment_IDList;
	}
	
	public static int GetBuyingEquipment_Value(IEnumerable<int> buyingKeyList){
		int retValue = 0;
		foreach(int buyingKey in buyingKeyList){
			int operationValue = 0x00000001;
			if(buyingKey > 0){
				operationValue = operationValue << (buyingKey-1);
				retValue = retValue | operationValue;
			}
		}
		return retValue;
	}
}

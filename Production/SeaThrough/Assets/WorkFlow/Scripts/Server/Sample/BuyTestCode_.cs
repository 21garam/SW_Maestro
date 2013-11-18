using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuyTestCode_ : MonoBehaviour {
public tk2dUITextInput ID_TextInput;
	public tk2dUIItem acceptBtr; 
	public tk2dUIItem searchBtr; 
	
	public WWW_ www;
	
	public GameObject bodySet;
	tk2dUIToggleControl[] bodySetCheckBoxList;
	public List<int> bodyKeyList = new List<int>();
	
	public GameObject eyeSet;
	tk2dUIToggleControl[] eyeSetCheckBoxList;
	public List<int> eyeKeyList = new List<int>();
	
	public GameObject mouthSet;
	tk2dUIToggleControl[] mouthSetCheckBoxList;
	public List<int> mouthKeyList = new List<int>();
	
	public GameObject finSet;
	tk2dUIToggleControl[] finSetCheckBoxList;
	public List<int> finKeyList = new List<int>();
	
	//public MessageBox_ prefabsMsgBox;
	//MessageBox_ msgBox;
	
	void Start(){
		bodySetCheckBoxList = bodySet.transform.GetComponentsInChildren<tk2dUIToggleControl>();
		eyeSetCheckBoxList = eyeSet.transform.GetComponentsInChildren<tk2dUIToggleControl>();
		mouthSetCheckBoxList = mouthSet.transform.GetComponentsInChildren<tk2dUIToggleControl>();
		finSetCheckBoxList = finSet.transform.GetComponentsInChildren<tk2dUIToggleControl>();
		ClearIndexed();
	}
	
	void OnEnable() {
		acceptBtr.OnClick += Accept;
		searchBtr.OnClick += Search;
    }
	
	void ClearIndexed(){
		for(int i = 0; i < bodySetCheckBoxList.Length; i++)
			bodySetCheckBoxList[i].IsOn = false;
		
		for(int i = 0; i < eyeSetCheckBoxList.Length; i++)
			eyeSetCheckBoxList[i].IsOn = false;
		
		for(int i = 0; i < mouthSetCheckBoxList.Length; i++)
			mouthSetCheckBoxList[i].IsOn = false;
		
		for(int i = 0; i < finSetCheckBoxList.Length; i++)
			finSetCheckBoxList[i].IsOn = false;
	}
	
	void OnDisable() {
		acceptBtr.OnClick -= Accept;
		searchBtr.OnClick -= Search;
    }
	
	void Accept(){
		bodyKeyList.Clear();
		for(int i = 0; i < bodySetCheckBoxList.Length; i++){
			if(bodySetCheckBoxList[i].IsOn == true){
				bodyKeyList.Add(i);
			}
		}
		int buying_equipment_body = Equipment_.GetBuyingEquipment_Value(bodyKeyList);
		
		eyeKeyList.Clear();
		for(int i = 0; i < eyeSetCheckBoxList.Length; i++){
			if(eyeSetCheckBoxList[i].IsOn == true){
				eyeKeyList.Add(i);
			}
		}
		int buying_equipment_eye = Equipment_.GetBuyingEquipment_Value(eyeKeyList);
		
		mouthKeyList.Clear();
		for(int i = 0; i < mouthSetCheckBoxList.Length; i++){
			if(mouthSetCheckBoxList[i].IsOn == true){
				mouthKeyList.Add(i);
			}
		}
		int buying_equipment_mouth = Equipment_.GetBuyingEquipment_Value(mouthKeyList);
		
		finKeyList.Clear();
		for(int i = 0; i < finSetCheckBoxList.Length; i++){
			if(finSetCheckBoxList[i].IsOn == true){
				finKeyList.Add(i);
			}
		}
		int buying_equipment_fin = Equipment_.GetBuyingEquipment_Value(finKeyList);
		
		if(ID_TextInput.Text != "")
			www.UpdateAccount(ID_TextInput.Text, AcceptCallBackFunc, 
							  WWW_.INTEGER_NULL, WWW_.INTEGER_NULL, WWW_.INTEGER_NULL, 
							  WWW_.INTEGER_NULL, WWW_.INTEGER_NULL, buying_equipment_body, buying_equipment_eye, buying_equipment_mouth, buying_equipment_fin);
	}
	
	void Test(string msg){
		Debug.Log(msg);	
	}
	
	void AcceptCallBackFunc(string msg){
		if(msg == WWWMessage_.OK)
			MessageBox("Update completed");
		else
			MessageBox("ID is not existed");
	}
	
	void Search(){
		ClearIndexed();
		if(ID_TextInput.Text != "")
			www.GetPlayerInfo(ID_TextInput.Text, SetBuyInfo);
	}
	
	void SetBuyInfo(string xml){
		if(XMLParser_.PlayerInfoXMLParse(xml)){
			//Debug.Log(XMLParser_.PlayerAccountInfo.ToString());
			int buying_equipment_body = XMLParser_.PlayerAccountInfo.buying_equipment_body;
			int buying_equipment_eye = XMLParser_.PlayerAccountInfo.buying_equipment_eye;
			int buying_equipment_mouth = XMLParser_.PlayerAccountInfo.buying_equipment_mouth;
			int buying_equipment_fin = XMLParser_.PlayerAccountInfo.buying_equipment_fin;
			int[] buyingBody_IDList = Equipment_.GetBuyingEquipment_IDList(buying_equipment_body);
			for(int i = 0; i < buyingBody_IDList.Length; i++){
				int buyingBodyID = buyingBody_IDList[i];
				if(buyingBodyID < bodySetCheckBoxList.Length){
					bodySetCheckBoxList[buyingBodyID].IsOn = true;
				}
			}
			buyingBody_IDList = null;
			
			int[] buyingEye_IDList = Equipment_.GetBuyingEquipment_IDList(buying_equipment_eye);
			for(int i = 0; i < buyingEye_IDList.Length; i++){
				int buyingEyeID = buyingEye_IDList[i];
				if(buyingEyeID < eyeSetCheckBoxList.Length){
					eyeSetCheckBoxList[buyingEyeID].IsOn = true;
				}
			}
			buyingEye_IDList = null;
			
			int[] buyingMouth_IDList = Equipment_.GetBuyingEquipment_IDList(buying_equipment_mouth);
			for(int i = 0; i < buyingMouth_IDList.Length; i++){
				int buyingMouthID = buyingMouth_IDList[i];
				if(buyingMouthID < mouthSetCheckBoxList.Length){
					mouthSetCheckBoxList[buyingMouthID].IsOn = true;
				}
			}
			buyingMouth_IDList = null;
			
			int[] buyingFin_IDList = Equipment_.GetBuyingEquipment_IDList(buying_equipment_fin);
			for(int i = 0; i < buyingFin_IDList.Length; i++){
				int buyingFinID = buyingFin_IDList[i];
				if(buyingFinID < finSetCheckBoxList.Length){
					finSetCheckBoxList[buyingFinID].IsOn = true;
				}
			}
			buyingFin_IDList = null;
			/*
			bool[] bodyCheckList = Equipment_.GetBuyingEquipmentBody_CheckList(buying_equipment_body);
			for(int i = 0; i < bodySetCheckBoxList.Length; i++)
				if(bodyCheckList[i])
					bodySetCheckBoxList[i].IsOn = true;
			bodyCheckList = null;
			
			bool[] eyeCheckList = Equipment_.GetBuyingEquipmentEye_CheckList(buying_equipment_eye);
			for(int i = 0; i < eyeSetCheckBoxList.Length; i++)
				if(eyeCheckList[i])
					eyeSetCheckBoxList[i].IsOn = true;
			eyeCheckList = null;
			
			bool[] mouthCheckList = Equipment_.GetBuyingEquipmentMouth_CheckList(buying_equipment_mouth);
			for(int i = 0; i < mouthSetCheckBoxList.Length; i++)
				if(mouthCheckList[i])
					mouthSetCheckBoxList[i].IsOn = true;
			mouthCheckList = null;
			
			bool[] finCheckList = Equipment_.GetBuyingEquipmentFin_CheckList(buying_equipment_fin);
			for(int i = 0; i < finSetCheckBoxList.Length; i++)
				if(finCheckList[i])
					finSetCheckBoxList[i].IsOn = true;
			finCheckList = null;
			*/
		}
		else{
			MessageBox("ID is not existed");
		}
	}
	
	private void MessageBox(string msg){
		//msgBox = GameObject.Instantiate(prefabsMsgBox) as MessageBox_; 
		//msgBox.Initalize(this, msg);
	}
}

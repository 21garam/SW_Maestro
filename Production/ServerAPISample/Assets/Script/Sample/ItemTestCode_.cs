using UnityEngine;
using System.Collections;

public class ItemTestCode_ : MonoBehaviour {
	public tk2dUITextInput ID_TextInput;
	public tk2dUIItem acceptBtr; 
	public tk2dUIItem searchBtr; 
	
	public WWW_ www;
	
	public tk2dUIToggleButtonGroup bodySet;
	public tk2dUIToggleButtonGroup eyeSet;
	public tk2dUIToggleButtonGroup mouthSet;
	public tk2dUIToggleButtonGroup finSet;
	
	public MessageBox_ prefabsMsgBox;
	MessageBox_ msgBox;
	
	void Start(){
		ClearIndexed();
	}
	
	void OnEnable() {
		acceptBtr.OnClick += Accept;
		searchBtr.OnClick += Search;
    }
	
	void ClearIndexed(){
		bodySet.SelectedIndex = 0;
		eyeSet.SelectedIndex = 0;
		mouthSet.SelectedIndex = 0;
		finSet.SelectedIndex = 0;
	}
	
	void OnDisable() {
		acceptBtr.OnClick -= Accept;
		searchBtr.OnClick -= Search;
    }
	
	void Accept(){
		//int equipment = Equipment_.GetEuipmentValueFromTag("Toy", "SuperMario", "Default", "Default");
		int equipment = Equipment_.GetEquipmentValueFromID(bodySet.SelectedIndex, eyeSet.SelectedIndex, mouthSet.SelectedIndex, finSet.SelectedIndex);
		//Debug.Log(equipment);
		if(ID_TextInput.Text != "")
			www.UpdateAccount(ID_TextInput.Text, AcceptCallBackFunc, 
							  WWW_.INTEGER_NULL, WWW_.INTEGER_NULL, WWW_.INTEGER_NULL, 
							  equipment);
	}
	
	void Test(string msg){
		Debug.Log(msg);	
	}
	
	void AcceptCallBackFunc(string msg){
		if(msg == WWWMessage_.OK){
			MessageBox("Update completed");
		}
		else
			MessageBox("ID is not existed");
	}
	
	void Search(){
		if(ID_TextInput.Text != "")
			www.GetPlayerInfo(ID_TextInput.Text, SetEquipmentInfo);
	}
	
	void SetEquipmentInfo(string xml){
		if(XMLParser_.PlayerInfoXMLParse(xml)){
			//Debug.Log(XMLParser_.PlayerAccountInfo.ToString());
			int equipment = XMLParser_.PlayerAccountInfo.equipment;
			int body = Equipment_.GetBodyValue_FromEquipment(equipment);
			int eye = Equipment_.GetEyeValue_FromEquipment(equipment);
			int mouth = Equipment_.GetMouthValue_FromEquipment(equipment);
			int fin = Equipment_.GetFinValue_FromEquipment(equipment);
			SetBodyView(body);
			SetEyeView(eye);
			SetMouthView(mouth);
			SetFinView(fin);
		}
		else{
			MessageBox("ID is not existed");
		}
	}
	
	void SetBodyView(long value_){
		if(value_ == Equipment_.GetBodyValue_FromTag("Default"))
			bodySet.SelectedIndex = 0;
		if(value_ == Equipment_.GetBodyValue_FromTag("Toy"))
			bodySet.SelectedIndex = 1;
		if(value_ == Equipment_.GetBodyValue_FromTag("SuperMario"))
			bodySet.SelectedIndex = 2;
	}
	
	void SetEyeView(long value_){
		if(value_ == Equipment_.GetEyeValue_FromTag("Default"))
			eyeSet.SelectedIndex = 0;
		if(value_ == Equipment_.GetEyeValue_FromTag("Toy"))
			eyeSet.SelectedIndex = 1;
		if(value_ == Equipment_.GetEyeValue_FromTag("SuperMario"))
			eyeSet.SelectedIndex = 2;
	}
	
	void SetMouthView(long value_){
		if(value_ == Equipment_.GetMouthValue_FromTag("Default"))
			mouthSet.SelectedIndex = 0;
		if(value_ == Equipment_.GetMouthValue_FromTag("Toy"))
			mouthSet.SelectedIndex = 1;
		if(value_ == Equipment_.GetMouthValue_FromTag("SuperMario"))
			mouthSet.SelectedIndex = 2;
	}
	
	void SetFinView(long value_){
		if(value_ == Equipment_.GetFinValue_FromTag("Default"))
			finSet.SelectedIndex = 0;
		if(value_ == Equipment_.GetFinValue_FromTag("Toy"))
			finSet.SelectedIndex = 1;
		if(value_ == Equipment_.GetFinValue_FromTag("SuperMario"))
			finSet.SelectedIndex = 2;
	}
	
	private void MessageBox(string msg){
		msgBox = GameObject.Instantiate(prefabsMsgBox) as MessageBox_; 
		msgBox.Initalize(this, msg);
	}
}

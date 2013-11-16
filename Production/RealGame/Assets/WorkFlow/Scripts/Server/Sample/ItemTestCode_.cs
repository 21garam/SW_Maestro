using UnityEngine;
using System.Collections;

public class ItemTestCode_ : MonoBehaviour {
	public WWW_ www;
	
	public GameObject MyC;
	public GameObject MyC2;
	
	int bodyID = 0;
	int eyeID = 0;
	int mouthID = 0;
	int finID = 0 ;
	
	//public MessageBox_ prefabsMsgBox;
	//MessageBox_ msgBox;
	
	//Item Button
	public tk2dUIItem C_1_1;
	public tk2dUIItem C_1_2;
	public tk2dUIItem C_1_3;
	public tk2dUIItem C_1_4;
	public tk2dUIItem C_2_1;
	public tk2dUIItem C_2_2;
	public tk2dUIItem C_2_3;
	public tk2dUIItem C_2_4;
	public tk2dUIItem C_3_1;
	public tk2dUIItem C_3_2;
	public tk2dUIItem C_3_3;
	public tk2dUIItem C_3_4;
	public tk2dUIItem C_4_1;
	public tk2dUIItem C_4_2;
	public tk2dUIItem C_4_3;
	public tk2dUIItem C_4_4;
	
	void Start(){
		Search();
	}
	
	void OnEnable() {
		C_1_1.OnClick += SetC_1_1;
		C_1_2.OnClick += SetC_1_2;
		C_1_3.OnClick += SetC_1_3;
		C_1_4.OnClick += SetC_1_4;
		C_2_1.OnClick += SetC_2_1;
		C_2_2.OnClick += SetC_2_2;
		C_2_3.OnClick += SetC_2_3;
		C_2_4.OnClick += SetC_2_4;
		C_3_1.OnClick += SetC_3_1;
		C_3_2.OnClick += SetC_3_2;
		C_3_3.OnClick += SetC_3_3;
		C_3_4.OnClick += SetC_3_4;
		C_4_1.OnClick += SetC_4_1;
		C_4_2.OnClick += SetC_4_2;
		C_4_3.OnClick += SetC_4_3;
		C_4_4.OnClick += SetC_4_4;
    }
	
	void OnDisable() {
		C_1_1.OnClick -= SetC_1_1;
		C_1_2.OnClick -= SetC_1_2;
		C_1_3.OnClick -= SetC_1_3;
		C_1_4.OnClick -= SetC_1_4;
		C_2_1.OnClick -= SetC_2_1;
		C_2_2.OnClick -= SetC_2_2;
		C_2_3.OnClick -= SetC_2_3;
		C_2_4.OnClick -= SetC_2_4;
		C_3_1.OnClick -= SetC_3_1;
		C_3_2.OnClick -= SetC_3_2;
		C_3_3.OnClick -= SetC_3_3;
		C_3_4.OnClick -= SetC_3_4;
		C_4_1.OnClick -= SetC_4_1;
		C_4_2.OnClick -= SetC_4_2;
		C_4_3.OnClick -= SetC_4_3;
		C_4_4.OnClick -= SetC_4_4;
    }
	
	void Accept(){
		//int equipment = Equipment_.GetEquipmentValueFromID(bodySet, eyeSet, 
		//	Equipment_.GetMouthValue_FromEquipment(mouthSet),Equipment_.GetFinValue_FromEquipment(finSet));
		int equipment = Equipment_.GetEquipmentValueFromID(bodyID, eyeID, mouthID, finID);
		if(GUI_Setting_.PLAYER_ID != ""){
			Debug.Log("ID : " + GUI_Setting_.PLAYER_ID);
			www.UpdateAccount(GUI_Setting_.PLAYER_ID, AcceptCallBackFunc, 
							  WWW_.INTEGER_NULL, WWW_.INTEGER_NULL, WWW_.INTEGER_NULL, 
							  equipment);
		}
		SetMyEquipmentC(bodyID, eyeID, mouthID, finID);
		
		/*SharedData.Instance.bodyId = bodyID;
		SharedData.Instance.eyesId = eyeID;
		SharedData.Instance.mouthId = mouthID;
		SharedData.Instance.finId = finID;*/
	}
	
	void Test(string msg){
		Debug.Log(msg);	
	}
	
	void AcceptCallBackFunc(string msg){
		Debug.Log(msg);
	}
	
	void Search(){
		if(GUI_Setting_.PLAYER_ID == ""){
			Debug.Log("Search : GUI_Setting_.PLAYER_ID is null");
			return;
		}
		www.GetPlayerInfo(GUI_Setting_.PLAYER_ID, SetEquipmentInfo);
		//SetMyEquipmentC(bodySet,eyeSet,mouthSet,finSet);
	}
	
	void SetEquipmentInfo(string xml){
		if(XMLParser_.PlayerInfoXMLParse(xml)){
			//Debug.Log(XMLParser_.PlayerAccountInfo.ToString());
			int equipment = XMLParser_.PlayerAccountInfo.equipment;
			int body = Equipment_.GetBodyValue_FromEquipment(equipment);
			int eye = Equipment_.GetEyeValue_FromEquipment(equipment);
			int mouth = Equipment_.GetMouthValue_FromEquipment(equipment);
			int fin = Equipment_.GetFinValue_FromEquipment(equipment);//GetFinVale_FromEquipment(equipment);
			SetMyEquipmentC(body, eye, mouth, fin);
			//bodySet = body;
			//eyeSet = eye;
			//mouthSet = mouth;
			//finSet = fin;
		}
		else{
			Debug.Log("ID is not existed");
		}
	}
	
	void SetMyEquipmentC(int body, int eye, int mouth, int fin){
		SetMyBody(body);
		SetMyeye(eye);
		SetMyfin(fin);
		SetMymouth(mouth);
	}
	
	void SetMyBody(int body){
		if(body == Equipment_.GetBodyValue_FromTag("Defualt")){
			MyC.transform.FindChild("body").GetComponent<tk2dSprite>().SetSprite("Body_1");
			MyC2.transform.FindChild("body").GetComponent<tk2dSprite>().SetSprite("Body_1");
			bodyID = 0;
		}
		else if(body == Equipment_.GetBodyValue_FromTag("cat")){
			MyC.transform.FindChild("body").GetComponent<tk2dSprite>().SetSprite("Body_2");	
			MyC2.transform.FindChild("body").GetComponent<tk2dSprite>().SetSprite("Body_2");
			bodyID = 1;
		}
		else if(body == Equipment_.GetBodyValue_FromTag("round")){
			MyC.transform.FindChild("body").GetComponent<tk2dSprite>().SetSprite("Body_3");	
			MyC2.transform.FindChild("body").GetComponent<tk2dSprite>().SetSprite("Body_3");
			bodyID = 2;
		}
		else if(body == Equipment_.GetBodyValue_FromTag("sharp")){
			MyC.transform.FindChild("body").GetComponent<tk2dSprite>().SetSprite("Body_4");
			MyC2.transform.FindChild("body").GetComponent<tk2dSprite>().SetSprite("Body_4");
			bodyID = 3;
		}
		else{
			MyC.transform.FindChild("body").GetComponent<tk2dSprite>().SetSprite("Body_1");	
			MyC2.transform.FindChild("body").GetComponent<tk2dSprite>().SetSprite("Body_1");
			Debug.Log("Body Value : " + body.ToString());
			bodyID = 0;
		}
	}
	
	void SetMyeye(int eye){
		if(eye == Equipment_.GetBodyValue_FromTag("Defualt")){
			MyC.transform.FindChild("eye").GetComponent<tk2dSprite>().SetSprite("Eye_1");	
			MyC2.transform.FindChild("eye").GetComponent<tk2dSprite>().SetSprite("Eye_1");	
		}
		else if(eye == Equipment_.GetBodyValue_FromTag("cat")){
			MyC.transform.FindChild("eye").GetComponent<tk2dSprite>().SetSprite("Eye_2");	
			MyC2.transform.FindChild("eye").GetComponent<tk2dSprite>().SetSprite("Eye_2");	
		}
		else if(eye == Equipment_.GetBodyValue_FromTag("round")){
			MyC.transform.FindChild("eye").GetComponent<tk2dSprite>().SetSprite("Eye_3");	
			MyC2.transform.FindChild("eye").GetComponent<tk2dSprite>().SetSprite("Eye_3");	
		}
		else if(eye == Equipment_.GetBodyValue_FromTag("sharp")){
			MyC.transform.FindChild("eye").GetComponent<tk2dSprite>().SetSprite("Eye_4");
			MyC2.transform.FindChild("eye").GetComponent<tk2dSprite>().SetSprite("Eye_4");	
		}
		else{
			MyC.transform.FindChild("eye").GetComponent<tk2dSprite>().SetSprite("Eye_1");
			MyC2.transform.FindChild("eye").GetComponent<tk2dSprite>().SetSprite("Eye_1");	
			Debug.Log("eye Value : " + eye.ToString());
		}
	}
	
	void SetMymouth(int mouth){
		if(mouth == Equipment_.GetBodyValue_FromTag("Defualt")){
			MyC.transform.FindChild("mouth").GetComponent<tk2dSprite>().SetSprite("Mouth_1");	
			MyC2.transform.FindChild("mouth").GetComponent<tk2dSprite>().SetSprite("Mouth_1");
		}
		else if(mouth == Equipment_.GetBodyValue_FromTag("cat")){
			MyC.transform.FindChild("mouth").GetComponent<tk2dSprite>().SetSprite("Mouth_2");
			MyC2.transform.FindChild("mouth").GetComponent<tk2dSprite>().SetSprite("Mouth_2");
		}
		else if(mouth == Equipment_.GetBodyValue_FromTag("round")){
			MyC.transform.FindChild("mouth").GetComponent<tk2dSprite>().SetSprite("Mouth_3");
			MyC2.transform.FindChild("mouth").GetComponent<tk2dSprite>().SetSprite("Mouth_3");
		}
		else if(mouth == Equipment_.GetBodyValue_FromTag("sharp")){
			MyC.transform.FindChild("mouth").GetComponent<tk2dSprite>().SetSprite("Mouth_4");
			MyC2.transform.FindChild("mouth").GetComponent<tk2dSprite>().SetSprite("Mouth_4");
		}
		else{
			MyC.transform.FindChild("mouth").GetComponent<tk2dSprite>().SetSprite("Mouth_1");
			MyC2.transform.FindChild("mouth").GetComponent<tk2dSprite>().SetSprite("Mouth_1");
			Debug.Log("mouth Value : " + mouth.ToString());
		}
	}
	
	void SetMyfin(int fin){
		if(fin == Equipment_.GetBodyValue_FromTag("Defualt")){
			MyC.transform.FindChild("fin").GetComponent<tk2dSprite>().SetSprite("Fin_1");
			MyC2.transform.FindChild("fin").GetComponent<tk2dSprite>().SetSprite("Fin_1");
		}
		else if(fin == Equipment_.GetBodyValue_FromTag("cat")){
			MyC.transform.FindChild("fin").GetComponent<tk2dSprite>().SetSprite("Fin_2");	
			MyC2.transform.FindChild("fin").GetComponent<tk2dSprite>().SetSprite("Fin_2");
		}
		else if(fin == Equipment_.GetBodyValue_FromTag("round")){
			MyC.transform.FindChild("fin").GetComponent<tk2dSprite>().SetSprite("Fin_3");
			MyC2.transform.FindChild("fin").GetComponent<tk2dSprite>().SetSprite("Fin_3");
		}
		else if(fin == Equipment_.GetBodyValue_FromTag("sharp")){
			MyC.transform.FindChild("fin").GetComponent<tk2dSprite>().SetSprite("Fin_4");
			MyC2.transform.FindChild("fin").GetComponent<tk2dSprite>().SetSprite("Fin_4");
		}
		else{
			MyC.transform.FindChild("fin").GetComponent<tk2dSprite>().SetSprite("Fin_1");
			MyC2.transform.FindChild("fin").GetComponent<tk2dSprite>().SetSprite("Fin_1");
			Debug.Log("fin Value : " + fin.ToString());
		}
	}
	
	void SetC_1_1(){
		bodyID = 0;//Equipment_.BODY[0];
		Accept();
	}
	
	void SetC_2_1(){
		bodyID = 1;//Equipment_.BODY[1];
		Accept();
	}
	
	void SetC_3_1(){
		bodyID = 2;//Equipment_.BODY[2];
		Accept();
	}
	
	void SetC_4_1(){
		bodyID = 3;//Equipment_.BODY[3];
		Accept();
	}
	
	void SetC_1_2(){
		eyeID = 0;//Equipment_.EYE[0];
		Accept();
	}
	
	void SetC_2_2(){
		eyeID = 1;//Equipment_.EYE[1];
		Accept();
	}
	
	void SetC_3_2(){
		eyeID = 2;//Equipment_.EYE[2];
		Accept();
	}
	
	void SetC_4_2(){
		eyeID = 3;//Equipment_.EYE[3];
		Accept();
	}
	
	void SetC_1_3(){
		mouthID = 0;//Equipment_.MOUTH[0];
		Accept();
	}
	
	void SetC_2_3(){
		mouthID = 1;//Equipment_.MOUTH[1];
		Accept();
	}
	
	void SetC_3_3(){
		mouthID = 2;//Equipment_.MOUTH[2];
		Accept();
	}
	
	void SetC_4_3(){
		mouthID = 3;//Equipment_.MOUTH[3];
		Accept();
	}
	
	void SetC_1_4(){
		finID = 0;//Equipment_.FIN[0];
		Accept();
	}
	
	void SetC_2_4(){
		finID = 1;//Equipment_.FIN[1];
		Accept();
	}
	
	void SetC_3_4(){
		finID = 2;//Equipment_.FIN[2];
		Accept();
	}
	
	void SetC_4_4(){
		finID = 3;//Equipment_.FIN[3];
		Accept();
	}
	
	private void MessageBox(string msg){
	//	msgBox = GameObject.Instantiate(prefabsMsgBox) as MessageBox_; 
	//	msgBox.Initalize(this, msg);
	}
}

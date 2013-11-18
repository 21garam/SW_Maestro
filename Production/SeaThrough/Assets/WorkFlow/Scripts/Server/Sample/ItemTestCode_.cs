using UnityEngine;
using System.Collections;

public class ItemTestCode_ : MonoBehaviour {
	public WWW_ www;
	
	public GameObject MyC;
	public GameObject MyC2;
	public GameObject MyC3;
	
	public static int bodyID = 0;
	public static int eyeID = 0;
	public static int mouthID = 0;
	public static int finID = 0 ;
	
	public tk2dUIProgressBar hp;
	public tk2dUIProgressBar score;
	public tk2dUIProgressBar fever;
	
	bool a = true;
	
	//public MessageBox_ prefabsMsgBox;
	//MessageBox_ msgBox;
	
	public tk2dUIItem backBtr;
	
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
		//Search();
	}
	
	public void Initialize(){
		Search();
		SetProgressbar();
		//hp.Value = XMLParser_.PlayerAccountInfo.hp_lv;
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
		
		backBtr.OnClick += Back;
    }
	
	void Back(){
		//backBtr.GetComponent<Transformer_>().BeginTransform();
		Transformer_ trans = backBtr.GetComponent<Transformer_>();
		trans.m_target.gameObject.transform.GetChild(0).GetComponent<RankingListCode_>().SetPlayerCostume();
		trans.BeginTransform();
		//Debug.Log("aaa");
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
		
		backBtr.OnClick -= Back;
    }
	
	void Accept(){
		//int equipment = Equipment_.GetEquipmentValueFromID(bodySet, eyeSet, 
		//	Equipment_.GetMouthValue_FromEquipment(mouthSet),Equipment_.GetFinValue_FromEquipment(finSet));
		Debug.Log("bodyID : " + bodyID.ToString() + ", eyeID : " + eyeID.ToString() + ", mouthID : " + mouthID.ToString() + ", finID : " + finID.ToString());
		int equipment = Equipment_.GetEquipmentValueFromID(bodyID, eyeID, mouthID, finID);
		if(GUI_Setting_.PLAYER_ID != ""){
			Debug.Log("ID : " + GUI_Setting_.PLAYER_ID);
			www.UpdateAccount(GUI_Setting_.PLAYER_ID, AcceptCallBackFunc, 
							  WWW_.INTEGER_NULL, WWW_.INTEGER_NULL, WWW_.INTEGER_NULL, 
							  equipment);
		}
		SetMyEquipmentC(equipment);
		//SetMyEquipmentC(bodyID, eyeID, mouthID, finID);
		
		SharedData.bodyId = bodyID;
		SharedData.eyesId = eyeID;
		SharedData.mouthId = mouthID;
		SharedData.finId = finID;
		
		SetProgressbar();
	}
	
	void SetProgressbar(){
		float bodyRate = (float)(SharedData.DEFAULT_HP + (SharedData.MAX_HP - SharedData.DEFAULT_HP) *  (float)(bodyID+1)/4);
		float scoreRate = (float)(SharedData.DEFAULT_HP + (SharedData.MAX_SCORE - SharedData.DEFAULT_SCORE) *  (float)(mouthID+1)/4);
		float feverRate = (float)(SharedData.DEFAULT_HP + (SharedData.MAX_FEVER - SharedData.DEFAULT_FEVER) *  (float)(finID+1)/4);
		bodyRate = bodyRate / SharedData.MAX_HP;
		hp.Value = bodyRate;
		scoreRate = scoreRate / SharedData.MAX_SCORE;
		score.Value = scoreRate;
		feverRate = feverRate / SharedData.MAX_FEVER;
		fever.Value = feverRate;
		//Debug.Log("SetProgressbar : " + bodyRate.ToString());
	}
	
	void Test(string msg){
		Debug.Log(msg);	
	}
	
	void AcceptCallBackFunc(string msg){
		Debug.Log(msg);
	}
	
	private void Search(){
		if(GUI_Setting_.PLAYER_ID != ""){
			www.GetPlayerInfo(GUI_Setting_.PLAYER_ID, SetEquipmentInfo);
		}else{
			
		}
		//SetMyEquipmentC(bodySet,eyeSet,mouthSet,finSet);
	}
	
	void SetEquipmentInfo(string xml){
		if(XMLParser_.PlayerInfoXMLParse(xml)){
			//Debug.Log(XMLParser_.PlayerAccountInfo.ToString());
			int equipment = XMLParser_.PlayerAccountInfo.equipment;
			//int body = Equipment_.GetBodyValue_FromEquipment(equipment);
			//int eye = Equipment_.GetEyeValue_FromEquipment(equipment);
			//int mouth = Equipment_.GetMouthValue_FromEquipment(equipment);
			//int fin = Equipment_.GetFinValue_FromEquipment(equipment);//GetFinVale_FromEquipment(equipment);
			//Debug.Log("SetEquipmentInfo : " + equipment.ToString());
			SetMyEquipmentC(equipment);
			//bodySet = body;
			//eyeSet = eye;
			//mouthSet = mouth;
			//finSet = fin;
		}
		else{
			Debug.Log("ID is not existed");
		}
	}
	
	void SetMyEquipmentC(int equipment){
		int body = Equipment_.GetBodyValue_FromEquipment(equipment);
		int eye = Equipment_.GetEyeValue_FromEquipment(equipment);
		int mouth = Equipment_.GetMouthValue_FromEquipment(equipment);
		int fin = Equipment_.GetFinValue_FromEquipment(equipment);//GetFinVale_FromEquipment(equipment);
		//Debug.Log("BODY : " + body.ToString() + ", EYE : " + eye.ToString() + ", MOUTH : " + mouth.ToString() + ", FIN" + fin.ToString());
		//Debug.Log("" + Equipment_.GetBodyValue_FromTag("round").ToString());
		SetMyEquipmentC(body, eye, mouth, fin);
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
			MyC3.transform.FindChild("body").GetComponent<tk2dSprite>().SetSprite("Body_1");
			bodyID = 0;
		}
		else if(body == Equipment_.GetBodyValue_FromTag("cat")){
			MyC.transform.FindChild("body").GetComponent<tk2dSprite>().SetSprite("Body_2");	
			MyC2.transform.FindChild("body").GetComponent<tk2dSprite>().SetSprite("Body_2");
			MyC3.transform.FindChild("body").GetComponent<tk2dSprite>().SetSprite("Body_2");
			bodyID = 1;
		}
		else if(body == Equipment_.GetBodyValue_FromTag("round")){
			MyC.transform.FindChild("body").GetComponent<tk2dSprite>().SetSprite("Body_3");	
			MyC2.transform.FindChild("body").GetComponent<tk2dSprite>().SetSprite("Body_3");
			MyC3.transform.FindChild("body").GetComponent<tk2dSprite>().SetSprite("Body_3");
			bodyID = 2;
		}
		else if(body == Equipment_.GetBodyValue_FromTag("sharp")){
			MyC.transform.FindChild("body").GetComponent<tk2dSprite>().SetSprite("Body_4");
			MyC2.transform.FindChild("body").GetComponent<tk2dSprite>().SetSprite("Body_4");
			MyC3.transform.FindChild("body").GetComponent<tk2dSprite>().SetSprite("Body_4");
			bodyID = 3;
		}
		else{
			MyC.transform.FindChild("body").GetComponent<tk2dSprite>().SetSprite("Body_1");	
			MyC2.transform.FindChild("body").GetComponent<tk2dSprite>().SetSprite("Body_1");
			MyC3.transform.FindChild("body").GetComponent<tk2dSprite>().SetSprite("Body_1");
			Debug.Log("Body Value : " + body.ToString());
			bodyID = 0;
		}
	}
	
	void SetMyeye(int eye){
		if(eye == Equipment_.GetEyeValue_FromTag("Defualt")){
			MyC.transform.FindChild("eye").GetComponent<tk2dSprite>().SetSprite("Eye_1");	
			MyC2.transform.FindChild("eye").GetComponent<tk2dSprite>().SetSprite("Eye_1");	
			MyC3.transform.FindChild("eye").GetComponent<tk2dSprite>().SetSprite("Eye_1");	
			eyeID = 0;
		}
		else if(eye == Equipment_.GetEyeValue_FromTag("cat")){
			MyC.transform.FindChild("eye").GetComponent<tk2dSprite>().SetSprite("Eye_2");	
			MyC2.transform.FindChild("eye").GetComponent<tk2dSprite>().SetSprite("Eye_2");	
			MyC3.transform.FindChild("eye").GetComponent<tk2dSprite>().SetSprite("Eye_2");	
			eyeID = 1;
		}
		else if(eye == Equipment_.GetEyeValue_FromTag("round")){
			MyC.transform.FindChild("eye").GetComponent<tk2dSprite>().SetSprite("Eye_3");	
			MyC2.transform.FindChild("eye").GetComponent<tk2dSprite>().SetSprite("Eye_3");	
			MyC3.transform.FindChild("eye").GetComponent<tk2dSprite>().SetSprite("Eye_3");	
			eyeID = 2;
		}
		else if(eye == Equipment_.GetEyeValue_FromTag("sharp")){
			MyC.transform.FindChild("eye").GetComponent<tk2dSprite>().SetSprite("Eye_4");
			MyC2.transform.FindChild("eye").GetComponent<tk2dSprite>().SetSprite("Eye_4");	
			MyC3.transform.FindChild("eye").GetComponent<tk2dSprite>().SetSprite("Eye_4");	
			eyeID = 3;
		}
		else{
			MyC.transform.FindChild("eye").GetComponent<tk2dSprite>().SetSprite("Eye_1");
			MyC2.transform.FindChild("eye").GetComponent<tk2dSprite>().SetSprite("Eye_1");	
			MyC3.transform.FindChild("eye").GetComponent<tk2dSprite>().SetSprite("Eye_1");	
			eyeID = 0;
			Debug.Log("eye Value : " + eye.ToString());
		}
		Debug.Log("SetMyeye : " + eyeID.ToString());
	}
	
	void SetMymouth(int mouth){
		if(mouth == Equipment_.GetMouthValue_FromTag("Defualt")){
			MyC.transform.FindChild("mouth").GetComponent<tk2dSprite>().SetSprite("Mouth_1");	
			MyC2.transform.FindChild("mouth").GetComponent<tk2dSprite>().SetSprite("Mouth_1");
			MyC3.transform.FindChild("mouth").GetComponent<tk2dSprite>().SetSprite("Mouth_1");
			mouthID = 0;
		}
		else if(mouth == Equipment_.GetMouthValue_FromTag("cat")){
			MyC.transform.FindChild("mouth").GetComponent<tk2dSprite>().SetSprite("Mouth_2");
			MyC2.transform.FindChild("mouth").GetComponent<tk2dSprite>().SetSprite("Mouth_2");
			MyC3.transform.FindChild("mouth").GetComponent<tk2dSprite>().SetSprite("Mouth_2");
			mouthID = 1;
		}
		else if(mouth == Equipment_.GetMouthValue_FromTag("round")){
			MyC.transform.FindChild("mouth").GetComponent<tk2dSprite>().SetSprite("Mouth_3");
			MyC2.transform.FindChild("mouth").GetComponent<tk2dSprite>().SetSprite("Mouth_3");
			MyC3.transform.FindChild("mouth").GetComponent<tk2dSprite>().SetSprite("Mouth_3");
			mouthID = 2;
		}
		else if(mouth == Equipment_.GetMouthValue_FromTag("sharp")){
			MyC.transform.FindChild("mouth").GetComponent<tk2dSprite>().SetSprite("Mouth_4");
			MyC2.transform.FindChild("mouth").GetComponent<tk2dSprite>().SetSprite("Mouth_4");
			MyC3.transform.FindChild("mouth").GetComponent<tk2dSprite>().SetSprite("Mouth_4");
			mouthID = 3;
		}
		else{
			MyC.transform.FindChild("mouth").GetComponent<tk2dSprite>().SetSprite("Mouth_1");
			MyC2.transform.FindChild("mouth").GetComponent<tk2dSprite>().SetSprite("Mouth_1");
			MyC3.transform.FindChild("mouth").GetComponent<tk2dSprite>().SetSprite("Mouth_1");
			mouthID = 0;
			Debug.Log("mouth Value : " + mouth.ToString());
		}
	}
	
	void SetMyfin(int fin){
		if(fin == Equipment_.GetFinValue_FromTag("Defualt")){
			MyC.transform.FindChild("fin").GetComponent<tk2dSprite>().SetSprite("Fin_1");
			MyC2.transform.FindChild("fin").GetComponent<tk2dSprite>().SetSprite("Fin_1");
			MyC3.transform.FindChild("fin").GetComponent<tk2dSprite>().SetSprite("Fin_1");
			finID = 0;
		}
		else if(fin == Equipment_.GetFinValue_FromTag("cat")){
			MyC.transform.FindChild("fin").GetComponent<tk2dSprite>().SetSprite("Fin_2");	
			MyC2.transform.FindChild("fin").GetComponent<tk2dSprite>().SetSprite("Fin_2");
			MyC3.transform.FindChild("fin").GetComponent<tk2dSprite>().SetSprite("Fin_2");
			finID = 1;
		}
		else if(fin == Equipment_.GetFinValue_FromTag("round")){
			MyC.transform.FindChild("fin").GetComponent<tk2dSprite>().SetSprite("Fin_3");
			MyC2.transform.FindChild("fin").GetComponent<tk2dSprite>().SetSprite("Fin_3");
			MyC3.transform.FindChild("fin").GetComponent<tk2dSprite>().SetSprite("Fin_3");
			finID = 2;
		}
		else if(fin == Equipment_.GetFinValue_FromTag("sharp")){
			MyC.transform.FindChild("fin").GetComponent<tk2dSprite>().SetSprite("Fin_4");
			MyC2.transform.FindChild("fin").GetComponent<tk2dSprite>().SetSprite("Fin_4");
			MyC3.transform.FindChild("fin").GetComponent<tk2dSprite>().SetSprite("Fin_4");
			finID = 3;
		}
		else{
			MyC.transform.FindChild("fin").GetComponent<tk2dSprite>().SetSprite("Fin_1");
			MyC2.transform.FindChild("fin").GetComponent<tk2dSprite>().SetSprite("Fin_1");
			MyC3.transform.FindChild("fin").GetComponent<tk2dSprite>().SetSprite("Fin_1");
			finID = 0;
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

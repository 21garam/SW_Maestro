using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public static class Util_{
	public static int ConvertStringtoInt(string s) {
		int j;
		bool result = System.Int32.TryParse(s, out j);
		if (true == result) {
			return j;
		} else {
			Debug.Log("Error...");
		    return 0;
		}
	}
}

public class UserRankingInfo{
	public int ranking;
	public string id;
	public int score;
	
	public UserRankingInfo(){
		this.Set(-1, "", 0);
	}
	
	public UserRankingInfo(int ranking, string id, int score){
		this.Set(ranking, id, score);
	}
	
	public void Set(int ranking, string id, int score){
		this.ranking = ranking; this.id = id; this.score = score;
	}
	
	public override string ToString (){
		return string.Format (ranking.ToString() + ". " + id.ToString() + ", " + score.ToString());
	}
}

public class UserAccountInfo{
	public string id;
	public string password;
	public int score;
	public int soft_cash;
	public int hard_cash;
	public int equipment;
	public int item;
	
	public int buying_equipment_eye;
	public int buying_equipment_body;
	public int buying_equipment_mouth;
	public int buying_equipment_fin;
	
	public int hp_lv;
	//public int scale_lv;
	public int speed_lv;
	public int fever_lv;
	
	public string nick_name;
	public int exp_lv;
	public int exp_sub;
	
	public UserAccountInfo(){
		this.Set("", "", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "", 0, 0);
		//this.Set("", "", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "", 0, 0);
	}
	
	public UserAccountInfo(string id, string password, int score, int soft_cash, int hard_cash, int equipment, int item, 
						   int buying_equipment_eye, int buying_equipment_body, int buying_equipment_mouth, int buying_equipment_fin,
						   int hp_lv, int speed_lv, int fever_lv,
						   string nick_name, int exp_lv, int exp_sub){
		this.Set(id, password, score, soft_cash, hard_cash, equipment, item, 
				 buying_equipment_eye, buying_equipment_body, buying_equipment_mouth, buying_equipment_fin, 
				 hp_lv, speed_lv, fever_lv, nick_name, exp_lv, exp_sub);
	}
	
	public void Set(string id, string password, int score, int soft_cash, int hard_cash, int equipment, int item,
				    int buying_equipment_eye, int buying_equipment_body, int buying_equipment_mouth, int buying_equipment_fin,
					int hp_lv, int speed_lv, int fever_lv,
					string nick_name, int exp_lv, int exp_sub){
		this.id = id; this.password = password; 
		this.score = score; this.soft_cash = soft_cash; 
		this.hard_cash = hard_cash; this.equipment = equipment; 
		this.item = item;	
		
		this.buying_equipment_eye = buying_equipment_eye;
		this.buying_equipment_body = buying_equipment_body;
		this.buying_equipment_mouth = buying_equipment_mouth;
		this.buying_equipment_fin = buying_equipment_fin;
		
		this.hp_lv = hp_lv;
		//this.scale_lv = scale_lv;
		this.speed_lv = speed_lv;
		this.fever_lv = fever_lv;
		
		this.nick_name = nick_name;
		this.exp_lv = exp_lv;
		this.exp_sub = exp_sub;
	}
	
	public override string ToString (){
		return string.Format ("id : " + id.ToString() + ", password : " + password.ToString() + ", score : " + score.ToString() + ", soft_cash : " + 
							  soft_cash.ToString() + ", hard_cash : " + hard_cash.ToString() + ", equipment : " + 
							  equipment.ToString() + ", item : " + item.ToString() + ", buying_equipment_eye : " + 
							  buying_equipment_eye.ToString() + ", buying_equipment_body : " + buying_equipment_body.ToString() + ", buying_equipment_mouth : " + buying_equipment_mouth.ToString() + ", buying_equipment_fin : " + buying_equipment_fin.ToString() + ", hp_lv : " + 
							  hp_lv.ToString() + ", speed_lv : " + speed_lv.ToString() + ", fever_lv : " + fever_lv.ToString() + ", nick_name : " + 
							  nick_name + ", exp_lv : " + exp_lv.ToString() + ", exp_sub : " + exp_sub.ToString()); 
	}
}

public class XMLParser_ {
	private static List<UserRankingInfo> otherUserInfoList = new List<UserRankingInfo>();
	private static UserRankingInfo playerRankingInfo = new UserRankingInfo();
	private static UserAccountInfo playerAccountInfo = new UserAccountInfo();
	
	static XMLParser_(){
	}
	
	public static UserAccountInfo PlayerAccountInfo{
		get{return playerAccountInfo;}
	}
	
	public static List<UserRankingInfo> OtherUserRankingInfoList{
		get{return otherUserInfoList;}
	}
	
	public static UserRankingInfo PlayerUserRankingInfo{
		get{return playerRankingInfo;}
	}
	
	public static bool PlayerInfoXMLParse(string xml){
		XmlDocument doc;
		XmlNode root;
		
		doc = new XmlDocument();
		doc.LoadXml(xml); 
		
		root = doc.FirstChild.NextSibling;
		
		if (root.HasChildNodes && root != null) {
			XmlAttribute idAtt = root.ChildNodes[0].Attributes["id"];
			XmlAttribute passwordAtt = root.ChildNodes[0].Attributes["password"];
			XmlAttribute scoreAtt = root.ChildNodes[0].Attributes["score"];
			XmlAttribute soft_cashAtt = root.ChildNodes[0].Attributes["soft_cash"];
			XmlAttribute hard_cashAtt = root.ChildNodes[0].Attributes["hard_cash"];
			XmlAttribute equipmentAtt = root.ChildNodes[0].Attributes["equipment"];
			XmlAttribute itemAtt = root.ChildNodes[0].Attributes["item"];
			
			XmlAttribute buying_equipment_eyeAtt = root.ChildNodes[0].Attributes["buying_equipment_eye"];
			XmlAttribute buying_equipment_bodyAtt = root.ChildNodes[0].Attributes["buying_equipment_body"];
			XmlAttribute buying_equipment_mouthAtt = root.ChildNodes[0].Attributes["buying_equipment_mouth"];
			XmlAttribute buying_equipment_finAtt = root.ChildNodes[0].Attributes["buying_equipment_fin"];
			
			XmlAttribute hp_lvAtt = root.ChildNodes[0].Attributes["hp_lv"];
			//XmlAttribute scale_lvAtt = root.ChildNodes[0].Attributes["scale_lv"];
			XmlAttribute speed_lvAtt = root.ChildNodes[0].Attributes["speed_lv"];
			XmlAttribute fever_lvAtt = root.ChildNodes[0].Attributes["fever_lv"];
			
			XmlAttribute nick_nameAtt = root.ChildNodes[0].Attributes["nick_name"];
			XmlAttribute exp_lvAtt = root.ChildNodes[0].Attributes["exp_lv"];
			XmlAttribute exp_subAtt = root.ChildNodes[0].Attributes["exp_sub"];
			
			playerAccountInfo.Set((string)idAtt.Value, (string)passwordAtt.Value, 
							Util_.ConvertStringtoInt((string)scoreAtt.Value), 
							Util_.ConvertStringtoInt((string)soft_cashAtt.Value), Util_.ConvertStringtoInt((string)hard_cashAtt.Value), 
							Util_.ConvertStringtoInt((string)equipmentAtt.Value), Util_.ConvertStringtoInt((string)itemAtt.Value),
							Util_.ConvertStringtoInt((string)buying_equipment_eyeAtt.Value), Util_.ConvertStringtoInt((string)buying_equipment_bodyAtt.Value),
							Util_.ConvertStringtoInt((string)buying_equipment_mouthAtt.Value), Util_.ConvertStringtoInt((string)buying_equipment_finAtt.Value),
							Util_.ConvertStringtoInt((string)hp_lvAtt.Value), //Util_.ConvertStringtoInt((string)scale_lvAtt.Value),
							Util_.ConvertStringtoInt((string)speed_lvAtt.Value), Util_.ConvertStringtoInt((string)fever_lvAtt.Value),
							(string)nick_nameAtt.Value, Util_.ConvertStringtoInt((string)exp_lvAtt.Value),
							Util_.ConvertStringtoInt((string)exp_subAtt.Value));
			return true;
		}
		return false;
	}
	
	public static bool RankingInfoXMLParse(string xml) {
		XmlDocument doc;
		XmlNode root;
	
		doc = new XmlDocument();
		doc.LoadXml(xml); 
		
		root = doc.FirstChild.NextSibling;
		
		if (root.HasChildNodes && root != null) {
			int userLength = root.ChildNodes.Count;
			otherUserInfoList.Clear();
			for (int i = 0; i < userLength; i++) {
				XmlAttribute rankingAtt = root.ChildNodes[i].Attributes["ranking"];
				XmlAttribute idAtt = root.ChildNodes[i].Attributes["id"];
				XmlAttribute scoreAtt = root.ChildNodes[i].Attributes["score"];
				if(i != userLength-1){
					UserRankingInfo userInfo = new UserRankingInfo(Util_.ConvertStringtoInt((string)rankingAtt.Value), 
													  (string)idAtt.Value, 
													  Util_.ConvertStringtoInt((string)scoreAtt.Value));
					otherUserInfoList.Add(userInfo);
				}
				else{
					playerRankingInfo.Set(Util_.ConvertStringtoInt((string)rankingAtt.Value),
									   (string)idAtt.Value,
									   Util_.ConvertStringtoInt((string)scoreAtt.Value));
				}
			}
			return true;
		}
		return false;
	}
}

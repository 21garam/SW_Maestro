using UnityEngine;
using System.Collections;

public class Login_ : MonoBehaviour {
	public tk2dUITextInput login_email;
	public tk2dUITextInput login_password;
	public tk2dUITextInput create_email;
	public tk2dUITextInput create_password;
	
	public tk2dUIItem loginBtr;
	public tk2dUIItem createBtr;
	public tk2dUIItem backBtr;
	public tk2dUIItem confirmBtr;
	public tk2dUIItem msgBoxBtr;
	public Camera cam;
	public MessageBox_ messgaeBox;
	
	WWW_ www;
	
	void Start () {
		www = GetComponent<WWW_>();
		msgBoxBtr.OnClick += CloseMSGBox;
		TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, true);
	} 
	
    void OnEnable() {
        loginBtr.OnClick += Login;
		confirmBtr.OnClick += CreateAccountConfirm;
		createBtr.OnClick += GoToCreateAccount;
		backBtr.OnClick += GoToBack;
	}
	
	private void CloseMSGBox(){
		messgaeBox.transform.position = new Vector3(-3, 1, -1);
		messgaeBox.active = false;
		enabled = true;
		TextClear();
	}
	
	private void TextClear(){
		login_email.Text = "";
		login_password.Text = "";
		create_email.Text = "";	
		create_password.Text = "";
	}
	
    private void GoToCreateAccount() {
        GoToLocation(cam, new Vector3(3, 1, -10));
	}
	
	private void GoToBack() {
		GoToLocation(cam, new Vector3(0, 1, -10));
	}
	
	private void CreateAccountConfirm() {
		if(!messgaeBox.active)
			www.CreateAccount(create_email.Text, create_password.Text, CreateAccountMessageBox);
	}
	
	public void CreateAccountMessageBox(string text){
		if(text == "ok"){
			messgaeBox.transform.position = new Vector3(3, 1, -1);
			messgaeBox.SetMessage("Success Create Account");
		}
		else{
			messgaeBox.transform.position = new Vector3(3, 1, -1);
			messgaeBox.SetMessage("Fail Create Account");
		}
	}
	
	private void Login() {
		if(!messgaeBox.active)
			www.Login(login_email.Text, login_password.Text, LoadLevel);
	}
	
	public void LoadLevel(string text){
		if(text == "ok"){
			Static_.PLAYER_ID = login_email.Text;
			Application.LoadLevel("Menu");
		}
		else
			LoginFailMessageBox();
	}
	
	private void LoginFailMessageBox(){
		messgaeBox.transform.position = new Vector3(0, 1, -1);
		messgaeBox.SetMessage("Login Fail");
	}
	
	private void GoToLocation(Camera obj, Vector3 loc) {
		Transform t = obj.transform;
    	StartCoroutine(TransformTo(t, 0.3f, loc));
    }
	
	IEnumerator TransformTo( Transform transform, float time, Vector3 toPos) {
        Vector3 fromPos = transform.localPosition;
        for (float t = 0; t < time; t += tk2dUITime.deltaTime) {
            float nt = Mathf.Clamp01( t / time );
            nt = Mathf.Sin(nt * Mathf.PI * 0.5f);

            transform.localPosition = Vector3.Lerp( fromPos, toPos, nt );
            yield return 0;
        }
		transform.localPosition = toPos;
    }
}

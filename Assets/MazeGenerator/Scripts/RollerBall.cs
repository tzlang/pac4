using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//<summary>
//Ball movement controlls and simple third-person-style camera
//</summary>
public class RollerBall : MonoBehaviour
{

    public GameObject ViewCamera = null;
    public AudioClip JumpSound = null;
    public AudioClip HitSound = null;
    public AudioClip CoinSound = null;

    //comment

    public Transform ToFollow;
    public GameObject gameOverScreen;

    private Rigidbody mRigidBody = null;
    private AudioSource mAudioSource = null;
    private bool mFloorTouched = false;




	[SerializeField]
    private Vector3 moveDir;

	[SerializeField]
    private float speed = 1;

    void Start()
	{
    	mRigidBody = GetComponent<Rigidbody>();
    	mAudioSource = GetComponent<AudioSource>();
	}

    //private void Update()
    //{
    //	transform.Translate(moveDir*speed*Time.deltaTime)
    //}

    void FixedUpdate()
	{
        //if (mRigidBody != null) {
        //	if (Input.GetButton ("Horizontal")) {
        //    	mRigidBody.AddTorque(Vector3.back * Input.GetAxis("Horizontal")*10);
        //	}
        //	if (Input.GetButton ("Vertical")) {
        //    	mRigidBody.AddTorque(Vector3.right * Input.GetAxis("Vertical")*10);
        //	}
        //	if (Input.GetButtonDown("Jump")) {
        //    	if(mAudioSource != null && JumpSound != null){
        //        	mAudioSource.PlayOneShot(JumpSound);
        //    	}
        //    	mRigidBody.AddForce(Vector3.up*200);
        //	}
        //}


        //VRInput.Touch.PrimaryTouchpad;
        if (OVRInput.Get(OVRInput.Button.PrimaryTouchpad))
    	{
        	Vector2 touch = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);
        	Vector3 forwardDir = ToFollow.forward.normalized;
            if (touch.y > 0.5f)
        	{

            	transform.Translate(new Vector3(forwardDir.x, 0, forwardDir.z) * speed * Time.deltaTime, Space.World);

        	}
            if (touch.y < -0.5f)
        	{
            	transform.Translate(new Vector3(forwardDir.x, 0, forwardDir.z) * speed * -1 * Time.deltaTime, Space.World);

        	}
    	}



        //transform.Translate(moveDir * speed * Time.deltaTime);
 	   //transform.Translate(new Vector3(0, 0, 1));
        //transform.position.z += 1;

        //   	if (ViewCamera != null) {
        //    	Vector3 direction = (Vector3.up*2+Vector3.back)*2;
        //    	RaycastHit hit;
        //    	Debug.DrawLine(transform.position,transform.position+direction,Color.red);
        //    	if(Physics.Linecast(transform.position,transform.position+direction,out hit)){
        //        	ViewCamera.transform.position = hit.point;
        //    	}else{
        //        	ViewCamera.transform.position = transform.position+direction;
        //    	}
        //    	ViewCamera.transform.LookAt(transform.position);
        //	}
	}

    void OnCollisionEnter(Collision coll)
	{
        if (coll.gameObject.tag == "Floor")
    	{
        	mFloorTouched = true;
            if (mAudioSource != null && HitSound != null && coll.relativeVelocity.y > .5f)
        	{
            	mAudioSource.PlayOneShot(HitSound, coll.relativeVelocity.magnitude);
        	}
    	}
        else
    	{
            if (mAudioSource != null && HitSound != null && coll.relativeVelocity.magnitude > 2f)
        	{
        	    mAudioSource.PlayOneShot(HitSound, coll.relativeVelocity.magnitude);
        	}
    	}
	}

    void OnCollisionExit(Collision coll)
	{
        if (coll.gameObject.tag == "Floor")
    	{
        	mFloorTouched = false;
    	}
	}

    void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.tag == "Coin")
    	{
            if (mAudioSource != null && CoinSound != null)
        	{
            	mAudioSource.PlayOneShot(CoinSound);
        	}
        	Destroy(other.gameObject);
    	}
        if (other.gameObject.tag == "Enemy")
    	{
        	gameOverScreen.SetActive(true);
    	}
	}


}

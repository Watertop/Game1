using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
    public float speed;
    public float scrollingspeed;
    public float specialAttack;

    public float invincibilityTime;
    public float fireRate;

    public float boundaryX;
    public float boundaryY;
    public float MovementX;
    public float MovementY;

    public Transform bulletSpawn1;
    public Transform bulletSpawn2;
    public Transform bulletSpawn3;
    public Transform bulletSpawn4;
    public Transform bulletSpawn5;

    public GameObject SpecialAttackBar;
    public GameObject BombBar;
    public float BombInt;



    public bool justDied;
    public bool isShooting;
    private Transform HitObject;
    public GameObject Laser;

    ObjectPoolerScript objPool;

    public bool isBombing;

    public static PlayerScript Player;

	// Use this for initialization
	void Awake () {
        Player = this;
        if(objPool == null){
            objPool = gameObject.GetComponent<ObjectPoolerScript>();
        }
	}
    void OnDisable(){
        CancelInvoke();
    }
	// Update is called once per frame
	void Update () {
        if(isBombing == true){
            isBombing = false;
        }
        Movement();
        Actions();
        HugeLaser();
	}
    void Movement(){
        transform.Translate(Vector2.right * Time.deltaTime * scrollingspeed);
        //If you press Up, go up, else if down, go down
        if(Input.GetKey(KeyCode.W)||Input.GetKey(KeyCode.UpArrow)){
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }else if(Input.GetKey(KeyCode.S)||Input.GetKey(KeyCode.DownArrow)){
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        }
        //If you press right, go right, else if left, go left
        if(Input.GetKey(KeyCode.D)||Input.GetKey(KeyCode.RightArrow)){
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }else if(Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.LeftArrow)){
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        //Clamp the Values so the player cant move off the screen
        transform.position = new Vector3(Mathf.Clamp(transform.position.x,-boundaryX+Camera.main.transform.position.x,boundaryX+Camera.main.transform.position.x),Mathf.Clamp(transform.position.y,-boundaryY+Camera.main.transform.position.y,boundaryY+Camera.main.transform.position.y),transform.position.z);
    }
    void Actions(){

        if(Input.GetButton("Fire1") && !IsInvoking("ShootPower2")){
            InvokeRepeating("ShootPower2",0,fireRate);
        }
        if(Input.GetButtonUp("Fire1")){
            CancelInvoke("ShootPower2");
        }
    }

    void HugeLaser(){
        RaycastHit2D Hit = Physics2D.Raycast(transform.position + new Vector3(0, 0.25001f, 0), Vector2.up);
        RaycastHit2D HitL = Physics2D.Raycast(transform.position + new Vector3(-0.6f, 0.25001f, 0), Vector2.up);
        RaycastHit2D HitR = Physics2D.Raycast(transform.position + new Vector3(0.6f, 0.25001f, 0), Vector2.up);


        if(Input.GetButton("Fire2") && specialAttack > 0){
            
            specialAttack = Mathf.Clamp(specialAttack - Time.deltaTime * 10,0,100);

            if(Hit){
                Laser.transform.position = new Vector3(transform.position.x, (Hit.transform.position.y + transform.position.y + 0.26f)/2, transform.position.z);
                Laser.transform.localScale = new Vector3(Laser.transform.localScale.x,100 * (Hit.transform.position.y - transform.position.y),Laser.transform.localScale.z);
                Laser.transform.parent.gameObject.SetActive(true);
                Hit.collider.gameObject.GetComponent<HealthScript>().currentHealth -= 30 * Time.deltaTime;
              

            }else if(HitL && HitR){
                if(HitL.distance < HitR.distance){
                    Laser.transform.position = new Vector3(transform.position.x, (HitL.transform.position.y + transform.position.y + 0.26f)/2, transform.position.z);
                    Laser.transform.localScale = new Vector3(Laser.transform.localScale.x,100 * (HitL.transform.position.y - transform.position.y),Laser.transform.localScale.z);
                    Laser.transform.parent.gameObject.SetActive(true);
                    HitL.collider.gameObject.GetComponent<HealthScript>().currentHealth -= 30 * Time.deltaTime;

                }else{
                    Laser.transform.position = new Vector3(transform.position.x, (HitR.transform.position.y + transform.position.y + 0.26f)/2, transform.position.z);
                    Laser.transform.localScale = new Vector3(Laser.transform.localScale.x,100 * (HitR.transform.position.y - transform.position.y),Laser.transform.localScale.z);
                    Laser.transform.parent.gameObject.SetActive(true);
                    HitR.collider.gameObject.GetComponent<HealthScript>().currentHealth -= 30 * Time.deltaTime;
                }
            }
            else if(HitL){
                Laser.transform.position = new Vector3(transform.position.x, (HitL.transform.position.y + transform.position.y + 0.26f)/2, transform.position.z);
                Laser.transform.localScale = new Vector3(Laser.transform.localScale.x,100 * (HitL.transform.position.y - transform.position.y),Laser.transform.localScale.z);
                Laser.transform.parent.gameObject.SetActive(true);
                HitL.collider.gameObject.GetComponent<HealthScript>().currentHealth -= 30 * Time.deltaTime;

            }else if(HitR){
                Laser.transform.position = new Vector3(transform.position.x, (HitR.transform.position.y + transform.position.y + 0.26f)/2, transform.position.z);
                Laser.transform.localScale = new Vector3(Laser.transform.localScale.x,100 * (HitR.transform.position.y - transform.position.y),Laser.transform.localScale.z);
                Laser.transform.parent.gameObject.SetActive(true);
                HitR.collider.gameObject.GetComponent<HealthScript>().currentHealth -= 30 * Time.deltaTime;
            }
            else{
                Laser.transform.position = new Vector3(transform.position.x, (5.1f + transform.position.y + 0.26f)/2, transform.position.z);
                Laser.transform.localScale = new Vector3(Laser.transform.localScale.x,100 * (5.1f - transform.position.y),Laser.transform.localScale.z);
                Laser.transform.parent.gameObject.SetActive(true);

            }


        }else{
            Laser.transform.parent.gameObject.SetActive(false);
            //specialAttack = Mathf.Clamp(specialAttack + Time.deltaTime * 10,0,100);
        }
        float specialBar = specialAttack / 100;
        SpecialAttackBar.transform.localScale = new Vector3 (specialBar , SpecialAttackBar.transform.localScale.y, SpecialAttackBar.transform.localScale.z);
    }

   
    void ShootPower(){
        //Shoots 5 bullets 
        GameObject obj1 = objPool.GetPooledObject();
        if (obj1 == null)return;
        obj1.transform.position = bulletSpawn1.position;
        obj1.GetComponent<BulletScript>().Direction = new Vector2(1,0);
        obj1.SetActive(true);

        GameObject obj2 = objPool.GetPooledObject();
        if (obj2 == null)return;
        obj2.transform.position = bulletSpawn2.position;
        obj2.GetComponent<BulletScript>().Direction = new Vector2(1,0);
        obj2.SetActive(true);

        GameObject obj3 = objPool.GetPooledObject();
        if (obj3 == null)return;
        obj3.transform.position = bulletSpawn3.position;
        obj3.GetComponent<BulletScript>().Direction = new Vector2(1,0);
        obj3.SetActive(true);

        GameObject obj4 = objPool.GetPooledObject();
        if (obj4 == null)return;
        obj4.transform.position = bulletSpawn4.position;
        obj4.GetComponent<BulletScript>().Direction = new Vector2(1,0);
        obj4.SetActive(true);

        GameObject obj5 = objPool.GetPooledObject();
        if (obj5 == null)return;
        obj5.transform.position = bulletSpawn5.position;
        obj5.GetComponent<BulletScript>().Direction = new Vector2(1,0);
        obj5.SetActive(true);
    }

    void ShootPower2(){
        //Shoots 5 bullets 
//        GameObject obj1 = objPool.GetPooledObject();
//        if (obj1 == null)return;
//        obj1.transform.position = bulletSpawn1.position;
//        obj1.GetComponent<BulletScript>().Direction = new Vector2(1,0.25f);
//        obj1.SetActive(true);

        GameObject obj2 = objPool.GetPooledObject();
        if (obj2 == null)return;
        obj2.transform.position = bulletSpawn2.position ;
        obj2.GetComponent<BulletScript>().Direction = new Vector2(1,0.125f);
        obj2.SetActive(true);

        GameObject obj3 = objPool.GetPooledObject();
        if (obj3 == null)return;
        obj3.transform.position = bulletSpawn3.position;
        obj3.GetComponent<BulletScript>().Direction = new Vector2(1,0);
        obj3.SetActive(true);

        GameObject obj4 = objPool.GetPooledObject();
        if (obj4 == null)return;
        obj4.transform.position = bulletSpawn4.position ;
        obj4.GetComponent<BulletScript>().Direction = new Vector2(1,-0.125f);
        obj4.SetActive(true);

//        GameObject obj5 = objPool.GetPooledObject();
//        if (obj5 == null)return;
//        obj5.transform.position = bulletSpawn5.position;
//        obj5.GetComponent<BulletScript>().Direction = new Vector2(1,-0.25f);
//        obj5.SetActive(true);
    }



    void Invincibility(){
        float timer = 0;

        if(justDied && timer != invincibilityTime){
            timer = invincibilityTime;
            justDied = false;
        }
        if(timer>=0){
            gameObject.GetComponent<HealthScript>().isInvincible = true;
            timer -= Time.deltaTime;
        }else{
            gameObject.GetComponent<HealthScript>().isInvincible = false;
        }
    }
}

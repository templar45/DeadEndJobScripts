using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Author:R. Casiano
//Modified by:Christopher Cruz
//controls shoot functionality for both AR and Handgun
// Added Dialogue script to reload
public class ShootingScript : MonoBehaviour
{
    //Serialized used to insert flash
    [SerializeField] MuzzleFlash _muzzleFlash = null;
    [SerializeField] Color _newColor = Color.white;

    //Modified by : Christopher Cruz
    public Camera cameraAtUse;
    //Variables used for enemy count.
    private int enemyCount;
    public GameObject[] enemies;
    public bool noEnemies;
    //Variables used for reload and ammo counter
    public int currentRounds;
    public int maxRounds = 9;
    public float reloadTime = 1f;
    public bool isReloading = false;
    public Button reloadBtn;
    public LayerMask ignoreCollider;
    //Variables used for muzzleFlash
    public GameObject muzzleFlash;
    public AudioSource gunSound;
    public Transform flashLocation;
    //used for spherecast
    public float range = 200f;
    private float radius = 0.1f;
    //used for player health
    public bool hasHealed;
    //Used for Assault Rifle
    public int currentARrounds;
    public int maxARrounds;
    public bool arFiring;
    private bool KeepFiring;
    //Audio
    public AudioSource reloadSound;
    public AudioClip reload1;
    public AudioClip reload2;
    public AudioClip reload3;
    //Shotgun Variables
    private bool hasFiredSG;
    private int pelletsPerShot = 6;
    public int currentSGshells;
    public int maxSGshells;
    private float sgSpread = 7f;
    private float sgRange = 35f;

    // Player Starts with 10 rounds per reload/magazine and AR with zero
    void Start()
    {
        currentRounds = maxRounds;
        maxARrounds = 60;
        maxSGshells = 12;
        currentARrounds = 45;
        currentSGshells = 8;

    }
    //Modified: Chris
    //keep track of how many enemies are in the scene in the update and if the player is firing AR
    void Update()
    {
        //checks condition and bool if player is using AR button
        if(arFiring == true && KeepFiring == true)
        {
            StartCoroutine(RateOfFire());
        }
        enemies =  GameObject.FindGameObjectsWithTag("Enemy");
        if(enemies.Length == 0)
        {
            noEnemies = true;
        }
        if(enemies.Length > 0)
        {
            noEnemies = false;
        }
    }
    //Modified: Chris
    public void Shoot()
    {
        Vector3 direction = cameraAtUse.transform.forward;
        RaycastHit Hit;

        //if loop that checks players ammo count when shoot shooting and automatically starts reloading when gun is empty
        if(isReloading)
        {
            Debug.Log("No, Still Reloading");
            return;
        }
        if(currentRounds <= 0)
        {
            StartCoroutine(ReloadGun());
            return;
        }
        //Rounds is removed everytime player hits shoot
        currentRounds--;
        //screen flashes when players shoot and also calls the muzzleflash script
        _muzzleFlash.StartFlash(.25f, .5f, _newColor);
        //ammo counter
        Debug.Log("Fired! Ammo left" +currentRounds);
        //muzzle flash instantiate on reticle and on the muzzleflash prefab a script is attached that destroys it after .5 seconds)
        Instantiate(muzzleFlash , flashLocation.position, flashLocation.rotation);
        //Need audio clips to play gunfire
        gunSound.Play();

        //Cast Ray from camera camera
        if(Physics.SphereCast(cameraAtUse.transform.position,radius,direction,out Hit, range))
        {
            print ("Objected: " + Hit.transform.gameObject.name);

            // Hit = Blue Line
            Debug.DrawLine(cameraAtUse.transform.position, Hit.point, Color.blue, 0.5f);
           
            if(Hit.collider.gameObject.tag == "Enemy")
            {
                //subtracts from the array everytime an enemy is destroyed
                enemyCount -= enemies.Length;
                // A Hit destroys the object
                Destroy(Hit.collider.gameObject);
                // Replace with correct damage amount
            }
            //if players shoots healthpack they recieve health
            if(Hit.collider.gameObject.tag == "Health")
            {
                Debug.Log("Health retrieved");
                hasHealed = true;
                Destroy(Hit.collider.gameObject);
            }
            //if player shoot ammo pack they recieve ammo
            if(Hit.collider.gameObject.tag == "Ammo")
            {
                Debug.Log("ammo recieved");
                currentARrounds += 25;
                currentSGshells += 4;
                if(currentARrounds >= maxARrounds)
                {
                    currentARrounds = maxARrounds;
                }
                if(currentSGshells >= maxSGshells)
                {
                    currentSGshells = maxSGshells;
                }
                Destroy(Hit.collider.gameObject);
            }
        }
    }
    //Counts down how many seconds to reload then resets the ammo counter back to full
    public IEnumerator ReloadGun()
    {
        isReloading = true;
        Debug.Log("Reloading");
        reloadBtn.interactable = false;

        // Audio Manager for reloading
        float rand = Random.value;
        if (rand >= 0.25f && rand <= 0.5f)
        {
            reloadSound.clip = reload1;
        }
        else if (rand >= 0.5f)
        {
            reloadSound.clip = reload2;
        }
        else 
        {
            reloadSound.clip = reload3;
        }
        
        reloadSound.Play();

        yield return new WaitForSeconds(reloadTime);

        currentRounds = maxRounds;
        isReloading = false;
        reloadBtn.interactable = true;
    }
    //Function of the AR when shootiing: muzzle flash, damage, checks ar ammo, fires spherecast
    public void ShootAr()
    {
        Vector3 direction = cameraAtUse.transform.forward;

        RaycastHit Hit;

        if(currentARrounds <= 0)
        {
            Debug.Log("No AR ammo");
            return;
        }

        _muzzleFlash.StartFlash(.25f, .5f, _newColor);
        Instantiate(muzzleFlash , flashLocation.position, flashLocation.rotation);
        currentARrounds--;
        Debug.Log("Fired AR! Ammo left" +currentARrounds);

        if(Physics.SphereCast(cameraAtUse.transform.position,radius,direction,out Hit, range))
        {
            print ("Objected: " + Hit.transform.gameObject.name);
            //red gizmo for ar
            Debug.DrawLine(cameraAtUse.transform.position, Hit.point, Color.red, 0.5f);
            //same for shoot that if the spherecast hits the following tags they do the following
            if(Hit.collider.gameObject.tag == "Enemy")
            {
                enemyCount -= enemies.Length;
                Destroy(Hit.collider.gameObject);
            }
            if(Hit.collider.gameObject.tag == "Health")
            {
                Debug.Log("Health retrieved");
                hasHealed = true;
                Destroy(Hit.collider.gameObject);
            }
            if(Hit.collider.gameObject.tag == "Ammo")
            {
                Debug.Log("ammo recieved");
                currentARrounds += 25;
                currentSGshells += 4;
                if(currentARrounds >= maxARrounds)
                {
                    currentARrounds = maxARrounds;
                }
                if(currentSGshells >= maxSGshells)
                {
                    currentSGshells = maxSGshells;
                }
                Destroy(Hit.collider.gameObject);
            }
        }

    }
    public void shootSg()
    {
        Vector3 direction = cameraAtUse.transform.forward;
        RaycastHit Hit;

        if(currentSGshells <= 0)
        {
            Debug.Log("No SG ammo");
            return;
        }
        if(hasFiredSG)
        {
            Debug.Log("rate of fire in affect");
            return;
        }

        _muzzleFlash.StartFlash(.25f, .5f, _newColor);
        currentSGshells--;
        Debug.Log("Fired! Ammo left" +currentSGshells);
        Instantiate(muzzleFlash , flashLocation.position, flashLocation.rotation);
        
        //Need audio clips to play gunfire


        for(int i = 0; i < pelletsPerShot; i++ )
        {
            if(Physics.SphereCast(cameraAtUse.transform.position,radius,getShotgunSpread(),out Hit, sgRange))
            {
                print ("Objected: " + Hit.transform.gameObject.name);
                //Green gizmo for SG
                Debug.DrawLine(cameraAtUse.transform.position, Hit.point, Color.green, 0.5f);
                //same for shoot that if the spherecast hits the following tags they do the following
                if(Hit.collider.gameObject.tag == "Enemy")
                {
                    enemyCount -= enemies.Length;
                    Destroy(Hit.collider.gameObject);
                }
                if(Hit.collider.gameObject.tag == "Health")
                {
                    Debug.Log("Health retrieved");
                    hasHealed = true;
                    Destroy(Hit.collider.gameObject);
                }
                if(Hit.collider.gameObject.tag == "Ammo")
                {
                    Debug.Log("ammo recieved");
                    currentARrounds += 25;
                    currentSGshells += 4;
                    if(currentARrounds >= maxARrounds)
                    {
                        currentARrounds = maxARrounds;
                    }
                    if(currentSGshells >= maxSGshells)
                    {
                        currentSGshells = maxSGshells;
                    }
                    Destroy(Hit.collider.gameObject);
                }
            }
        }
        StartCoroutine(SGRateOfFire());
    }
    //UI button for players to be able to manually reload
    public void reloadButton()
    {
        StartCoroutine(ReloadGun());
    }
    //UI Buttons/Code for For AR when held down and released
    public void PointUp()
    {
        arFiring = false;
        KeepFiring = false;
    }
    public void PointDown()
    {
        arFiring = true;
        KeepFiring = true;
    }
    //enum that controls rate of fire
    public IEnumerator RateOfFire()
    {
        ShootAr();
        KeepFiring = false;
        Debug.Log("waiting for Rate of Fire timer");
        yield return new WaitForSeconds(.15f);
        KeepFiring = true;
    }

    Vector3 getShotgunSpread()
    {
        Vector3 targetPos = cameraAtUse.transform.position + cameraAtUse.transform.forward * sgRange;

        targetPos = new Vector3(targetPos.x + Random.Range(-sgSpread, sgSpread), targetPos.y + Random.Range(-sgSpread, sgSpread), targetPos.z + Random.Range(-sgSpread, sgSpread));

        Vector3 direction = targetPos - cameraAtUse.transform.position;
        return direction.normalized;
    }

    public IEnumerator SGRateOfFire()
    {
        hasFiredSG = true;
        yield return new WaitForSeconds(.7f);
        hasFiredSG = false;
    }
}


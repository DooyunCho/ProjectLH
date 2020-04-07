using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WorldInteraction : MonoBehaviour {
    bool MouseMode = true;

    // only mouse
    Vector3 forward, right;
    public float angularSpeed;

    UnityEngine.AI.NavMeshAgent playerAgent;
    private GameObject interactedObject;
    private bool isPressed = false;
    private float pressedTime;

    public ParticleSystem particalSystem;
    public Transform interactableName;
    private Skill readySkill;
    private SkillController skillController;

    public TrackingAction trackingAction;
    public ClickAction clickAction;

    // Use this for initialization
    void Start ()
    {
        skillController = GetComponent<SkillController>();

        Instantiate(interactableName, new Vector3(), Quaternion.identity);
        interactableName.gameObject.SetActive(false);

        // MouseMode
        playerAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        angularSpeed = playerAgent.angularSpeed;

        trackingAction = new TrackingAction(this, playerAgent);

        // KeyboardMode
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 5f, Color.red);

        if (MouseMode)
        {
            // is stop?
            if (AnimationController.Instance.isTrigger("isRun") &&
                (Vector3.Distance(transform.position, playerAgent.destination) <= 0.5f ||
                playerAgent.remainingDistance <= playerAgent.stoppingDistance))
            {
                AnimationController.Instance.setTrigger("isRun", false);
            }

            Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit interactionInfo;

            // print to interactable name.
            if (Physics.Raycast(interactionRay, out interactionInfo, Mathf.Infinity))
            {
                playerAgent.updateRotation = true;
                GameObject targetObject = interactionInfo.collider.gameObject;

                if (targetObject.GetComponent<Interactable>() != null)
                {
                    Vector3 position = targetObject.transform.position;
                    position.y += (targetObject.GetComponent<BoxCollider>().transform.position.y * 0.5f);
                    interactableName.transform.position = position;
                    interactableName.GetComponent<TextMesh>().text = targetObject.GetComponent<Interactable>().myName;
                    interactableName.gameObject.SetActive(true);
                    
                    // 클릭시 InteractedObject 지정
                    if (Input.GetMouseButtonDown(0))
                    {
                        interactedObject = targetObject;
                    }
                }
                else
                {
                    interactableName.gameObject.SetActive(false);
                }
            }

            /**************** 스킬 사용 *****************/
            if (readySkill != null)
            {
                trackingAction.Tracking(Input.mousePosition);

                if (Input.GetMouseButtonUp(0))
                {
                    Debug.Log("Fire1");
                    StartCoroutine(skillController.Fire(readySkill, interactedObject));
                    trackingAction.FinishTracking();
                    readySkill = null;
                }
                else if (Input.GetMouseButtonDown(0))
                {
                    //Debug.Log("GetMouseButtonDown");
                }
                else if (Input.GetMouseButton(0))
                {
                    //Debug.Log("GetMouseButton");
                }

                return;
            }

            /**************** 우 클릭 *****************/
            if (Input.GetMouseButtonDown(1)) // 마우스 트래킹 시작
            {
                trackingAction.StartTracking();
                return;
            }
            else if (Input.GetMouseButtonUp(1)) // 마우스 트래킹 해제
            {
                trackingAction.FinishTracking();
                return;
            }
            else if (Input.GetMouseButton(1))
            {
                trackingAction.Tracking(Input.mousePosition);

                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<PlayerWeaponController>().PerformWeaponAttack();
                }
            }

            /**************** 좌 클릭 *****************/
            if (Input.GetMouseButtonDown(0))
            {
                // UI 클릭 제외
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    return;
                }
                
                pressedTime = Time.time;
                
                if (trackingAction.isTracking)
                {
                    if (readySkill != null)
                    {
                        readySkill = null;
                    }
                    /*if (readySkill != null)
                    {
                        Debug.Log("Fire2");
                        StartCoroutine(skillController.Fire(readySkill, Input.mousePosition));
                        readySkill = null;
                        playerAgent.angularSpeed = angularSpeed;
                        playerAgent.updateRotation = true;
                        return;
                    }
                    else
                    {
                        GetComponent<PlayerWeaponController>().PerformWeaponAttack();
                        return;
                    }*/
                }

                GetInteraction(Input.mousePosition);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                //Debug.Log("GetMouseButtonUp");
                return;
            }
            else if (Input.GetMouseButton(0))
            {
                // UI 클릭 제외
                if (EventSystem.current.IsPointerOverGameObject()) return;

                float currentTime = Time.time;

                if (pressedTime + 0.15f > currentTime)
                    return;
                
                pressedTime = currentTime;
                
                if (interactedObject != null)
                {
                    // TODO
                    // Keep Target
                    if (interactedObject.GetComponent<Interactable>() != null)
                    {
                        GetInteraction(interactedObject);
                    }
                    // Keep Moving
                    else
                    {
                        GetInteraction(Input.mousePosition);
                    }
                }

                //Debug.Log("GetMouseButton");
            }
        }
        else
        {
            if (Input.anyKey && !EventSystem.current.IsPointerOverGameObject())
            {
                Vector3 RightMovement = right * playerAgent.speed * Time.smoothDeltaTime * Input.GetAxis("Horizontal");
                Vector3 ForwardMovement = forward * playerAgent.speed * Time.smoothDeltaTime * Input.GetAxis("Vertical");
                Vector3 FinalMovement = ForwardMovement + RightMovement;
                Vector3 Direction = Vector3.Normalize(FinalMovement);

                if (Direction != Vector3.zero)
                {
                    transform.position += FinalMovement;
                }
            }

            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                GetComponent<PlayerWeaponController>().PerformWeaponAttack();
            }
            else if (Input.GetMouseButtonDown(1) && !EventSystem.current.IsPointerOverGameObject())
            {
                GetInteraction(Input.mousePosition);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("1");
            readySkill = skillController.GetSkill("fireball");
            skillController.PrintRange(readySkill);
            trackingAction.StartTracking();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            readySkill = skillController.GetSkill("energybolt");
            Debug.Log("2");
            skillController.PrintRange(readySkill);
            trackingAction.StartTracking();
        }
    }

    void CancelInteraction()
    {
        if (interactedObject != null)
        {
            if (interactedObject.GetComponent<Interactable>() != null)
            {
                interactedObject.GetComponent<Interactable>().CancelInteract();
            }
        }
    }

    void GetInteraction(GameObject target)
    {
        playerAgent.updateRotation = true;

        if (target.tag == "Enemy")
        {
            GetComponent<PlayerWeaponController>().PerformWeaponAttack(target);
            //target.GetComponent<Interactable>().MoveToInteraction(playerAgent);
        }
        else if (target.tag == "Interactable Object")
        {
            target.GetComponent<Interactable>().MoveToInteraction(playerAgent);
        }
    }

    void GetInteraction(Vector3 input)
    {
        CancelInteraction();
        Ray interactionRay = Camera.main.ScreenPointToRay(input);
        RaycastHit interactionInfo;

        if (Physics.Raycast(interactionRay, out interactionInfo, Mathf.Infinity, LayerMask.NameToLayer("Unclickable")))
        {
            playerAgent.updateRotation = true;
            interactedObject = interactionInfo.collider.gameObject;
            

            if (interactedObject.tag == "Enemy")
            {
                GetComponent<PlayerWeaponController>().PerformWeaponAttack(interactedObject);
            }
            else if (interactedObject.tag == "Interactable Object")
            {
                Debug.Log("MoveToInteraction");
                interactedObject.GetComponent<Interactable>().MoveToInteraction(playerAgent);
            }
            else
            {
                Instantiate(particalSystem, interactionInfo.point, Quaternion.identity);
                playerAgent.stoppingDistance = 0f;
                playerAgent.destination = interactionInfo.point;
                AnimationController.Instance.setTrigger("isRun", true);
            }
        }
    }

    public void SetMouseMode(bool value)
    {
        Debug.Log("SetMouseMode:" + value);
        MouseMode = value;
        playerAgent.updatePosition = value;
        playerAgent.updateRotation = value;
    }

    public class TrackingAction : MonoBehaviour
    {
        UnityEngine.AI.NavMeshAgent playerAgent;
        MonoBehaviour player;
        float originRotateSpeed;
        public bool isTracking { get; set; }

        public TrackingAction(MonoBehaviour player, UnityEngine.AI.NavMeshAgent agent)
        {
            this.player = player;
            this.playerAgent = agent;
            originRotateSpeed = agent.angularSpeed;
        }

        public void StartTracking()
        {
            playerAgent.angularSpeed = 0;
            playerAgent.updateRotation = true;
            isTracking = true;
        }

        public void Tracking(Vector3 position)
        {
            Plane playerPlane = new Plane(Vector3.up, player.transform.position);
            Ray ray = Camera.main.ScreenPointToRay(position);

            float hitdist = 0.0f;
            if (playerPlane.Raycast(ray, out hitdist) && !EventSystem.current.IsPointerOverGameObject())
            {
                Vector3 targetPoint = ray.GetPoint(hitdist);
                Quaternion targetRotation = Quaternion.LookRotation(targetPoint - player.transform.position);
                player.transform.rotation = Quaternion.Slerp(player.transform.rotation, targetRotation, 5f * Time.deltaTime);
            }

        }

        public void FinishTracking()
        {
            playerAgent.angularSpeed = originRotateSpeed;
            playerAgent.updateRotation = false;
            isTracking = false;
        }
    }

    public class ClickAction : MonoBehaviour
    {
        void Click(Vector3 position)
        {
        }
    }
}

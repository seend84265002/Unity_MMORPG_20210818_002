using UnityEngine;          //�ޥ� Unity API  (�ܮw�A��ƻP�\��)
using UnityEngine.Video;    //�ޥ� �v�� API    

namespace Win { 



    //�׹��� ���O ���O�W�� : �~�����O 
    // MonoBehaviour Unity �����O :�n���b����W�@�w�n�~��
    // �~�ӫ�|�ɦ������O������
    // �b���O�H�Φ����W��K�[�T���׽u�|�K�[�K�n
    // �`�Φ���:��� Frid �B�ݩ� Property(�ܼ�)�B ��k Method �B �ƥ� Event 
    /// <summary>
    /// Wen 2021.09.06
    /// �ĤT�H�ٱ��
    /// ���ʡA���D 
    /// </summary>
    public class ThirdPersonController : MonoBehaviour
    {
        #region ��� Freld

        //�x�s�C����ơA�Ҧp:���ʳt�סA���D����..
        //�`�Υ|�j����:��ơA�B�I�ơA�r��A���L��
        //���y�k:�׹����A��������A���W��(���w �w�]��) ����
        //�׹���:
        //1.���} public  ���\��L���O�s���A��ܦb�ݩʭ��O�A�ݭn�վ㪺��Ƴ]�w�����}
        //2.�p�H private  �T���L���O�s���A���æb�ݩʭ��O�A �w�]��
        //   Unity �H�ݩʭ��O���D
        //   ��_�{���w�]�ȽЫ�....>Reset
        //  ����ݩ� Attribute :���U�����
        //  ����ݩʻy�k : [�ݩʦW��(�ݩʭ�)]
        //  Header ���D
        //  Tooltip ����:�ƹ����d�b���W�٤W�|��ܪ�����
        //  Range �d��:�i�H�ϥΦb�ƭ�������ƤW�A�Ҧp: int , float
        [Header("���ʳt��"), Tooltip("�Ψӽվ㨤�Ⲿ�ʳt��"), Range(0, 500)]
        public float speed = 10.5f;
        [Header("���D����"), Tooltip("�Ψӽվ㨤����D����"), Range(0, 1000)]
        public int jump = 100;

        [Header("�ˬd�a�O���")]
        [Tooltip("�ˬd����O�_�b�a�O�W��")]
        public bool v3floor;
        public Vector3 v3checkGroundoffect;
        [Range(0, 3)]
        public float v3radius = 0.2f;

        [Header("�����ɮ�")]
        public AudioClip Audiojump;
        public AudioClip Audiofloor;

        [Header("�ʵe�Ѽ�")]
        public string aniWalk = "�����}��";
        public string aniRun = "�]�B�}��";
        public string aniHurt = "����Ĳ�o";
        public string aniDie = "���`Ĳ�o";
        public string aniJump = "��ģĲ�o";
        public string aniIsFloor = "�O�_�b�a�O�W";

        public GameObject playerObject;

        private ThridPersonCamera thridPersonCamera;
        private AudioSource aud;
        private Rigidbody rig;
        private Animator ani;

        [Header("���V�t��"), Range(0, 50)]
        public float speedLookAt = 2;




        #endregion

        #region �ݩ� Property 
        /// <summary>
        /// ��ģ
        /// </summary>
        /// <paran mane="keyJump">��ģ����</paran>
        private bool keyJump { get => Input.GetKeyDown(KeyCode.Space); }
        /// <summary>
        /// �H�����ĭ��q
        /// </summary>
        /// <paran name="volumeRandom">���ĭ��q</paran>
        private float volumeRandom { get => Random.Range(0.7f, 1.2f); }
    #endregion

    #region ��k Method
    //�w�q�P��@ �������{�����϶��A�\��
    //��k�y�k : �׹��� �Ǧ^������� ��k�W�� (�Ѽ�1,...�Ѽ�N){ �{���϶� }
    //�`�ζǦ^���� : �L�Ǧ^ void - ����k�S���Ǧ^���
    //�榡�� �ƪ� ctrl + K + D
    //�P�| ctrl + M O
    //�i�} ctrl + M L
    //�W���C�⬰�H���� - �S���Q�I�s
    //�W���C�⬰���� - ���Q�I�s

    /// <summary>
    /// ����
    /// </summary>
    /// <param name="speedmove">���ʳt��</param>    
    private void Move(float speedmove)
        {
            //�Ш��� Animator �ݩ� Apply Root Motion :�Ŀ�ɨϥΰʵe�첾��T
            //����.�[�t�� = �T���V�q�A�[�t�ץΨӱ������ �T�Ӷb�V���B�ʳt��
            //�e��*��J�� =���ʳt��
            //�ϥΫe�ᥪ�k�b�V�B�ʨëO���쥻���a�ߤޤO
            rig.velocity = transform.forward * MoveInput("Vertical") * speedmove +
                           transform.right * MoveInput("Horizontal") * speedmove +
                           Vector3.up * rig.velocity.y;


        }
        /// <summary>
        /// ���ʫ����J
        /// </summary>
        /// <param name="axisName">�n�����b�V�W��</param>
        /// <returns>�^�ǭȯB�I��0</returns>
        private float MoveInput(string axisName)
        {
            return Input.GetAxis(axisName);
        }
        /// <summary>
        /// �ˬd�a�O
        /// </summary>
        /// <returns>�^�ǥ��L��false</returns>
        private bool CheckGround()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position +
                transform.right * v3checkGroundoffect.x +
                transform.up * v3checkGroundoffect.y +
                transform.forward * v3checkGroundoffect.z
                , v3radius, 1 << 3); //�]���O3�ҥH�g 1<<3   �H������  10�N�O 1<<10
                                     //print("�y��I��Ĥ@�Ӫ��� : " + hits[0].name);
                                     // �Ǧ^ �I���}�C�ƶq > 0 �A�u�n�I����w�ϼh�A����N�N��b�a���W 
            if (!v3floor && hits.Length > 0) aud.PlayOneShot(Audiofloor, volumeRandom);
            v3floor = hits.Length > 0;
            return hits.Length > 0;
        }
        /// <summary>
        /// ���D
        /// </summary>
        private void Jump()
        {
            //print("�y��I��Ĥ@�Ӫ��� : " + CheckGround());
            //�åB &&
            //�p�G �b�a���W �åB���U�ť��� �N��ģ
            //if (CheckGround() && Input.GetKeyDown(KeyCode.Space))
            if (CheckGround() && keyJump)
            {
                //print("���_��");
                rig.AddForce(transform.up * jump);

                aud.PlayOneShot( Audiojump, volumeRandom);
            }
        }
        /// <summary>
        /// ��s�ʵe
        /// </summary>
        private void UpdateAnimation()
        {
            ani.SetBool(aniWalk, MoveInput("Vertical") != 0 ||
                                 MoveInput("Horizontal") != 0);
            ani.SetBool(aniIsFloor, v3floor);
            if (keyJump) ani.SetTrigger(aniJump);
            //ani.SetBool(aniWalk, true);
        }


        private void LookAtForward()
        {
            if (Mathf.Abs(MoveInput("Vertical")) > 0.1f)
            {//���o�e�訤�� = �|�� .���ۨ���(�e��y��-�����y��)
                Quaternion angle = Quaternion.LookRotation(thridPersonCamera.posForawrd - transform.position);
                //�����󪺨��� = �|�� .�t��
                transform.rotation = Quaternion.Lerp(transform.rotation, angle, Time.deltaTime*speedLookAt);

            }
        }
    
      
        #endregion


        #region �ƥ� Event 
        // �S�w�ɶ��I�|���檺��k�A�{�����J�f Start ���� Console Main
        // �}�l�ƥ�:�C���}�l����@���A�B�z��l�ơA�������..����
        private void Start()
        {
            //1.�������W�١A���o����(����(��������)) ��@ ��������;
            aud = playerObject.GetComponent(typeof(AudioSource)) as AudioSource;
            //2.���}���C������A���o���<�x��>();
            rig = gameObject.GetComponent<Rigidbody>();
            //3.���o����<�x��>();
            //���O�i�H�ϥ��~�����O(���˧O)�������A���}�ΫO�@
            ani = GetComponent<Animator>();
            //��v�����O= �z�L�����M�䪫��<�x��>();
            //FindObjectOfType ���n��b Updata ���ϥη|�y���j�q�į�t��
            thridPersonCamera = FindObjectOfType<ThridPersonCamera>();
        }



        //��s�ƥ�:�@������� 60 �� �A60FPS -Frame Per Secound �A�ΨӳB�z�D�ʹB�ʡA���ʪ���A��ť���a��J����C
        private void Update()
        {   

            Jump();
            UpdateAnimation();
            LookAtForward();
            //Move(speed);
        }



        // �T�w��s�ƥ� : �T�w 0.02 �����@��
        //�ΨӳB�z���z�欰: �Ҧp Rigidbody API
        private void FixedUpdate()
        {
            Move(speed);
        }
        private void OnDrawGizmos()
        {
            //1.���w �C��
            //2.ø�s�ϧ�

            Gizmos.color = new Color(1, 0, 0.2f, 0.3f);
            // transform �P���}���b�P���h�� transform ����
            Gizmos.DrawSphere(transform.position +
                transform.right * v3checkGroundoffect.x +
                transform.up * v3checkGroundoffect.y +
                transform.forward * v3checkGroundoffect.z
                , v3radius);   //Gizmos.DrawSphere(�y�� x y z �A�b�|)
        }
        #endregion



    }
}

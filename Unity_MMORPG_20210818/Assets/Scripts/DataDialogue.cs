using UnityEngine;
namespace Wen.Dialogue
{
    /// <summary>
    /// ��ܨt�θ��
    /// NPC��ܭn���T���q
    /// �����ȡA���Ȥ��A��������
    /// </summary>
    // ScriptableObject �~�Ӧ����O�|�ܦ��}���ƪ���
    // �i�N���}����Ʒ�����O�s�b�M�� Project��
    // CreatAssetMenu ���O�ݩ�:�������O�إߪ��M�פ����
    // menuName ���W�١A�i��/���h
    // fileName �ɮצW��
    [CreateAssetMenu(menuName = "Wen/��ܸ��", fileName = "NPC ��ܸ��")]
    public class DataDialogue : ScriptableObject
    {
        //TextArea �r����ݩ� �A�i�]�w���
        [Header("���ȫe��ܤ��e"), TextArea(2, 7)]
        public string[] beforeMission;
        [Header("���ȶi�椤��ܤ��e"), TextArea(2, 7)]
        public string[] missionning;
        [Header("���ȧ�����ܤ��e"), TextArea(2, 7)]
        public string[] afterMission;
        [Header("���ȻݨD�ƶq"), Range(0,100)]
        public int countNeed;

        //�ϥΦC�|
        //�y�k: �׹��� �C�|�W�� �۩w�q���W��
        [Header("NPC ���Ȫ��A"), Range(0, 100)]
        public StateNPCMission stateNPCMission = StateNPCMission.BeforMission ;

    }

}


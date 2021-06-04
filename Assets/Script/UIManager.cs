using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    //txtScore�Q�[���I�u�W�F�N�g�̎���Text�R���|�[�l���g���C���X�y�N�^�[����A�T�C������
    private Text txtScore;

   ///<summary>
   ///�X�R�A�\�����X�V
   /// </summary>
   ///<param name="score"></param>param>
   public void UpdateDisplayScore(int score)
    {
        txtScore.text = score.ToString();
    }
}

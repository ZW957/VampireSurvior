using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Weapon {
    [DefaultExecutionOrder(200)]
    /// <summary>
    /// �ޯ�t��
    /// </summary>
    public class SkillSystem : MonoBehaviour
    {
        private Button[] btnSkills = new Button[3];
        private TextMeshProUGUI[] textTitles = new TextMeshProUGUI[3];
        private Image[] imgSkills = new Image[3];
        private TextMeshProUGUI[] textSkillLv = new TextMeshProUGUI[3];
        private TextMeshProUGUI[] textDescriptions = new TextMeshProUGUI[3];

        /// <summary>
        /// btnSkills[i] = GameObject.Find("���s�ޯ���" + (i+1)).GetComponent<Button>();
        /// textTitles[i] = GameObject.Find("��r�ޯ���D" + i).GetComponent<TextMeshProUGUI>();
        /// imgSkills[i] = GameObject.Find("�Ϥ��ޯ�ϥ�" + i).GetComponent<Image>();
        /// textSkillLv[i] = GameObject.Find("��r�ޯ໡��" + i).GetComponent<TextMeshProUGUI>();
        /// textDescriptions[i] = GameObject.Find("��r�ޯ൥�Ū��A" + i).GetComponent<TextMeshProUGUI>();
        /// </summary>

        private void Awake()
        {
            LevelManager.instance.onlevelUP += ShowSkillUI;
            for(int i = 0; i < 3; i++)
            {
                btnSkills[i] = GameObject.Find("���s�ޯ���" + (i+1)).GetComponent<Button>();
                textTitles[i] = GameObject.Find("��r�ޯ���D" + (i + 1)).GetComponent<TextMeshProUGUI>();
                imgSkills[i] = GameObject.Find("�Ϥ��ޯ�ϥ�" + (i + 1)).GetComponent<Image>();
                textSkillLv[i] = GameObject.Find("��r�ޯ໡��" + (i + 1)).GetComponent<TextMeshProUGUI>();
                textDescriptions[i] = GameObject.Find("��r�ޯ൥�Ū��A" + (i + 1)).GetComponent<TextMeshProUGUI>();
            }
        }
        // Start is called before the first frame update
        void Start()
        {
        

        }

        // Update is called once per frame
        void Update()
        {
        
        }
        private void ShowSkillUI()
        {

        }
    } 
}

using System.Xml;
using UnityEngine;

public class XMLManager : MonoBehaviour
{
    // Resources/XML/Weapon.XML 파일.
    public string xmlFileName = "Weapon";

    void Start()
    {
        LoadXML(xmlFileName);
    }

    private void LoadXML(string _fileName)
    {
        TextAsset txtAsset = (TextAsset)Resources.Load("XML/" + _fileName);
        XmlDocument xmlDoc = new XmlDocument();
        Debug.Log(txtAsset.text);
        xmlDoc.LoadXml(txtAsset.text);

        // 하나씩 가져오기 테스트 예제.
        //XmlNodeList item_Table = xmlDoc.GetElementsByTagName("name");
        //foreach (XmlNode name in item_Table)
        //{
        //    Debug.Log("[one by one] name : " + name.InnerText);
        //}

        // 전체 아이템 가져오기 예제.
        XmlNodeList all_nodes = xmlDoc.SelectNodes("Item/Weapon");
        foreach (XmlNode node in all_nodes)
        {
            // 수량이 많으면 반복문 사용.
            Debug.Log("[at once] code :" + node.SelectSingleNode("code").InnerText);
            Debug.Log("[at once] name : " + node.SelectSingleNode("name").InnerText);
            Debug.Log("[at once] type : " + node.SelectSingleNode("type").InnerText);
            Debug.Log("[at once] grade : " + node.SelectSingleNode("grade").InnerText);
            Debug.Log("[at once] damage : " + node.SelectSingleNode("damage").InnerText);
        }
    }
}
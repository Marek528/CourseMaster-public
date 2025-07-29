using System.Xml;

namespace CourseMaster
{
    public class Course
    {
        private string courseXml = "";
        private XmlDocument doc = new XmlDocument();
        public string Link {  get; private set; }
        public string RawXMLtext { get; private set; }

        public string Name { get; private set; }
        public string Thumbnail { get; private set; }
        public int Sections { get; private set; }
        public List<string> SectionTitles { get; private set; }
        public int Topics { get; private set; }
        public List<List<string>> TopicTitles { get; private set; }
        public List<List<string>> TopicContent { get; private set; }

        // quizes
        public int Quizes { get; private set; }
        public List<List<string>> Questions { get; private set; }
        public List<List<string>> Answers { get; private set; }
        public List<List<string>> CorrectAnswers { get; private set; }

        public Course(string xml)
        {
            courseXml += xml;
            doc.Load(courseXml);
            Link = xml;
            RawXMLtext = doc.OuterXml;

            Name = getNodeText("course/title");
            Thumbnail = getNodeText("course/img");
            Sections = getNodeAmount("course/sections/section");
            Topics = getNodeAmount("course/sections/section/topics/topic");
            SectionTitles = getMultipleNodeText("course/sections/section/title");

            TopicTitles = new List<List<string>>();
            TopicContent = new List<List<string>>();

            for (int i = 1; i <= Topics; i++)
            {
                TopicTitles.Add(getMultipleNodeText("course/sections/section/topics/topic/title", i));
                TopicContent.Add(getMultipleNodeText("course/sections/section/topics/topic/content", i));
            }

            // quizes
            Quizes = getNodeAmount("course/quizes/quiz");

            Questions = new List<List<string>>();
            Answers = new List<List<string>>();
            CorrectAnswers = new List<List<string>>();
            for (int i = 1; i <= Quizes; i++)
            {
                Questions.Add(getMultipleNodeText("course/quizes/quiz/question/text", i));
                Answers.Add(getAnswers("course/quizes/quiz/question/answers/answer", i));
                CorrectAnswers.Add(getAnswers("course/quizes/quiz/question/answers/answer", i, true));
            }
        }

        private string getNodeText(string node)
        {
            XmlNode node1 = doc.SelectSingleNode(node);

            if (node1 != null)
            {
                if (node.Contains("img"))
                {
                    return node1.Attributes["src"]?.Value.ToString();
                }

                if (node.Contains("title"))
                {
                    return node1.InnerText;
                }

                return null;
            }

            return null;
        }

        private int getNodeAmount(string node)
        {
            XmlNodeList nodes = doc.SelectNodes(node);
            int count = 0;

            foreach (XmlNode item in nodes)
            {
                if (!string.IsNullOrEmpty(item.InnerText))
                {
                    count++;
                }
                else
                {
                    MessageBox.Show($"The node is empty, node: {node}; item: {item.InnerText}");
                    break;
                }
            }

            return count;
        }

        private List<string> getMultipleNodeText(string node, int checkID = 0)
        {
            List<string> arr = new List<string>();
            XmlNodeList nodes = doc.SelectNodes(node);
            string attribute;

            foreach (XmlNode item in nodes)
            {
                if (!string.IsNullOrEmpty(item.InnerText))
                {
                    if (checkID != 0)
                    {
                        attribute = item.ParentNode.Attributes["id"]?.Value;
                        if (attribute == checkID.ToString())
                        {
                            arr.Add(item.InnerText);
                        }
                    }
                    else
                    {
                        arr.Add(item.InnerText);
                    }
                }
                else
                {
                    MessageBox.Show($"The node is empty, node: {node}; item: {item.InnerText}");
                    break;
                }
            }

            return arr;
        }

        private List<string> getAnswers(string node, int checkID, bool findCorrectAns = false)
        {
            List<string> arr = new List<string>();
            XmlNodeList nodes = doc.SelectNodes(node);
            string multipleAnswers = "", attribute;
            int count = 0;

            foreach(XmlNode item in nodes)
            {
                if (string.IsNullOrEmpty(item.InnerText))
                {
                    MessageBox.Show($"The node is empty, node: {node}; item: {item.InnerText}");
                    break;
                }

                attribute = item.ParentNode.Attributes["id"]?.Value;

                if (attribute == checkID.ToString())
                {
                    if (!findCorrectAns)
                    {
                        multipleAnswers += $"{item.InnerText};";
                        count++;

                        if (count == 4)
                        {
                            arr.Add(multipleAnswers);
                            multipleAnswers = "";
                            count = 0;
                        }
                    }
                    else
                    {
                        attribute = item.Attributes["correct"]?.Value;
                        if (attribute == "true")
                        {
                            arr.Add(item.InnerText);
                        }
                    } 
                }
            }

            return arr;
        }
    }
}

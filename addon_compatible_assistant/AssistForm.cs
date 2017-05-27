using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace addon_compatible_assistant
{
    public partial class AssistForm : Form
    {
        // 要自动替换的内容
        private Dictionary<string, string> replaceDict;
        private Dictionary<string, string> replaceDictIgnoreCase;
        // 要处理的目录列表
        private List<DirectoryInfo> dirList = new List<DirectoryInfo>();

        public AssistForm()
        {
            InitializeComponent();

            InitializeParams();
        }

        // 初始化替换列表等
        private void InitializeParams()
        {
            // 考虑大小写的替换表
            replaceDict = new Dictionary<string, string>()
            {
                { "$(MEDIUM_FONT)", "EsoZH/fonts/univers57.otf" },
                { "$(BOLD_FONT)", "EsoZH/fonts/univers67.otf" },
                { "$(CHAT_FONT)", "EsoZH/fonts/univers57.otf" },
                { "$(GAMEPAD_LIGHT_FONT)", "EsoZH/fonts/ftn47.otf" },
                { "$(GAMEPAD_MEDIUM_FONT)", "EsoZH/fonts/ftn57.otf" },
                { "$(GAMEPAD_BOLD_FONT)", "EsoZH/fonts/ftn87.otf" },
                { "$(ANTIQUE_FONT)", "EsoZH/fonts/proseantiquepsmt.otf" },
                { "$(HANDWRITTEN_FONT)", "EsoZH/fonts/handwritten_bold.otf" },
                { "$(STONE_TABLET_FONT)", "EsoZH/fonts/trajanpro-regular.otf" },

                { "\"MEDIUM_FONT\"", "\"EsoZH/fonts/univers57.otf\"" },
                { "\"BOLD_FONT\"", "\"EsoZH/fonts/univers67.otf\"" },
                { "\"CHAT_FONT\"", "\"EsoZH/fonts/univers57.otf\"" },
                { "\"GAMEPAD_LIGHT_FONT\"", "\"EsoZH/fonts/ftn47.otf\"" },
                { "\"GAMEPAD_MEDIUM_FONT\"", "\"EsoZH/fonts/ftn57.otf\"" },
                { "\"GAMEPAD_BOLD_FONT\"", "\"EsoZH/fonts/ftn87.otf\"" },
                { "\"ANTIQUE_FONT\"", "\"EsoZH/fonts/proseantiquepsmt.otf\"" },
                { "\"HANDWRITTEN_FONT\"", "\"EsoZH/fonts/handwritten_bold.otf\"" },
                { "\"STONE_TABLET_FONT\"", "\"EsoZH/fonts/trajanpro-regular.otf\"" },
            };

            // 忽略大小写的替换表
            replaceDictIgnoreCase = new Dictionary<string, string>()
            {
                { "EsoUI/Common/Fonts/", "EsoZH/fonts/" },
            };
        }

        // 遍历检查目录，列出将处理的目录
        private void CheckDirectory()
        {
            DirectoryInfo dir = new DirectoryInfo(Directory.GetCurrentDirectory());

            if (dir.Name.ToLower() != "addons")
            {
                MessageBox.Show("请把本程序复制到 AddOns 下运行。");
            }

            listViewDir.Clear();
            listViewDir.Columns.Add("插件", listViewDir.Width - 50);
            dirList.Clear();

            // 遍历，排除汉化插件的目录
            foreach (DirectoryInfo subDir in dir.GetDirectories())
            {
                if (subDir.Name.ToLower() != "esoui" && subDir.Name.ToLower() != "esozh" && subDir.Name.ToLower() != "gamedata")
                {
                    dirList.Add(subDir);
                    listViewDir.Items.Add(subDir.Name);
                }
            }
        }

        // 修改插件文本
        private void EditAddons()
        {
            List<FileInfo> files = new List<FileInfo>();    // 待修改的文件列表

            // 遍历寻找文件
            foreach (DirectoryInfo addonDir in dirList)
            {
                foreach (FileInfo info in addonDir.EnumerateFiles("*.*", SearchOption.AllDirectories))
                {
                    if (info.Name.ToLower().EndsWith(".lua") || info.Name.ToLower().EndsWith(".xml"))
                    {
                        files.Add(info);
                    }
                }
            }

            foreach (FileInfo file in files)
            {
                EditFile(file);
            }

            MessageBox.Show("修改完成。");
        }

        // 修改一个文件
        private void EditFile(FileInfo file)
        {
            // read
            StreamReader sr = new StreamReader(file.FullName);
            string text = sr.ReadToEnd();
            sr.Close();

            // edit
            foreach (var item in replaceDict)
            {
                text = text.Replace(item.Key, item.Value);
            }
            foreach (var item in replaceDictIgnoreCase)
            {
                text = Regex.Replace(text, item.Key, item.Value, RegexOptions.IgnoreCase);
            }

            // write
            StreamWriter sw = new StreamWriter(file.FullName);
            sw.Write(text);
            sw.Close();
        }

        // 显示窗口时
        private void AssistForm_Shown(object sender, EventArgs e)
        {
            CheckDirectory();
        }

        // 点击修改按钮时
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            buttonEdit.Enabled = false;
            EditAddons();
        }
    }
}

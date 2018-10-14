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
                { "esoui/common/fonts/eso_fwudc_70-m.ttf", "EsoZH/fonts/univers57.otf" },
                { "esoui/common/fonts/eso_fwntlgudc70-db.ttf", "EsoZH/fonts/univers57.otf" },
                { "EsoUI/Common/Fonts/", "EsoZH/fonts/" },

                { "AUI/fonts/Kingthings_Calligraphica_2.ttf", "EsoZH/fonts/univers57.otf" },
                { "AUI/fonts/Almendra-Bold.otf", "EsoZH/fonts/univers67.otf" },
                { "AUI/fonts/SansitaOne.ttf", "EsoZH/fonts/univers67.otf" },
                { "AUI/fonts/Bellota-Bold.otf", "EsoZH/fonts/univers57.otf" },
                { @"\[\[CombatCloud/Media/Fonts/[A-Za-z\-_]*\.[ot]?tf\]\]", "[[EsoZH/fonts/univers57.otf]]" },
                { "FoundryTacticalCombat/lib/fonts/Metamorphous.otf", "EsoZH/fonts/univers57.otf" },
                { "MasterMerchant/Fonts/arialn.ttf", "EsoZH/fonts/univers57.otf" },
                { "MasterMerchant/Fonts/esocartographer-bold.otf", "EsoZH/fonts/univers67.otf" },
                { "MasterMerchant/Fonts/fontin_sans_b.otf", "EsoZH/fonts/univers67.otf" },
                { "MasterMerchant/Fonts/fontin_sans_i.otf", "EsoZH/fonts/univers57.otf" },
                { "MasterMerchant/Fonts/fontin_sans_r.otf", "EsoZH/fonts/univers57.otf" },
                { "MasterMerchant/Fonts/fontin_sans_sc.otf", "EsoZH/fonts/univers57.otf" },
                { "MiniMap/Fonts/arialn.ttf", "EsoZH/fonts/univers57.otf" },
                { "MiniMap/Fonts/consola.ttf", "EsoZH/fonts/univers57.otf" },
                { "MiniMap/Fonts/esocartographer-bold.otf", "EsoZH/fonts/univers67.otf" },
                { "MiniMap/Fonts/fontin_sans_b.otf", "EsoZH/fonts/univers67.otf" },
                { "MiniMap/Fonts/fontin_sans_i.otf", "EsoZH/fonts/univers57.otf" },
                { "MiniMap/Fonts/fontin_sans_r.otf", "EsoZH/fonts/univers57.otf" },
                { "MiniMap/Fonts/fontin_sans_sc.otf", "EsoZH/fonts/univers57.otf" },
                { "/BanditsUserInterface/fonts/univers57.otf", "EsoZH/fonts/univers57.otf" },
                { "/BanditsUserInterface/fonts/univers67.otf", "EsoZH/fonts/univers57.otf" },
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

            // 遍历修改文件
            foreach (FileInfo file in files)
            {
                if (file.Name.ToLower() == "libaddonmenu-2.0.lua")
                {
                    EditLAM(file);
                }
                else
                {
                    EditFile(file);
                }
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
            foreach (var item in replaceDictIgnoreCase)
            {
                text = Regex.Replace(text, item.Key, item.Value, RegexOptions.IgnoreCase);
            }
            foreach (var item in replaceDict)
            {
                text = text.Replace(item.Key, item.Value);
            }

            // write
            StreamWriter sw = new StreamWriter(file.FullName);
            sw.Write(text);
            sw.Close();
        }

        // 修改 LibAddonMenu 文件
        private void EditLAM(FileInfo file)
        {
            // read
            StreamReader sr = new StreamReader(file.FullName);
            string text = sr.ReadToEnd();
            sr.Close();

            // edit
            string pattern1 = @"
                local\ controlPanelNames\ =
                [^{}]*                  # any non brace stuff.
                \{(                     # First '{' + capturing bracket
                    (?:
                    [^{}]               # Match all non-braces
                    |
                    (?<open> \{ )       # Match '{', and capture into 'open'
                    |
                    (?<-open> \} )      # Match '}', and delete the 'open' capture
                    )+                  # Change to * if you want to allow {}
                    (?(open)(?!))       # Fails if 'open' stack isn't empty!
                )\}                     # Last
            ";
            string pattern2 = @"
                local\ localization\ =
                [^{}]*                  # any non brace stuff.
                \{(                     # First '{' + capturing bracket
                    (?:
                    [^{}]               # Match all non-braces
                    |
                    (?<open> \{ )       # Match '{', and capture into 'open'
                    |
                    (?<-open> \} )      # Match '}', and delete the 'open' capture
                    )+                  # Change to * if you want to allow {}
                    (?(open)(?!))       # Fails if 'open' stack isn't empty!
                )\}                     # Last '}' + close capturing bracket
            ";

            string matched = Regex.Match(text, pattern1, RegexOptions.IgnorePatternWhitespace).Value;
            if (matched != "" && !matched.Contains("zh = "))
            {
                // 情况1
                matched = matched.Replace("local controlPanelNames = {", "local controlPanelNames = {\n\t\tzh = \"插件设置\",");
                text = Regex.Replace(text, pattern1, matched, RegexOptions.IgnorePatternWhitespace);
            }
            else
            {
                matched = Regex.Match(text, pattern2, RegexOptions.IgnorePatternWhitespace).Value;
                if (matched != "" && !matched.Contains("zh = "))
                {
                    // 情况2
                    string zhText = "    zh = {\n        PANEL_NAME = \"插件\",\n" +
                            "        VERSION = \"版本: <<X:1>>\",\n        WEBSITE = \"访问网站\",\n" +
                            "        PANEL_INFO_FONT = \"EsoZh/fonts/univers57.otf|14|soft-shadow-thin\",\n    },";
                    matched = matched.Replace("local localization = {", "local localization = {\n" + zhText);
                    text = Regex.Replace(text, pattern2, matched, RegexOptions.IgnorePatternWhitespace);
                }
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

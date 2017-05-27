using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace addon_compatible_assistant
{
    public partial class AssistForm : Form
    {
        // 要自动替换的内容
        private Dictionary<string, string> replaceList;
        private Dictionary<string, string> replaceListIgnoreCase;
        // 要处理的目录列表
        private List<DirectoryInfo> dirList = new List<DirectoryInfo>();

        public AssistForm()
        {
            InitializeComponent();

            listViewDir.HeaderStyle = ColumnHeaderStyle.None;

            InitializeParams();
            CheckDirectory();
        }

        // 初始化替换列表等
        private void InitializeParams()
        {
            // 考虑大小写的替换表
            replaceList = new Dictionary<string, string>()
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
            replaceListIgnoreCase = new Dictionary<string, string>()
            {
                { "EsoUI/Common/Fonts/", "EsoZH/fonts/" },
            };
        }

        // 遍历检查目录，列出将处理的目录
        private void CheckDirectory()
        {
            DirectoryInfo dir = new DirectoryInfo(Directory.GetCurrentDirectory());
            listViewDir.Clear();
            listViewDir.Columns.Add("", listViewDir.Width - 50);
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
    }
}

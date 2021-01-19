using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormAnimation;

namespace keyBoard
{
    public partial class Form1 : MaterialForm
    {
        public static int clickPX;
        public static int clickPY;
        public static int upPX;
        public static int upPY;
        public static string test = "aa";
        public static MaterialRaisedButton[] buttonList = new MaterialRaisedButton[5];
        public static System.Windows.Forms.Button[] manyButtons = new Button[5];
        public static Panel panel1 = new Panel();
        public static string[] region = new string[] { "中", "左", "上", "右", "下" };
        public static int[,] regionPoint = new int[5, 2];
        public static int button_size = 70;
        public static int CenterULX;
        public static int CenterULY;
        public static int CenterDRX;
        public static int CenterDRY;
        public static string regionResult;
        public static bool flag = true;

        int screenH = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
        //ディスプレイの幅
        int screenW = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;



        public static string[,] japanese = new string[,] {
            {"あ","い","う","え","お"},
            {"か","き","く","け","こ" },
            {"さ","し","す","せ","そ" },
            {"た","ち","つ","て","と" },
            {"な","に","ぬ","ね","の" },
            {"は","ひ","ふ","へ","ほ" },
            {"ま","み","む","め","も" },
            {"や","ゆ","よ","","" },
            {"ら","り","る","れ","ろ" },
            {"わ","を","ん","","" },

        };

        public static string[,] dakuten = new string[,] {
            {"あ","い","う","え","お"},
            {"が","ぎ","ぐ","げ","ご" },
            {"ざ","じ","ず","ぜ","ぞ" },
            {"だ","ぢ","づ","で","ど" },
            {"な","に","ぬ","ね","の" },
            {"ば","び","ぶ","べ","ぼ" },
            {"ま","み","む","め","も"},
            {"や","ゆ","よ","",""},
            {"ら","り","る","れ","ろ"},
            {"わ","を","ん","",""},
        };

        public static string[,] handakuon = new string[,]
        {
            {"あ","い","う","え","お"},
            {"が","ぎ","ぐ","げ","ご"},
            {"ざ","じ","ず","ぜ","ぞ"},
            {"だ","ぢ","づ","で","ど" },
            {"な","に","ぬ","ね","の" },
            {"ぱ","ぴ","ぷ","ぺ","ぽ"},
            {"ま","み","む","め","も"},
            {"や","ゆ","よ","",""},
            {"ら","り","る","れ","ろ"},
            {"わ","を","ん","",""},
        };


        public Form1()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);

        }

        //フォームを非アクティブに
        private const int WS_EX_NOACTIVATE = 0x8000000;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams p = base.CreateParams;

                if (!DesignMode)
                {
                    p.ExStyle |= (WS_EX_NOACTIVATE);
                }

                return (p);
            }
        }



        private void materialRaisedButton1_MouseDown(object sender, MouseEventArgs e)
        {
            CreateButton(sender);
        }

        private void materialRaisedButton1_MouseUp(object sender, MouseEventArgs e)
        {
            for (int i = 1; i <= 4; i++)
            {
                this.Controls.Remove(manyButtons[i]);
            }
        }


        private void materialRaisedButton2_MouseDown(object sender, MouseEventArgs e)
        {
            CreateButton(sender);

        }



        private void materialRaisedButton2_MouseUp(object sender, MouseEventArgs e)
        {
            upPX = int.Parse(Cursor.Position.X.ToString());
            upPY = int.Parse(Cursor.Position.Y.ToString());


            CenterULX = PointToScreen(manyButtons[0].Location).X;
            CenterULY = PointToScreen(manyButtons[0].Location).Y;

            CenterDRX = CenterULX + manyButtons[0].Size.Width;
            CenterDRY = CenterULY + manyButtons[0].Size.Height;



            regionResult = CheckRegion();

            //入力処理
            int i = 0;
            foreach (string tmp in region)
            {
                if (tmp == regionResult)
                {
                    string sendText = manyButtons[i].Text;
                    SendKeys.Send(sendText);


                }
                i++;

            }




            for (int m = 1; m <= 4; m++)
            {

                this.Controls.Remove(manyButtons[m]);
            }
        }



        private void CreateButton(object sender)
        {
            int T = 0;
            int Y = 0;
            while (((MaterialRaisedButton)sender).Text != japanese[T, Y])
            {
                T++;
            }


            manyButtons[0] = ((MaterialRaisedButton)sender);
            clickPX = int.Parse(PointToScreen(Cursor.Position).X.ToString());
            clickPY = int.Parse(PointToScreen(Cursor.Position).Y.ToString());

            //い行のボタン
            int panelx = ((MaterialRaisedButton)sender).Location.X - button_size;
            int panely = ((MaterialRaisedButton)sender).Location.Y;
            manyButtons[1] = new MaterialRaisedButton();
            manyButtons[1].Name = "iButton";
            manyButtons[1].Text = japanese[T, 1];
            manyButtons[1].Location = new Point(panelx, panely);
            manyButtons[1].Size = new Size(button_size, button_size);
            manyButtons[1].TabIndex = 9;
            manyButtons[1].BringToFront();
            this.Controls.Add(manyButtons[1]);



            //ウ行のボタン
            panelx = ((MaterialRaisedButton)sender).Location.X;
            panely = ((MaterialRaisedButton)sender).Location.Y - button_size;
            manyButtons[2] = new MaterialRaisedButton();
            manyButtons[2].Name = "uButton";
            manyButtons[2].Text = japanese[T, 2];
            manyButtons[2].Location = new Point(panelx, panely);
            manyButtons[2].Size = new Size(button_size, button_size);
            manyButtons[2].TabIndex = 9;
            manyButtons[2].BringToFront();
            this.Controls.Add(manyButtons[2]);


            //え行のボタン
            panelx = ((MaterialRaisedButton)sender).Location.X + button_size;
            panely = ((MaterialRaisedButton)sender).Location.Y;
            manyButtons[3] = new MaterialRaisedButton();
            manyButtons[3].Name = "eButton";
            manyButtons[3].Text = japanese[T, 3];
            manyButtons[3].Location = new Point(panelx, panely);
            manyButtons[3].Size = new Size(button_size, button_size);
            manyButtons[3].BringToFront();
            manyButtons[3].TabIndex = 9;
            this.Controls.Add(manyButtons[3]);

            //お行のボタン
            panelx = ((MaterialRaisedButton)sender).Location.X;
            panely = ((MaterialRaisedButton)sender).Location.Y + button_size;
            manyButtons[4] = new MaterialRaisedButton();
            manyButtons[4].Name = "oButton";
            manyButtons[4].Text = japanese[T, 4];
            manyButtons[4].Location = new Point(panelx, panely);
            manyButtons[4].Size = new Size(button_size, button_size);
            manyButtons[4].BringToFront();
            manyButtons[4].TabIndex = 9;
            this.Controls.Add(manyButtons[4]);

            //test label

            foreach (MaterialRaisedButton mate in manyButtons)
            {
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
        }

        //今後つかうかも？
        public IEnumerable<Control> GetSelfAndChildrenRecursive(Control parent)
        {
            List<Control> controls = new List<Control>();

            foreach (Control child in parent.Controls)
            {
                controls.AddRange(GetSelfAndChildrenRecursive(child));
            }

            controls.Add(parent);

            return controls;
        }


        private string CheckRegion()
        {
            //ア列検知
            if (CenterULX < upPX && CenterULY < upPY && upPX < CenterDRX && upPY < CenterDRY)
            {
                regionResult = "中";
                return regionResult;
            }




            //い列入力検知(いくき等)
            if (upPX < CenterULX)
            {
                int Threshold = CenterULX - upPX;
                int yCheck = CenterULY - Threshold;
                int yCheckPlus = yCheck + manyButtons[0].Size.Height + Threshold * 2;
                if (yCheck < upPY && upPY < yCheckPlus)
                {
                    regionResult = "左";
                    return regionResult;
                }

            }


            //う列検知
            if (upPY < CenterULY)
            {
                int Threshold = CenterULY - upPY;
                int xCheck = CenterULX - Threshold;
                int xCheckPlus = xCheck + manyButtons[0].Size.Width + Threshold * 2;
                if (xCheck < upPX && upPX < xCheckPlus)
                {
                    regionResult = "上";
                    return regionResult;
                }
            }


            //え列検知
            if (CenterDRX < upPX)
            {
                int Threshold = upPX - CenterDRX;
                int yCheck = CenterDRY - Threshold - manyButtons[0].Size.Height;
                int yCheckPlus = yCheck + manyButtons[0].Size.Height + Threshold * 2;
                if (yCheck < upPY && upPY < yCheckPlus)
                {
                    regionResult = "右";
                    return regionResult;
                }

            }

            //お列検知
            if (CenterDRY < upPY)
            {
                int Threshold = upPY - CenterULY;
                int xCheck = CenterULX + Threshold;
                int xCheckPlus = xCheck - manyButtons[0].Size.Width - Threshold * 2;
                if (upPX < xCheck && xCheckPlus < upPX)
                {
                    regionResult = "下";
                    return regionResult;
                }
            }

            return regionResult = "範囲外";
        }


        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            dynamically_adding();
        }

        private void set_label_Click(object sender, EventArgs e)
        {

        }

        private void Manual_Click(object sender, EventArgs e)
        {
            this.Controls.Remove(menu);
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            foreach (Control material_btn in this.Controls)
            {
                if (material_btn is MaterialRaisedButton)
                {
                    material_btn.Visible = false;
                }

            }
            this.Controls.Remove(menu);

        }

        private void Hamburger_MouseDown(object sender, MouseEventArgs e)
        {
            this.Controls.Remove(menu);
        }

        private void Keyboard_MouseDown(object sender, MouseEventArgs e)
        {
            this.Controls.Remove(menu);
            foreach (Control material_btn in this.Controls)
            {
                if (material_btn is MaterialRaisedButton)
                {
                    material_btn.Visible = true;
                }

            }

        }


     

        private void Power_off(object sender, EventArgs e)
        {
                DialogResult result = MessageBox.Show("アプリケーションを終了しますか？",
                "メッセージ",
                 MessageBoxButtons.YesNoCancel,
                 MessageBoxIcon.Information,
                 MessageBoxDefaultButton.Button2);

                //何が選択されたか調べる
                if (result == DialogResult.Yes)
                {
                    //「はい」が選択された時
                    this.Close();
                }
    }


        //動的コントロール追加（ラベル、ボタン）
        private System.Windows.Forms.Label key_label;
        private System.Windows.Forms.Label set_label;
        private System.Windows.Forms.Label manual_label;
        private System.Windows.Forms.Label exit_label;
        private System.Windows.Forms.Button Hamburger;
        private System.Windows.Forms.Button Keyboard;
        private System.Windows.Forms.Button Settings;
        private System.Windows.Forms.Button Manual;
        private System.Windows.Forms.Button Exit;
        private void dynamically_adding()
        {
            // 
            // メニューバー
            // 
            this.menu = new System.Windows.Forms.Panel();
            this.menu.BackColor = System.Drawing.SystemColors.ControlLight;

            this.menu.Location = new System.Drawing.Point(0, 63);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(256, 568);
            this.menu.TabIndex = 109;
            this.menu.SuspendLayout();
            

            // 
            // キーボード
            // 
            this.key_label = new System.Windows.Forms.Label();
            this.key_label.Font = new System.Drawing.Font("游ゴシック", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.key_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.key_label.Location = new System.Drawing.Point(70, 120);
            this.key_label.Name = "key_label";
            this.key_label.Size = new System.Drawing.Size(90, 29);
            this.key_label.TabIndex = 110;
            this.key_label.Text = "キーボード";
            this.key_label.AutoSize = true;
            this.key_label.MouseDown += new System.Windows.Forms.MouseEventHandler(Keyboard_MouseDown); 
            this.menu.Controls.Add(key_label);
            

            // 
            // 設定
            // 
            this.set_label = new System.Windows.Forms.Label();
            this.set_label.Font = new System.Drawing.Font("游ゴシック", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.set_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.set_label.Location = new System.Drawing.Point(76, 245);
            this.set_label.Name = "set_label";
            this.set_label.Size = new System.Drawing.Size(61, 20);
            this.set_label.TabIndex = 111;
            this.set_label.Text = "設定";
            this.set_label.Click += new System.EventHandler(this.Settings_Click);
            this.set_label.AutoSize = true;
            this.menu.Controls.Add(set_label);

            // 
            // 使い方
            // 
            this.manual_label = new System.Windows.Forms.Label();
            this.manual_label.Font = new System.Drawing.Font("游ゴシック", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.manual_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.manual_label.Location = new System.Drawing.Point(70, 365);
            this.manual_label.Name = "manual_label";
            this.manual_label.Size = new System.Drawing.Size(93, 23);
            this.manual_label.TabIndex = 112;
            this.manual_label.Text = "使い方";
            this.manual_label.AutoSize = true;
            this.menu.Controls.Add(manual_label);

            // 
            // 終了
            // 
            this.exit_label = new System.Windows.Forms.Label();
            this.exit_label.Font = new System.Drawing.Font("游ゴシック", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.exit_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.exit_label.Location = new System.Drawing.Point(76, 485);
            this.exit_label.Name = "exit_label";
            this.exit_label.Size = new System.Drawing.Size(93, 30);
            this.exit_label.TabIndex = 112;
            this.exit_label.Text = "終了";
            this.exit_label.AutoSize = true;
            this.menu.Controls.Add(exit_label);



            // 
            // 使い方ボタン
            // 
            this.Manual = new System.Windows.Forms.Button();
            this.Manual.BackColor = System.Drawing.Color.Transparent;
            //this.Manual.BackgroundImage = Properties.Resources.Manual_BackgroundImage;
            this.Manual.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Manual.FlatAppearance.BorderSize = 0;
            this.Manual.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Manual.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Manual.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Manual.ForeColor = System.Drawing.Color.Transparent;
            this.Manual.Location = new System.Drawing.Point(18, 360);
            this.Manual.Name = "Manual";
            this.Manual.Size = new System.Drawing.Size(30, 38);
            this.Manual.TabIndex = 108;
            this.Manual.UseVisualStyleBackColor = false;
            this.Manual.Click += new System.EventHandler(this.Manual_Click);
            this.menu.Controls.Add(Manual);
            // 
            // 設定ボタン
            // 
            this.Settings = new System.Windows.Forms.Button();
            this.Settings.BackColor = System.Drawing.Color.Transparent;
            //this.Settings.BackgroundImage = Properties.Resources.Settings_BackgroundImage;
            this.Settings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Settings.FlatAppearance.BorderSize = 0;
            this.Settings.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Settings.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Settings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Settings.ForeColor = System.Drawing.Color.Transparent;
            this.Settings.Location = new System.Drawing.Point(15, 240);
            this.Settings.Name = "Settings";
            this.Settings.Size = new System.Drawing.Size(35, 38);
            this.Settings.TabIndex = 107;
            this.Settings.UseVisualStyleBackColor = false;
            this.Settings.Click += new System.EventHandler(this.Settings_Click);
            this.menu.Controls.Add(Settings);
            // 
            // ハンバーガー
            // 
            this.Hamburger = new System.Windows.Forms.Button();
            this.Hamburger.BackColor = System.Drawing.Color.Transparent;
            //this.Hamburger.BackgroundImage = Properties.Resources.Hamburger_BackgroundImage;
            this.Hamburger.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Hamburger.FlatAppearance.BorderSize = 0;
            this.Hamburger.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Hamburger.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Hamburger.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Hamburger.ForeColor = System.Drawing.Color.Transparent;
            this.Hamburger.Location = new System.Drawing.Point(3, 3);
            this.Hamburger.Name = "Hamburger";
            this.Hamburger.Size = new System.Drawing.Size(44, 49);
            this.Hamburger.TabIndex = 106;
            this.Hamburger.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Hamburger.UseVisualStyleBackColor = false;
            this.Hamburger.MouseDown += new System.Windows.Forms.MouseEventHandler(Hamburger_MouseDown);
            this.menu.Controls.Add(Hamburger);

            // 
            //　キーボードボタン
            // 

            this.Keyboard = new System.Windows.Forms.Button();
            this.Keyboard.BackColor = System.Drawing.Color.Transparent;
            //this.Keyboard.BackgroundImage = Properties.Resources.Keyboard_BackgroundImage;
            this.Keyboard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Keyboard.FlatAppearance.BorderSize = 0;
            this.Keyboard.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Keyboard.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Keyboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Keyboard.ForeColor = System.Drawing.Color.Transparent;
            this.Keyboard.Location = new System.Drawing.Point(15, 110);
            this.Keyboard.Name = "Keyboard";
            this.Keyboard.Size = new System.Drawing.Size(38, 38);
            this.Keyboard.TabIndex = 106;
            this.Keyboard.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Keyboard.UseVisualStyleBackColor = false;
            this.Keyboard.MouseDown += new System.Windows.Forms.MouseEventHandler(Keyboard_MouseDown);
            this.menu.Controls.Add(Keyboard);

            // 
            //　終了ボタン
            // 

            this.Exit = new System.Windows.Forms.Button();
            this.Exit.BackColor = System.Drawing.Color.Transparent;
            //this.Exit.BackgroundImage = Properties.Resources.Exit_BackgroundImage;
            this.Exit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Exit.FlatAppearance.BorderSize = 0;
            this.Exit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Exit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Exit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Exit.ForeColor = System.Drawing.Color.Transparent;
            this.Exit.Location = new System.Drawing.Point(15, 480);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(38, 38);
            this.Exit.TabIndex = 106;
            this.Exit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Exit.UseVisualStyleBackColor = false;
            this.Exit.MouseDown += new System.Windows.Forms.MouseEventHandler(Power_off);

            this.menu.Controls.Add(Exit);


            ///ボタンとラベルをパネルに追加
            this.Controls.Add(this.menu);
            this.menu.BringToFront();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}

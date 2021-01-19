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
        public static MaterialRaisedButton[] buttonList = new MaterialRaisedButton[20];
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

     

        public static string[,]　japanese = new string[,] { 
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
            CenterDRY = CenterULY+ manyButtons[0].Size.Height;



            regionResult= CheckRegion();

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
            while (((Button)sender).Text != japanese[T, Y])
            {
                T++;
            }


            manyButtons[0] = ((Button)sender);
            clickPX = int.Parse(PointToScreen(Cursor.Position).X.ToString());
            clickPY = int.Parse(PointToScreen(Cursor.Position).Y.ToString());

            //い行のボタン
            int panelx = ((Button)sender).Location.X - button_size;
            int panely = ((Button)sender).Location.Y;
            manyButtons[1] = new Button();
            manyButtons[1].Name = "iButton";
            manyButtons[1].Text = japanese[T, 1];
            manyButtons[1].Location = new Point(panelx, panely);
            manyButtons[1].Size = new Size(button_size, button_size);
            manyButtons[1].TabIndex = 9;
            manyButtons[1].BringToFront();
            this.Controls.Add(manyButtons[1]);



            //ウ行のボタン
            panelx = ((Button)sender).Location.X;
            panely = ((Button)sender).Location.Y - button_size;
            manyButtons[2] = new Button();
            manyButtons[2].Name = "uButton";
            manyButtons[2].Text = japanese[T, 2];
            manyButtons[2].Location = new Point(panelx, panely);
            manyButtons[2].Size = new Size(button_size, button_size);
            manyButtons[2].TabIndex = 9;
            manyButtons[2].BringToFront();
            this.Controls.Add(manyButtons[2]);


            //え行のボタン
            panelx = ((Button)sender).Location.X + button_size;
            panely = ((Button)sender).Location.Y;
            manyButtons[3] = new Button();
            manyButtons[3].Name = "eButton";
            manyButtons[3].Text = japanese[T, 3];
            manyButtons[3].Location = new Point(panelx, panely);
            manyButtons[3].Size = new Size(button_size, button_size);
            manyButtons[3].BringToFront();
            manyButtons[3].TabIndex = 9;
            this.Controls.Add(manyButtons[3]);

            //お行のボタン
            panelx = ((Button)sender).Location.X;
            panely = ((Button)sender).Location.Y + button_size;
            manyButtons[4] = new Button();
            manyButtons[4].Name = "oButton";
            manyButtons[4].Text = japanese[T, 4];
            manyButtons[4].Location = new Point(panelx, panely);
            manyButtons[4].Size = new Size(button_size, button_size);
            manyButtons[4].BringToFront();
            manyButtons[4].TabIndex = 9;
            this.Controls.Add(manyButtons[4]);

            //test label

            foreach(Button mate in manyButtons)
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
                int Threshold = CenterULX-upPX;
                int yCheck = CenterULY-Threshold;
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
                int xCheck = CenterULX-Threshold;
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
                int yCheck = CenterDRY - Threshold-manyButtons[0].Size.Height;
                int yCheckPlus= yCheck + manyButtons[0].Size.Height + Threshold * 2;
                if (yCheck < upPY && upPY < yCheckPlus)
                {
                    regionResult = "右";
                    return regionResult;
                }

            }

            //お列検知
            if (CenterDRY<upPY)
            {
                int Threshold = upPY - CenterULY;
                int xCheck = CenterULX + Threshold;
                int xCheckPlus = xCheck - manyButtons[0].Size.Width - Threshold * 2;
                if (upPX<xCheck && xCheckPlus < upPX)
                {
                    regionResult = "下";
                    return regionResult;
                }
            }

            return regionResult="範囲外";
        }


        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            dynamically_adding();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Controls.Remove(panel2);
        }

        private void button4_Click(object sender, EventArgs e)
        {
                foreach (Control material_btn in this.Controls)
                {
                    if (material_btn is MaterialRaisedButton)
                    {
                        material_btn.Visible= false;
                    }

                }
            this.Controls.Remove(panel2);

        }

        private void button2_MouseDown(object sender, MouseEventArgs e)
        {
            this.Controls.Remove(panel2);
        }

        private void button3_MouseDown(object sender, MouseEventArgs e)
        {
            this.Controls.Remove(panel2);
            foreach (Control material_btn in this.Controls)
            {
                if (material_btn is MaterialRaisedButton)
                {
                    material_btn.Visible = true;
                }

            }

        }


        //動的コントロール追加（ラベル、ボタン）
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private void dynamically_adding()
        {
            // 
            // 横パネル
            // 
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel2.BackColor = System.Drawing.SystemColors.ControlLight;

            this.panel2.Location = new System.Drawing.Point(0, 63);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(256, 568);
            this.panel2.TabIndex = 109;
            this.panel2.SuspendLayout();
            

            // 
            // ホーム
            // 
            this.label2 = new System.Windows.Forms.Label();
            this.label2.Font = new System.Drawing.Font("游ゴシック", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.label2.Location = new System.Drawing.Point(70, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 29);
            this.label2.TabIndex = 110;
            this.label2.Text = "キーボード";
            this.label2.AutoSize = true;
            this.label2.MouseDown += new System.Windows.Forms.MouseEventHandler(button3_MouseDown); 
            this.panel2.Controls.Add(label2);
            

            // 
            // 設定
            // 
            this.label3 = new System.Windows.Forms.Label();
            this.label3.Font = new System.Drawing.Font("游ゴシック", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.label3.Location = new System.Drawing.Point(76, 245);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 20);
            this.label3.TabIndex = 111;
            this.label3.Text = "設定";
            this.label3.Click += new System.EventHandler(this.button4_Click);
            this.label3.AutoSize = true;
            this.panel2.Controls.Add(label3);

            // 
            // 使い方
            // 
            this.label6 = new System.Windows.Forms.Label();
            this.label6.Font = new System.Drawing.Font("游ゴシック", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.label6.Location = new System.Drawing.Point(70, 365);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 23);
            this.label6.TabIndex = 112;
            this.label6.Text = "使い方";
            this.label6.AutoSize = true;
            this.panel2.Controls.Add(label6);

            // 
            // 電源
            // 
            this.label7 = new System.Windows.Forms.Label();
            this.label7.Font = new System.Drawing.Font("游ゴシック", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.label7.Location = new System.Drawing.Point(76, 485);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 30);
            this.label7.TabIndex = 112;
            this.label7.Text = "電源";
            this.label7.AutoSize = true;
            this.panel2.Controls.Add(label7);



            // 
            // 使い方ボタン
            // 
            this.button5 = new System.Windows.Forms.Button();
            this.button5.BackColor = System.Drawing.Color.Transparent;
            this.button5.BackgroundImage = Properties.Resources.button5_BackgroundImage;
            this.button5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button5.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.ForeColor = System.Drawing.Color.Transparent;
            this.button5.Location = new System.Drawing.Point(18, 360);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(30, 38);
            this.button5.TabIndex = 108;
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            this.panel2.Controls.Add(button5);
            // 
            // 設定ボタン
            // 
            this.button4 = new System.Windows.Forms.Button();
            this.button4.BackColor = System.Drawing.Color.Transparent;
            this.button4.BackgroundImage = Properties.Resources.button4_BackgroundImage;
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.ForeColor = System.Drawing.Color.Transparent;
            this.button4.Location = new System.Drawing.Point(15, 240);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(35, 38);
            this.button4.TabIndex = 107;
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            this.panel2.Controls.Add(button4);
            // 
            // ハンバーガー
            // 
            this.button2 = new System.Windows.Forms.Button();
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.BackgroundImage = Properties.Resources.button2_BackgroundImage;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.Color.Transparent;
            this.button2.Location = new System.Drawing.Point(3, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(44, 49);
            this.button2.TabIndex = 106;
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.MouseDown += new System.Windows.Forms.MouseEventHandler(button2_MouseDown);
            this.panel2.Controls.Add(button2);

            // 
            // ホームボタン
            // 

            this.button3 = new System.Windows.Forms.Button();
            this.button3.BackColor = System.Drawing.Color.Transparent;
            this.button3.BackgroundImage = Properties.Resources.keyboard;
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.ForeColor = System.Drawing.Color.Transparent;
            this.button3.Location = new System.Drawing.Point(15, 110);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(38, 38);
            this.button3.TabIndex = 106;
            this.button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button3.UseVisualStyleBackColor = false;
            this.button3.MouseDown += new System.Windows.Forms.MouseEventHandler(button3_MouseDown);
            this.panel2.Controls.Add(button3);

            // 
            //　電源ボタン
            // 

            this.button6 = new System.Windows.Forms.Button();
            this.button6.BackColor = System.Drawing.Color.Transparent;
            this.button6.BackgroundImage = Properties.Resources.button3_BackgroundImage;
            this.button6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button6.FlatAppearance.BorderSize = 0;
            this.button6.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button6.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.ForeColor = System.Drawing.Color.Transparent;
            this.button6.Location = new System.Drawing.Point(15, 480);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(38, 38);
            this.button6.TabIndex = 106;
            this.button6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button6.UseVisualStyleBackColor = false;
            this.button6.MouseDown += new System.Windows.Forms.MouseEventHandler(button2_MouseDown);
            this.panel2.Controls.Add(button6);



            this.Controls.Add(this.panel2);
            this.panel2.BringToFront();
        }

        private void keyButton1_MouseDown(object sender, MouseEventArgs e)
        {
            CreateButton(sender);
        }

        private void keyButton1_MouseUp(object sender, MouseEventArgs e)
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
    }
}

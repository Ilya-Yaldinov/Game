namespace Game
{
    partial class GameBackGround
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameBackGround));
            this.MainHero = new System.Windows.Forms.PictureBox();
            this.MoveTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.MainHero)).BeginInit();
            this.SuspendLayout();
            // 
            // MainHero
            // 
            this.MainHero.BackColor = System.Drawing.Color.Transparent;
            this.MainHero.Location = new System.Drawing.Point(640, 300);
            this.MainHero.Name = "MainHero";
            this.MainHero.Size = new System.Drawing.Size(70, 80);
            this.MainHero.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.MainHero.TabIndex = 0;
            this.MainHero.TabStop = false;
            // 
            // MoveTimer
            // 
            this.MoveTimer.Interval = 7;
            // 
            // GameBackGround
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.MainHero);
            this.Name = "GameBackGround";
            this.Text = "Game";
            this.Load += new System.EventHandler(this.Game_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Game_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Game_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.MainHero)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox MainHero;
        private System.Windows.Forms.Timer MoveTimer;
    }
}


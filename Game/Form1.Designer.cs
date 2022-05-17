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
            this.MainHero = new System.Windows.Forms.PictureBox();
            this.MoveTimer = new System.Windows.Forms.Timer(this.components);
            this.GameProgressTimer = new System.Windows.Forms.Timer(this.components);
            this.KillLable = new System.Windows.Forms.Label();
            this.BoxCount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.MainHero)).BeginInit();
            this.SuspendLayout();
            // 
            // MainHero
            // 
            this.MainHero.BackColor = System.Drawing.Color.Transparent;
            this.MainHero.Location = new System.Drawing.Point(180, 246);
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
            // GameProgressTimer
            // 
            this.GameProgressTimer.Enabled = true;
            this.GameProgressTimer.Interval = 10;
            this.GameProgressTimer.Tick += new System.EventHandler(this.GameProgressTimer_Tick);
            // 
            // KillLable
            // 
            this.KillLable.AutoSize = true;
            this.KillLable.BackColor = System.Drawing.Color.Transparent;
            this.KillLable.Font = new System.Drawing.Font("Segoe Script", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.KillLable.Location = new System.Drawing.Point(787, 9);
            this.KillLable.Name = "KillLable";
            this.KillLable.Size = new System.Drawing.Size(0, 51);
            this.KillLable.TabIndex = 1;
            this.KillLable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BoxCount
            // 
            this.BoxCount.AutoSize = true;
            this.BoxCount.BackColor = System.Drawing.Color.Transparent;
            this.BoxCount.Font = new System.Drawing.Font("Segoe Script", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BoxCount.Location = new System.Drawing.Point(787, 60);
            this.BoxCount.Name = "BoxCount";
            this.BoxCount.Size = new System.Drawing.Size(0, 51);
            this.BoxCount.TabIndex = 2;
            this.BoxCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GameBackGround
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lime;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.BoxCount);
            this.Controls.Add(this.KillLable);
            this.Controls.Add(this.MainHero);
            this.Name = "GameBackGround";
            this.Text = "Game";
            this.Load += new System.EventHandler(this.Game_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Game_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Game_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.MainHero)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox MainHero;
        private System.Windows.Forms.Timer MoveTimer;
        private System.Windows.Forms.Timer GameProgressTimer;
        private System.Windows.Forms.Label KillLable;
        private System.Windows.Forms.Label BoxCount;
    }
}


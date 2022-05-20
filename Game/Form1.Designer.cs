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
            this.EndOfGame = new System.Windows.Forms.Label();
            this.LivePicture = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.MainHero)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LivePicture)).BeginInit();
            this.SuspendLayout();
            // 
            // MainHero
            // 
            this.MainHero.BackColor = System.Drawing.Color.Transparent;
            this.MainHero.Location = new System.Drawing.Point(611, 117);
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
            this.KillLable.Font = new System.Drawing.Font("Segoe Script", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.KillLable.Location = new System.Drawing.Point(897, 0);
            this.KillLable.Name = "KillLable";
            this.KillLable.Size = new System.Drawing.Size(306, 38);
            this.KillLable.TabIndex = 1;
            this.KillLable.Text = "Противников убито: ";
            this.KillLable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BoxCount
            // 
            this.BoxCount.AutoSize = true;
            this.BoxCount.BackColor = System.Drawing.Color.Transparent;
            this.BoxCount.Font = new System.Drawing.Font("Segoe Script", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BoxCount.Location = new System.Drawing.Point(897, 38);
            this.BoxCount.Name = "BoxCount";
            this.BoxCount.Size = new System.Drawing.Size(280, 38);
            this.BoxCount.TabIndex = 2;
            this.BoxCount.Text = "Коробочек собранно:";
            this.BoxCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // EndOfGame
            // 
            this.EndOfGame.AutoSize = true;
            this.EndOfGame.BackColor = System.Drawing.Color.Transparent;
            this.EndOfGame.Font = new System.Drawing.Font("Segoe Script", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EndOfGame.Location = new System.Drawing.Point(334, 232);
            this.EndOfGame.Name = "EndOfGame";
            this.EndOfGame.Size = new System.Drawing.Size(582, 102);
            this.EndOfGame.TabIndex = 3;
            this.EndOfGame.Text = "Игра окончена.";
            this.EndOfGame.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.EndOfGame.Visible = false;
            // 
            // LivePicture
            // 
            this.LivePicture.BackColor = System.Drawing.Color.Transparent;
            this.LivePicture.Location = new System.Drawing.Point(-4, 0);
            this.LivePicture.Name = "LivePicture";
            this.LivePicture.Size = new System.Drawing.Size(286, 76);
            this.LivePicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.LivePicture.TabIndex = 4;
            this.LivePicture.TabStop = false;
            // 
            // GameBackGround
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.LivePicture);
            this.Controls.Add(this.EndOfGame);
            this.Controls.Add(this.BoxCount);
            this.Controls.Add(this.KillLable);
            this.Controls.Add(this.MainHero);
            this.Name = "GameBackGround";
            this.Text = "Game";
            this.Load += new System.EventHandler(this.Sprite_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Game_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Game_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.MainHero)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LivePicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer MoveTimer;
        private System.Windows.Forms.Timer GameProgressTimer;
        private System.Windows.Forms.Label KillLable;
        private System.Windows.Forms.Label BoxCount;
        private System.Windows.Forms.Label EndOfGame;
        private System.Windows.Forms.PictureBox LivePicture;
        private System.Windows.Forms.PictureBox MainHero;
    }
}


using System.ComponentModel;

namespace SimulationInterface
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}

			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.bot = new System.Windows.Forms.Label();
			this.item = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// bot
			// 
			this.bot.BackColor = System.Drawing.SystemColors.ControlDark;
			this.bot.Image = ((System.Drawing.Image) (resources.GetObject("bot.Image")));
			this.bot.Location = new System.Drawing.Point(158, 115);
			this.bot.Name = "bot";
			this.bot.Size = new System.Drawing.Size(24, 24);
			this.bot.TabIndex = 0;
			this.bot.Text = "35";
			this.bot.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// item
			// 
			this.item.BackColor = System.Drawing.Color.FromArgb(((int) (((byte) (0)))), ((int) (((byte) (192)))), ((int) (((byte) (0)))));
			this.item.Location = new System.Drawing.Point(261, 115);
			this.item.Name = "item";
			this.item.Size = new System.Drawing.Size(24, 24);
			this.item.TabIndex = 1;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.item);
			this.Controls.Add(this.bot);
			this.Name = "MainForm";
			this.Text = "MainForm";
			this.ResumeLayout(false);
		}

		private System.Windows.Forms.Label bot;
		private System.Windows.Forms.Label item;

		#endregion
	}
}
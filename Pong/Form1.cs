using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpGL;
using SharpGL.SceneGraph.Assets;

namespace Pong
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            gameOverLbl.Top = openGLControl1.Height / 2 - gameOverLbl.Height / 2;
            gameOverLbl.Left = openGLControl1.Width / 2 - gameOverLbl.Width / 2;
            gameOverLbl.Visible = false;

        }
        bool enabled_W = false;
        bool enabled_S = false;
        bool enabled_Up = false;
        bool enabled_Down = false;
        float leftPlayerRacketY = 0.3f;
        float rightPlayerRacketY = 0.3f;
        float ballX = 0.0f;
        float ballY = 0.0f;
        float speedX = 0.003f;
        float speedY = 0.003f;
        float RacketWidth = 0.015f;
        float RacketHeight = 0.15f;
        float ballWidth = 0.02f;
        float ballHeigth = 0.02f;
       // Boolean isPlay = true;

         

        private void openGLControl1_OpenGLDraw(object sender, SharpGL.RenderEventArgs args)
        {
            
                OpenGL gl = this.openGLControl1.OpenGL;
                gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

                if (enabled_Up && rightPlayerRacketY <= 0.4f)                                //движенеи ракеток
                {
                    rightPlayerRacketY += 0.01f;
                }
                if (enabled_Down && rightPlayerRacketY - RacketHeight >= -0.4f)
                {
                    rightPlayerRacketY -= 0.01f;
                }
                if (enabled_W && leftPlayerRacketY <= 0.4f)
                {
                    leftPlayerRacketY += 0.01f;
                }
                if (enabled_S && leftPlayerRacketY - RacketHeight >= -0.4)
                {
                    leftPlayerRacketY -= 0.01f;
                }

             ballX += speedX;                                    //движение мяча
               ballY += speedY;

                if (ballY >= 0.4f)                                 //отражение от верха
                {
                    speedY = -speedY;
                }
                if (ballY - ballHeigth <= -0.4f)                    //отражение от низа
                {
                    speedY = -speedY;
                }
               /* if (ballX + ballWidth >= 2.4 || ballX <= -2.4)
                {
                    isPlay = false;
                    gameOverLbl.Visible = true;
                }*/

                    if (ballX + ballWidth >= 0.65f - RacketWidth && ballY <= rightPlayerRacketY && ballY - ballHeigth >= rightPlayerRacketY - RacketHeight)
                    {
                        speedX += 0.0002f;
                        // speedY += 0.01f;
                        speedX = -speedX;
                    }
                if (ballX <= -0.65f + RacketWidth && ballY <= leftPlayerRacketY && ballY - ballHeigth >= leftPlayerRacketY - RacketHeight)
                {
                    speedX -= 0.0002f;
                    // speedY += 0.01f;
                    speedX = -speedX;
                }
                gl.LoadIdentity();                                   //прорисовка левой ракетки
                gl.Translate(-0.65f, leftPlayerRacketY,-1.0f);
                gl.Begin(OpenGL.GL_QUADS);
                gl.Color(1.0f, 1.0f, 1.0f);
                gl.Vertex(0, 0);
                gl.Vertex(RacketWidth, 0);
                gl.Vertex(RacketWidth, -RacketHeight);
                gl.Vertex(0, -RacketHeight);
                gl.End();

                gl.LoadIdentity();                                   //прорисовка правой ракетки
                gl.Translate(0.65f, rightPlayerRacketY, -1.0f);
                gl.Begin(OpenGL.GL_QUADS);
                gl.Color(1.0f, 1.0f, 1.0f);
                gl.Vertex(0, 0);
                gl.Vertex(-RacketWidth, 0);
                gl.Vertex(-RacketWidth, -RacketHeight);
                gl.Vertex(0, -RacketHeight);
                gl.End();

                gl.LoadIdentity();                                  //прорисовка мяча
                gl.Translate(ballX, ballY, -1.0f);
                gl.Begin(OpenGL.GL_QUADS);
                gl.Color(1.0f, 1.0f, 1.0f);
                gl.Vertex(0, 0);
                gl.Vertex(ballWidth, 0);
                gl.Vertex(ballWidth, ballHeigth);
                gl.Vertex(0, ballHeigth);
                gl.End();

                gl.Flush();
            
        }

        private void openGLControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
            if (e.KeyCode == Keys.W)
            {
                enabled_W = true;
            }
            if (e.KeyCode == Keys.S)
            {
                enabled_S = true;
            }
            if (e.KeyCode == Keys.K)
            {
                enabled_Up = true;
            }
            if (e.KeyCode == Keys.M)
            {
                enabled_Down = true;
            }
        }

        private void openGLControl1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                enabled_W = false;
            }
            if (e.KeyCode == Keys.S)
            {
                enabled_S = false;
            }
            if (e.KeyCode == Keys.K)
            {
                enabled_Up = false;
            }
            if (e.KeyCode == Keys.M)
            {
                enabled_Down = false;
            }
        }
    }
}

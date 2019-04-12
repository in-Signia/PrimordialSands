using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;

namespace PrimordialSands.Shaders
{
	public class TestShader : CustomSky
	{
        private Random _random = new Random();
        private bool _isActive;

        public override void OnLoad()
        {
        }

        public override void Update(GameTime gameTime)
        {
        }

        private float GetIntensity()
        {
            return 0.05f;
        }

        public override Color OnTileColor(Color inColor)
        {
            float intensity = this.GetIntensity();
            return new Color(Vector4.Lerp(new Vector4(0.5f, 0.8f, 1f, 1f), inColor.ToVector4(), 1f - intensity));
        }

        public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
        {
            if (maxDepth >= 0f && minDepth < 0f)
            {
                float intensity = this.GetIntensity();

                Texture2D texture2D = Main.rainTexture;
                Microsoft.Xna.Framework.Rectangle rectangle6 = texture2D.Frame(1, 1, 0, 0);

                spriteBatch.Draw(texture2D, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.Green * intensity);
            }
        }

        public override float GetCloudAlpha()
        {
            return 0f;
        }

        public override void Activate(Vector2 position, params object[] args)
        {
            this._isActive = true;
        }

        public override void Deactivate(params object[] args)
        {
            this._isActive = false;
        }

        public override void Reset()
        {
            this._isActive = false;
        }

        public override bool IsActive()
        {
            return this._isActive;
        }
    }
}

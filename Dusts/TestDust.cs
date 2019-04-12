using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace PrimordialSands.Dusts
{
    class TestDust : ModDust
    {
        public override bool Autoload(ref string name, ref string texture)
        {
            texture = null;
            return base.Autoload(ref name, ref texture);
        }

        public override void OnSpawn(Dust dust)
        {
            if (Main.rand.NextFloat() < 0.9078947f)
            {
                Vector2 position = Main.LocalPlayer.Center;
                dust = Terraria.Dust.NewDustDirect(position, 100, 100, 230, 0f, 0.5263162f, 50, new Color(255, 255, 255), 0.9210526f);
                dust.noGravity = true;
                dust.shader = GameShaders.Armor.GetSecondaryShader(96, Main.LocalPlayer);
                dust.fadeIn = 2f;
            }
        }
    }
}
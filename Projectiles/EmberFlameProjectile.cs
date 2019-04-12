using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace PrimordialSands.Projectiles
{
    public class EmberFlameProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ember Flame");
            Main.projFrames[projectile.type] = 3;
        }

        public override void SetDefaults()
        {
            projectile.width = 12;
            projectile.height = 12;
            projectile.penetrate = 3;
            projectile.aiStyle = 14;
            projectile.timeLeft = 300;
            aiType = 400;
            projectile.alpha = 0;
            projectile.magic = true;
            projectile.friendly = true;
            projectile.noEnchantments = true;
        }

        public override void AI()
        {
            int num = projectile.frameCounter + 1;
            projectile.frameCounter = num;
            if (num >= 9)
            {
                projectile.frameCounter = 0;
                num = projectile.frame + 1;
                projectile.frame = num;
                if (num >= 3)
                {
                    projectile.frame = 0;
                }
            }
        }
        

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item66, projectile.position);
            Dust.NewDust(projectile.Center, 0, 0, 127, 0f, 0f, 100, default(Color), 1f);
        }
    }
}
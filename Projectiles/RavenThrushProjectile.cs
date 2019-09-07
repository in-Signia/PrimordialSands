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
    public class RavenThrushProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Raven's Eiserne");
        }

        public override void SetDefaults()
        {
            projectile.width = 42;
            projectile.height = 44;
            projectile.aiStyle = 3;
            projectile.penetrate = 5;
            projectile.timeLeft = 260;
            projectile.thrown = true;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
        }
        public override void AI()
        {
            if (projectile.ai[0] == 0)
            {
                projectile.position.Y = Main.mouseY + Main.screenPosition.Y;
                projectile.ai[1] += 1;
            }
            if (projectile.ai[1] == 200)
            {
                projectile.position.Y = projectile.oldPosition.Y;
            }
        }
    }
}
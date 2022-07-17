using System;
using System.Numerics;

namespace Data.ValueObject
{
    [Serializable]
        public class PlayerData
        {
            public PlayerMovementData MovementData;
            public PlayerThrowForceData ThrowForceData;
        }

        [Serializable]
        public class PlayerMovementData
        {
            public float ForwardSpeed = 5;
            public float SidewaysSpeed = 2;
            public float ForwardForceSpeed = 10;
        }

        [Serializable]
        public class PlayerThrowForceData
        {
            public Vector2 ThrowForce = new Vector2(2, 2);
        }
}
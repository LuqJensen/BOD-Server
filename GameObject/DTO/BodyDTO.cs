﻿using System;

namespace GameObject.DTO
{
    [Serializable]
    public class BodyDTO
    {
        public double Width;
        public double Height;
        public int Type;
        public PointDTO Position;
    }
}
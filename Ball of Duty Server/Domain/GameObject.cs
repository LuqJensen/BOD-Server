﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Ball_of_Duty_Server.Domain
{
    public abstract class GameObject
    {
        public GameObject()
        {
            throw new System.NotImplementedException();
        }

        public int Id
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public View View
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public Physics Physics
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public Body Body
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }
    }
}
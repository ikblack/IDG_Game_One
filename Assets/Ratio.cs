﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDG
{
    public struct Ratio
    {

        private static readonly int precision = 1000;
        private int _ratio;
        public int ToPrecisionInt()
        {
            return _ratio;
        }
        public void SetPrecisionInt(int r)
        {
            _ratio = r; 
        }
        public static Ratio Max(Ratio a,Ratio b)
        {
            return a > b ? a : b;
        }

        public Ratio Abs()
        {
            return new Ratio(Math.Abs(_ratio));
        }
        public static Ratio Lerp(Ratio a, Ratio b, Ratio t)
        {
            return a + (b - a) * t;
        }
        public Ratio(int up, int down)
        {
            _ratio = (up * precision) / down;
        }
        public int ToInt()
        {
            return _ratio / precision;
        }
        public Ratio(float up)
        {
            _ratio = UnityEngine.Mathf.FloorToInt(up * precision);
            
        }
        private Ratio(int precisionRatio)
        {
            _ratio = precisionRatio;

        }
        

        public static Ratio operator +(Ratio a, Ratio b)
        {
            return new Ratio(a._ratio + b._ratio);
        }
        public static Ratio operator +(Ratio a, int b)
        {
            return new Ratio(a._ratio + b*precision);
        }
        public static Ratio operator -(Ratio a, Ratio b)
        {
            return new Ratio(a._ratio - b._ratio);
        }
        public static Ratio operator -(Ratio a, int b)
        {
            return new Ratio(a._ratio - b * precision);
        }
        public static Ratio operator *(Ratio a, Ratio b)
        {
            return new Ratio(a._ratio * b._ratio / precision);
        }
        public static Ratio operator *(Ratio a, int b)
        {
           
            return new Ratio(a._ratio * b );
        }

        public static Ratio operator /(Ratio a, Ratio b)
        {
          
            return new Ratio(a._ratio * precision / b._ratio);
        }
        //public static Ratio operator *(Ratio a, float b)
        //{
        //    return new Ratio(a._ratio *UnityEngine.Mathf.Floor(b));
        //}
        public static Ratio operator /(Ratio a, int b)
        {
           // UnityEngine.Debug.Log("[Ratio] " + a.ToString() + "/ [int] " + b + "=" + new Ratio(a._ratio / b));
            return new Ratio(a._ratio/ b);
        }
        public static Ratio operator %(Ratio a, int b)
        {
            return new Ratio(a._ratio% (b*precision));
        }
        public static bool operator >(Ratio a, Ratio b)
        {
            return a._ratio > b._ratio;
        }
        public static bool operator <(Ratio a, Ratio b)
        {
            return a._ratio < b._ratio;
        }
        public static bool operator <(Ratio a, int b)
        {
            return a._ratio < b*precision;
        }
        public static bool operator >(Ratio a, int b)
        {
            return a._ratio > b * precision;
        }
        public static bool operator <=(Ratio a, int b)
        {
            return a._ratio <= b * precision;
        }
        public static bool operator >=(Ratio a, int b)
        {
            return a._ratio >= b * precision;
        }
        public static bool operator >=(Ratio a, Ratio b)
        {
            return a._ratio >= b._ratio;
        }
        public static bool operator <=(Ratio a, Ratio b)
        {
            return a._ratio <= b._ratio;
        }
        public static bool operator ==(Ratio a, Ratio b)
        {
            return a._ratio == b._ratio;
        }
        public static bool operator ==(Ratio a, int b)
        {
            return a._ratio == b * precision;
        }
        public static bool operator !=(Ratio a, int b)
        {
            return a._ratio != b * precision;
        }
        public static bool operator !=(Ratio a, Ratio b)
        {
            return a._ratio != b._ratio;
        }
        public static Ratio operator -(Ratio a)
        {
            return new Ratio(-a._ratio);
        }
        public float ToFloat()
        {
            return _ratio * 1.0f / precision;
        }

        public override string ToString()
        {
            return ToFloat().ToString();
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public Ratio Sqrt()
        {
            return new Ratio(UnityEngine.Mathf.Sqrt(ToFloat()));
        }
        public Ratio SQR(Ratio a)
        {
            Ratio x = a, y = new Ratio(0f), z = new Ratio(1);
            while (MathR.Abs(x - y) > z)
            {
                y = x;
                x = new Ratio(1,2) * (x + a / x);
            }
            return x;
        }
        

    }

    
}
namespace IDG.TrueRatio
{
    public struct Ratio
    {
        private int _u;//分子
        public int u
        {
            get { return _u; }
            private set { _u = value; }
        }
        private int _d;//分母 
        public int d
        {
            get { return _d; }
            private set
            {
                if (value == 0) { throw new InvalidOperationException("分母不能为零"); }
                else if (value > 0) { _d = value; }
                else { _d = -value; u = -u; }
            }
        }
        //private static Ratio temp = new Ratio(0);
        //public static Ratio Add(Ratio a,Ratio b)
        //{
        //    temp.Reset(0);
        //    temp.Add(a);
        //    temp.Add(b);
        //    return temp;
        //}
        //public static Ratio GetRatio(int up, int down = 1)
        //{
        //    return new Ratio(up, down);
        //}
        public Ratio(int up, int down = 1)
        {
            _u = 0;
            _d = 1;
            u = up;
            d = down;
            Reduction();
        }
        private void Reset(int up, int down = 1)
        {
            u = up;
            d = down;
            Reduction();
        }
        public override string ToString()
        {
            return u + "/" + d;
        }
        private void Reduction()//约分
        {
            int gcd = GCD(u, d);
            u /= gcd;
            d /= gcd;
        }
        private int GCD(int a, int b)
        {
            int temp = 1;
            while (b != 0)
            {
                temp = a % b;
                a = b;
                b = temp;
            }
            return a;
        }
        public static Ratio operator +(Ratio a, Ratio b)
        {
            return new Ratio(a.u * b.d + b.u * a.d, a.d * b.d);
        }
        public static Ratio operator -(Ratio a, Ratio b)
        {
            return new Ratio(a.u * b.d - b.u * a.d, a.d * b.d);
        }
        public static Ratio operator *(Ratio a, Ratio b)
        {
            return new Ratio(a.u * b.u, a.d * b.d);
        }

        public static Ratio operator /(Ratio a, Ratio b)
        {
            return a * !b;
        }
        public static Ratio operator !(Ratio a)
        {
            return new Ratio(a.d, a.u);
        }
        public float ToFloat()
        {
            return u * 1.0f / d;
        }

    }
}

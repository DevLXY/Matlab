using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matlab
{
    public class Matrix
    {

        private double[,] InnerVar;
        #region 属性
        public double this[int row, int col]                                //假如Matrix类实例化之后的名字叫x，则x[]（中括号内输入指定参数）即代表这个属性
        {
            set
            {
                InnerVar[row - 1, col - 1] = value;
            }
            get
            {
                return InnerVar[row - 1, col - 1];
            }
        }



        /// <summary>
        /// 提取矩阵第col列
        /// </summary>
        /// <param name="s">任意字符串（带双引号）</param>
        /// <param name="col">第col列</param>
        /// <returns></returns>
        public Matrix this[string s, int col]                               //Matrix this是double this的重载，重载的返回值可以不一样，但是重载的输入参数必须不一样
        {
            get
            {
                Matrix m = new Matrix(InnerVar.GetLength(0), 1);
                for (int i = 1; i <= InnerVar.GetLength(0); i++)
                {
                    m[i, 1] = this[i, col];                               //this表示这个属性的前面一个重载
                }
                return m;
            }
        }

        public Matrix this[int row, string s]
        {
            get
            {
                Matrix m = new Matrix(1, InnerVar.GetLength(1));
                for (int i = 1; i <= InnerVar.GetLength(1); i++)
                {
                    m[1, i] = this[row, i];
                }
                return m;
            }

        }
        public double this[int i]
        {
            get
            {
                if (this.ColumnCount != 1 && this.RowCount != 1)
                {
                    throw new Exception("不是行向量或列向量");
                }
                else if (this.RowCount == 1)
                {
                    return this[1, i];
                }
                else
                {
                    return this[i, 1];
                }
            }
        }


        public int RowCount
        {
            get
            {
                return InnerVar.GetLength(0);
            }
        }
        public int ColumnCount
        {
            get
            {
                return InnerVar.GetLength(1);
            }
        }

        #endregion 

        public Matrix(int row, int col)                              //Matrix a = new Matrix（int x, int y）,新生成一个x*y的矩阵
        {
            this.InnerVar = new double[row, col];
        }
        public Matrix(int row, int col, double a) : this(row, col)
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    this.InnerVar[i, j] = a;
                }
            }
        }
        public Matrix(double[,] m)                                 //Matrix a = new Matrix（double[,] x）,将二维数组x变为矩阵a                
        {
            this.InnerVar = m;
        }
        public Matrix(double[] m) : this(1, m.Length)                 //Matrix a = new Matrix（double[] x）,将一维数组x变为1行的矩阵，继承自第一个构造函数的重载
        {

            for (int i = 0; i < m.Length; i++)
            {
                InnerVar[0, i] = m[i];                                     //因为继承自第一个构造函数重载，所以double[,] InnerVar在第一个构造函数里已经初始化了，可以直接用
            }
        }




        /// <summary>
        /// 二维矩阵转置
        /// </summary>
        /// <returns>Matrix类</returns>
        public Matrix Transposition()
        {
            Matrix m = new Matrix(InnerVar.GetLength(1), InnerVar.GetLength(0));        //只要不在Matrix类构造函数里实例化Matrix就不会死循环
            for (int i = 1; i <= InnerVar.GetLength(1); i++)
            {
                for (int j = 1; j <= InnerVar.GetLength(0); j++)
                {
                    m[i, j] = this[j, i];                                           //m[i,j]是this属性的调用，this是调用这个方法的Matrix X
                }
            }
            return m;
        }

        /// <summary>
        /// Matrix to double array
        /// </summary>
        /// <returns>double[,]</returns>
        public double[,] ToArray()
        {
            return InnerVar;
        }

        /// <summary>
        /// Matrix to string
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < InnerVar.GetLength(0); i++)
            {
                for (int j = 0; j < InnerVar.GetLength(1); j++)
                {
                    //s += string.Format("{0,10}", InnerVar[i, j]);
                    s += InnerVar[i, j].ToString(".####").PadLeft(10);
                }
                s += "\n";
            }
            return s;
        }

        #region 矩阵加法
        /// <summary>
        /// Override add operator
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>Matrix</returns>
        public static Matrix operator +(Matrix a, Matrix b)
        {
            if (a.RowCount == b.RowCount && a.ColumnCount == b.ColumnCount)
            {
                Matrix m = new Matrix(a.RowCount, a.ColumnCount);
                for (int i = 1; i <= m.RowCount; i++)
                {
                    for (int j = 1; j <= m.ColumnCount; j++)
                    {
                        m[i, j] = a[i, j] + b[i, j];
                    }
                }
                return m;
            }
            else
            {
                throw new Exception("两矩阵无法相加");
            }
        }

        public static Matrix operator +(double a, Matrix b)
        {
            Matrix aa = new Matrix(b.RowCount, b.ColumnCount, a);
            return aa+b;
        }

        public static Matrix operator + (Matrix a, double b)
        {
            return b+a; 
        }
        #endregion

        #region 矩阵减法
        public static Matrix operator -(Matrix a, Matrix b)
        {
            if (a.RowCount == b.RowCount && a.ColumnCount == b.ColumnCount)
            {
                Matrix m = new Matrix(a.RowCount, a.ColumnCount);
                for (int i = 1; i <= m.RowCount; i++)
                {
                    for (int j = 1; j <= m.ColumnCount; j++)
                    {
                        m[i, j] = a[i, j] - b[i, j];
                    }
                }
                return m;
            }
            else
            {
                throw new Exception("两矩阵无法相减");
            }

        }
        public static Matrix operator -(double a, Matrix b)
        {
            Matrix aa = new Matrix(b.RowCount, b.ColumnCount, a);
            return (aa- b);
        }
        public static Matrix operator - (Matrix a, double b)
        {
            Matrix bb = new Matrix(a.RowCount, a.ColumnCount, b);
            return (a-bb);
        }

        #endregion

        #region 矩阵乘法 （还没重写呢）
        public static Matrix MatrixMult(Matrix a, Matrix b)             //输入两个实例化的Matrix类a，b，相乘
        {
            if (a.ColumnCount == b.RowCount)
            {
                Matrix c = new Matrix(a.RowCount, b.ColumnCount);
                for (int i = 1; i <= a.RowCount; i++)
                {
                    for (int j = 1; j <= b.ColumnCount; j++)
                    {

                        for (int k = 1; k <= a.ColumnCount; k++)
                        {
                            c[i, j] += a[i, k] * b[k, j];//////////////////////////////////////
                        }
                    }
                }
                return c;
            }
            else
            {
                throw new Exception("两数组无法相乘");
            }


        }

        public static Matrix MatrixDotMult(Matrix a, Matrix b)
        {
            if (a.ColumnCount == b.ColumnCount && a.RowCount == b.RowCount)
            {
                Matrix m = new Matrix(a.RowCount, b.ColumnCount);
                for (int i = 1; i <= b.RowCount; i++)
                {
                    for (int j = 1; j <= b.ColumnCount; j++)
                    {
                        m[i, j] = a[i, j] * b[i, j];
                    }
                }
                return m;
            }
            else
            {
                throw new Exception("两数组无法相乘");
            }
        }

        public static Matrix MatrixDotMult(double a, Matrix b)
        {
            Matrix aa = new Matrix(b.RowCount, b.ColumnCount, a);

            return MatrixDotMult(aa, b);
        }

        public static Matrix MatrixDotMult(Matrix a, double b)
        {
            return MatrixDotMult(b, a);
        }

        #endregion

        #region 矩阵除法
        public static Matrix operator /(Matrix a, Matrix b)
        {
            if (a.ColumnCount == b.ColumnCount && a.RowCount == b.RowCount)
            {
                Matrix m = new Matrix(a.RowCount, b.ColumnCount);
                for (int i = 1; i <= b.RowCount; i++)
                {
                    for (int j = 1; j <= b.ColumnCount; j++)
                    {
                        m[i, j] = a[i, j] / b[i, j];
                    }
                }
                return m;
            }
            else
            {
                throw new Exception("两矩阵无法相除");
            }
        }

        public static Matrix operator /(double a, Matrix b)
        {
            Matrix aa = new Matrix(b.RowCount, b.ColumnCount, a);

            return (aa/b);
        }

        public static Matrix operator /(Matrix a, double b)
        {
            Matrix bb = new Matrix(a.RowCount, a.ColumnCount, b);

            return (a/ bb);
        }
        #endregion

        #region 矩阵指数运算
        public static Matrix operator ^(Matrix a, Matrix b)
        {
            if (a.RowCount == b.RowCount && a.ColumnCount == b.ColumnCount)
            {
                Matrix m = new Matrix(a.RowCount, a.ColumnCount);
                for (int i = 1; i <= m.RowCount; i++)
                {
                    for (int j = 1; j <= m.ColumnCount; j++)
                    {
                        m[i, j] = Math.Pow(a[i, j], b[i, j]);
                    }
                }
                return m;
            }
            else
            {
                throw new Exception("两矩阵无法进行此指数运算");
            }

        }
        public static Matrix operator^(Matrix a, double b)
        {
            Matrix bb = new Matrix(a.RowCount, a.ColumnCount, b);
            return (a^ bb);
        }
        #endregion

        #region 矩阵等号（传递值而不是传递地址）
        //public static Matrix operator equals(Matrix a, Matrix b)
        //{

        //}
        #endregion

        #region 矩阵布尔判断（比较值而不是比较地址）  ？需要添加matrix a = double b的双等号重载么？
        public static bool operator ==(Matrix a, Matrix b)
        {
            bool c = true ;
            if (a.RowCount == b.RowCount && a.ColumnCount == b.ColumnCount)
            {
                
                for (int i = 1; i <= a.RowCount; i++)
                {
                    for (int j = 1; j <= a.ColumnCount; j++)
                    {
                        
                        if (a[i, j] != b[i, j])
                        {
                            return false;
                        }
                    }
                }
                return c;
            }
            else
            {
                return false;
            }
        }
        public static bool operator !=(Matrix a, Matrix b)
        {
            bool c = false;
            if (a.RowCount == b.RowCount && a.ColumnCount == b.ColumnCount)
            {

                for (int i = 1; i <= a.RowCount; i++)
                {
                    for (int j = 1; j <= a.ColumnCount; j++)
                    {

                        if (a[i, j] != b[i, j])
                        {
                            return true;
                        }
                    }
                }
                return c;
            }
            else
            {
                return true;
            }

        }
        //重写Object.Equals()和Object.GetHashCode().《入门经典》11.2.1最后一部分
        public int val;
        public override bool Equals(object a)
        {
            if(a is Matrix)
            {
                return val == ((Matrix)a).val;
            }
            else
            {
                throw new ArgumentException("Cannot compare Matrix object with object of type" + a.GetType().ToString());
            }
        }
        public override int GetHashCode()
        {
            return val;
        }
        #endregion
    }
}

namespace Hidistro.UI.Common.Controls
{
    using A0CvdP16Is;
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Globalization;
    using System.Reflection;

    public class CustomChart
    {
        public class BarGraph : CustomChart.Chart
        {
            private string A(3CMasI7ZWb;
            private float A)10I)RHI9Lsv;
            private int A0CvdP16Is;
            private float A1n5e4PVCphB;
            private const float AAZ0JeEma(2x58C7QQ)d9L4MH = 15f;
            private const int ABOZyE4bfGcbd5Z8N5SO7b2 = 7;
            private const int AC2pFbvHCa1v4C1DIZ8SngolueQb = 9;
            private const float AD)2Z(NKy = 10f;
            private const float AE)AZJK0MajbU = 5f;
            private Color AFCkJKk;
            private float AGeIcrrVo5pC(snrJL;
            private float AHxCaphXxC5uvu;
            private bool AIYbPEZFPIB(c7C;
            private bool AJK4pBZQRuLk5fMo8iYo;
            private Color AKCpNDo;
            private string ALQRCRR5Is;
            private float AM)mPO1uc;
            private float ANGfB7Jm04qsM;
            private float AOe1)lE0rcBELK1;
            private string APcIraKe5xxvaf;
            private string AQ6jo2Bia3;
            private float AR8sE0qN4pN;
            private float ASlZXEiY7x4L;
            private float ATD5fXUgVGp;
            private float AUllfP6RvXoY;
            private float AVivJ4D0ElWGf;
            private float AW1XO4UolR7IJnrUf;
            private float AXOdS9UDLghHtu;
            private float AYHfyoSxRhu8eJED4H;
            private float AZuGq8iZR;

            public BarGraph()
            {
                this.AQ6jo2Bia3 = string.Empty;
                this.ATD5fXUgVGp = 0f;
                this.APcIraKe5xxvaf = string.Empty;
                this.AR8sE0qN4pN = 0f;
                this.AAZ0JeEma(2x58C7QQ)d9L4MH();
            }

            public BarGraph(Color bgColor)
            {
                this.AQ6jo2Bia3 = string.Empty;
                this.ATD5fXUgVGp = 0f;
                this.APcIraKe5xxvaf = string.Empty;
                this.AR8sE0qN4pN = 0f;
                this.AAZ0JeEma(2x58C7QQ)d9L4MH();
                this.BackgroundColor = bgColor;
            }

            private void AAZ0JeEma(2x58C7QQ)d9L4MH()
            {
                this.AYHfyoSxRhu8eJED4H = 800f;
                this.AXOdS9UDLghHtu = 350f;
                this.ALQRCRR5Is = A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("VgBlAHIAZABhAG4AYQA=");
                this.AFCkJKk = Color.White;
                this.AKCpNDo = Color.Black;
                this.AW1XO4UolR7IJnrUf = 30f;
                this.AHxCaphXxC5uvu = 30f;
                this.A0CvdP16Is = 2;
                this.AJK4pBZQRuLk5fMo8iYo = false;
                this.AIYbPEZFPIB(c7C = false;
            }

            private void ABOZyE4bfGcbd5Z8N5SO7b2(int num1, float single1)
            {
                this.AGeIcrrVo5pC(snrJL = single1 / ((float) (num1 * 2));
                this.AVivJ4D0ElWGf = single1 / ((float) (num1 * 2));
            }

            private void AC2pFbvHCa1v4C1DIZ8SngolueQb()
            {
                this.AKCpNDo();
                this.AQ6jo2Bia3 = this.AQ6jo2Bia3 + A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("MAA=");
                this.ASlZXEiY7x4L = this.AD)2Z(NKy(this.AQ6jo2Bia3, 7, this.FontFamily);
                float num = 5f + this.ASlZXEiY7x4L;
                float num2 = 0f;
                if (this.AJK4pBZQRuLk5fMo8iYo)
                {
                    this.AOe1)lE0rcBELK1 = (20f + this.AR8sE0qN4pN) + 5f;
                    num2 = (15f + this.AOe1)lE0rcBELK1) + 5f;
                }
                else
                {
                    num2 = 5f;
                }
                this.AM)mPO1uc = (this.AXOdS9UDLghHtu - this.AW1XO4UolR7IJnrUf) - this.AHxCaphXxC5uvu;
                this.ANGfB7Jm04qsM = (this.AYHfyoSxRhu8eJED4H - num) - num2;
                this.AZuGq8iZR = num;
                this.A)10I)RHI9Lsv = this.AW1XO4UolR7IJnrUf;
                this.AUllfP6RvXoY = this.ATD5fXUgVGp / this.AM)mPO1uc;
            }

            private float AD)2Z(NKy(string text2, int num1, string text1)
            {
                float width;
                Bitmap image = null;
                Graphics graphics = null;
                Font font = null;
                try
                {
                    font = new Font(text1, (float) num1);
                    image = new Bitmap(1, 1, PixelFormat.Format32bppArgb);
                    graphics = Graphics.FromImage(image);
                    width = graphics.MeasureString(text2, font).Width;
                }
                finally
                {
                    if (graphics != null)
                    {
                        graphics.Dispose();
                    }
                    if (image != null)
                    {
                        image.Dispose();
                    }
                    if (font != null)
                    {
                        font.Dispose();
                    }
                }
                return width;
            }

            private void AE)AZJK0MajbU()
            {
                int num = 0;
                foreach (CustomChart.ChartItem item in base.DataPoints)
                {
                    if (item.Value >= 0f)
                    {
                        item.SweepSize = item.Value / this.AUllfP6RvXoY;
                    }
                    item.StartPos = (this.AVivJ4D0ElWGf / 2f) + (num * (this.AGeIcrrVo5pC(snrJL + this.AVivJ4D0ElWGf));
                    num++;
                }
            }

            private void AFCkJKk()
            {
                float num = 0f;
                this.ATD5fXUgVGp *= 1.1f;
                if (this.ATD5fXUgVGp != 0f)
                {
                    double num2 = Convert.ToDouble(Math.Floor(Math.Log10((double) this.ATD5fXUgVGp)));
                    num = Convert.ToSingle((double) (Math.Ceiling((double) (((double) this.ATD5fXUgVGp) / Math.Pow(10.0, num2))) * Math.Pow(10.0, num2)));
                }
                else
                {
                    num = 1f;
                }
                this.A1n5e4PVCphB = num / ((float) this.A0CvdP16Is);
                double y = Convert.ToDouble(Math.Floor(Math.Log10((double) this.A1n5e4PVCphB)));
                this.A1n5e4PVCphB = Convert.ToSingle((double) (Math.Ceiling((double) (((double) this.A1n5e4PVCphB) / Math.Pow(10.0, y))) * Math.Pow(10.0, y)));
                this.ATD5fXUgVGp = this.A1n5e4PVCphB * this.A0CvdP16Is;
            }

            private void AGeIcrrVo5pC(snrJL(Graphics graphics1)
            {
                SolidBrush brush = null;
                Font font = null;
                StringFormat format = null;
                try
                {
                    brush = new SolidBrush(this.AKCpNDo);
                    font = new Font(this.ALQRCRR5Is, 7f);
                    format = new StringFormat {
                        Alignment = StringAlignment.Center
                    };
                    int num = 0;
                    foreach (CustomChart.ChartItem item in base.DataPoints)
                    {
                        using (SolidBrush brush2 = new SolidBrush(item.ItemColor))
                        {
                            float height = (item.SweepSize == 0f) ? 2f : item.SweepSize;
                            float y = (this.A)10I)RHI9Lsv + this.AM)mPO1uc) - height;
                            graphics1.FillRectangle(brush2, (this.AZuGq8iZR + item.StartPos) + ((this.AGeIcrrVo5pC(snrJL / 2f) - 8f), y, 16f, height);
                            if (this.AIYbPEZFPIB(c7C)
                            {
                                float x = this.AZuGq8iZR + (num * (this.AGeIcrrVo5pC(snrJL + this.AVivJ4D0ElWGf));
                                float num5 = (y - 2f) - font.Height;
                                RectangleF layoutRectangle = new RectangleF(x, num5, this.AGeIcrrVo5pC(snrJL + this.AVivJ4D0ElWGf, (float) font.Height);
                                graphics1.DrawString((item.Value.ToString(CultureInfo.InvariantCulture) == A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("MAAuADAA")) ? A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("MAA=") : item.Value.ToString(CultureInfo.InvariantCulture), font, brush, layoutRectangle, format);
                            }
                            num++;
                        }
                    }
                }
                finally
                {
                    if (brush != null)
                    {
                        brush.Dispose();
                    }
                    if (font != null)
                    {
                        font.Dispose();
                    }
                    if (format != null)
                    {
                        format.Dispose();
                    }
                }
            }

            private void AHxCaphXxC5uvu(Graphics graphics1)
            {
                Font font = null;
                SolidBrush brush = null;
                StringFormat format = null;
                Pen pen = null;
                try
                {
                    font = new Font(this.ALQRCRR5Is, 9f);
                    brush = new SolidBrush(this.AKCpNDo);
                    format = new StringFormat();
                    pen = new Pen(this.AKCpNDo);
                    format.Alignment = StringAlignment.Near;
                    float x = (this.AZuGq8iZR + this.ANGfB7Jm04qsM) + 6f;
                    float y = this.A)10I)RHI9Lsv;
                    float num3 = x + 5f;
                    float num4 = (num3 + 10f) + 5f;
                    float num5 = 0f;
                    int num6 = 0;
                    for (int i = 0; i < base.DataPoints.Count; i++)
                    {
                        CustomChart.ChartItem item = base.DataPoints[i];
                        string s = item.Description + A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("KAA=") + item.Label + A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("KQA=");
                        num5 += font.Height + 5f;
                        float num8 = (y + 5f) + ((i - num6) * (font.Height + 5f));
                        graphics1.DrawString(s, font, brush, num4, num8, format);
                        graphics1.FillRectangle(new SolidBrush(base.DataPoints[i].ItemColor), num3, num8 + 3f, 10f, 10f);
                    }
                    graphics1.DrawRectangle(pen, x, y, this.AOe1)lE0rcBELK1, num5 + 5f);
                }
                finally
                {
                    if (font != null)
                    {
                        font.Dispose();
                    }
                    if (brush != null)
                    {
                        brush.Dispose();
                    }
                    if (format != null)
                    {
                        format.Dispose();
                    }
                    if (pen != null)
                    {
                        pen.Dispose();
                    }
                }
            }

            private void AIYbPEZFPIB(c7C(Graphics graphics1)
            {
                Font font = null;
                SolidBrush brush = null;
                StringFormat format = null;
                Pen pen = null;
                StringFormat format2 = null;
                try
                {
                    font = new Font(this.ALQRCRR5Is, 7f);
                    brush = new SolidBrush(this.AKCpNDo);
                    format = new StringFormat();
                    pen = new Pen(this.AKCpNDo);
                    format2 = new StringFormat();
                    format.Alignment = StringAlignment.Near;
                    RectangleF layoutRectangle = new RectangleF(0f, (this.A)10I)RHI9Lsv - 10f) - font.Height, this.AZuGq8iZR * 2f, (float) font.Height);
                    format2.Alignment = StringAlignment.Center;
                    graphics1.DrawString(this.A(3CMasI7ZWb, font, brush, layoutRectangle, format2);
                    for (int i = 0; i < this.A0CvdP16Is; i++)
                    {
                        float num2 = this.AW1XO4UolR7IJnrUf + ((i * this.A1n5e4PVCphB) / this.AUllfP6RvXoY);
                        float y = num2 - (font.Height / 2);
                        RectangleF ef2 = new RectangleF(5f, y, this.ASlZXEiY7x4L, (float) font.Height);
                        graphics1.DrawString((this.ATD5fXUgVGp - (i * this.A1n5e4PVCphB)).ToString(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("IwAsACMAIwAjAC4AIwAjAA==")), font, brush, ef2, format);
                        graphics1.DrawLine(pen, this.AZuGq8iZR, num2, this.AZuGq8iZR - 4f, num2);
                    }
                    graphics1.DrawLine(pen, this.AZuGq8iZR, this.A)10I)RHI9Lsv, this.AZuGq8iZR, this.A)10I)RHI9Lsv + this.AM)mPO1uc);
                }
                finally
                {
                    if (font != null)
                    {
                        font.Dispose();
                    }
                    if (brush != null)
                    {
                        brush.Dispose();
                    }
                    if (format != null)
                    {
                        format.Dispose();
                    }
                    if (pen != null)
                    {
                        pen.Dispose();
                    }
                    if (format2 != null)
                    {
                        format2.Dispose();
                    }
                }
            }

            private void AJK4pBZQRuLk5fMo8iYo(Graphics graphics1)
            {
                Font font = null;
                SolidBrush brush = null;
                StringFormat format = null;
                Pen pen = null;
                try
                {
                    font = new Font(this.ALQRCRR5Is, 7f);
                    brush = new SolidBrush(this.AKCpNDo);
                    format = new StringFormat();
                    pen = new Pen(this.AKCpNDo);
                    format.Alignment = StringAlignment.Center;
                    graphics1.DrawLine(pen, this.AZuGq8iZR, this.A)10I)RHI9Lsv + this.AM)mPO1uc, this.AZuGq8iZR + this.ANGfB7Jm04qsM, this.A)10I)RHI9Lsv + this.AM)mPO1uc);
                    float y = (this.A)10I)RHI9Lsv + this.AM)mPO1uc) + 2f;
                    float width = this.AGeIcrrVo5pC(snrJL + this.AVivJ4D0ElWGf;
                    int num3 = 0;
                    foreach (CustomChart.ChartItem item in base.DataPoints)
                    {
                        float x = this.AZuGq8iZR + (num3 * width);
                        RectangleF layoutRectangle = new RectangleF(x, y, width, (float) font.Height);
                        string s = this.AJK4pBZQRuLk5fMo8iYo ? item.Label : item.Description;
                        graphics1.DrawString(s, font, brush, layoutRectangle, format);
                        num3++;
                    }
                }
                finally
                {
                    if (font != null)
                    {
                        font.Dispose();
                    }
                    if (brush != null)
                    {
                        brush.Dispose();
                    }
                    if (format != null)
                    {
                        format.Dispose();
                    }
                    if (pen != null)
                    {
                        pen.Dispose();
                    }
                }
            }

            private void AKCpNDo()
            {
                for (int i = 0; i < this.A0CvdP16Is; i++)
                {
                    string str = (this.ATD5fXUgVGp - (i * this.A1n5e4PVCphB)).ToString(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("IwAsACMAIwAjAC4AIwAjAA=="));
                    if (this.AQ6jo2Bia3.Length < str.Length)
                    {
                        this.AQ6jo2Bia3 = str;
                    }
                }
            }

            private string ALQRCRR5Is(string text1)
            {
                string str = text1;
                if (text1.Length > 2)
                {
                    int startIndex = Convert.ToInt32(Math.Floor((double) (text1.Length / 2)));
                    str = text1.Substring(0, 1) + text1.Substring(startIndex, 1) + text1.Substring(text1.Length - 1, 1);
                }
                return str;
            }

            public void CollectDataPoints(string[] values)
            {
                string[] labels = values;
                this.CollectDataPoints(labels, values);
            }

            public void CollectDataPoints(string[] labels, string[] values)
            {
                if (labels.Length != values.Length)
                {
                    throw new Exception(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("WAAgAGQAYQB0AGEAIABjAG8AdQBuAHQAIABpAHMAIABkAGkAZgBmAGUAcgBlAG4AdAAgAGYAcgBvAG0AIABZACAAZABhAHQAYQAgAGMAbwB1AG4AdAA="));
                }
                for (int i = 0; i < labels.Length; i++)
                {
                    float axZF = Convert.ToSingle(values[i]);
                    string label = this.ALQRCRR5Is(labels[i]);
                    base.DataPoints.Add(new CustomChart.ChartItem(label, labels[i], axZF, 0f, 0f, base.GetColor(i)));
                    if (this.ATD5fXUgVGp < axZF)
                    {
                        this.ATD5fXUgVGp = axZF;
                    }
                    if (this.AJK4pBZQRuLk5fMo8iYo)
                    {
                        string str2 = labels[i] + A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("IAAoAA==") + label + A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("KQA=");
                        float num3 = this.AD)2Z(NKy(str2, 9, this.FontFamily);
                        if (this.AR8sE0qN4pN < num3)
                        {
                            this.APcIraKe5xxvaf = str2;
                            this.AR8sE0qN4pN = num3;
                        }
                    }
                }
                this.AFCkJKk();
                this.AC2pFbvHCa1v4C1DIZ8SngolueQb();
                this.ABOZyE4bfGcbd5Z8N5SO7b2(base.DataPoints.Count, this.ANGfB7Jm04qsM);
                this.AE)AZJK0MajbU();
            }

            public override Bitmap Draw()
            {
                int height = Convert.ToInt32(this.AXOdS9UDLghHtu);
                Bitmap image = new Bitmap(Convert.ToInt32(this.AYHfyoSxRhu8eJED4H), height);
                using (Graphics graphics = Graphics.FromImage(image))
                {
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    Rectangle rect = new Rectangle(-1, -1, image.Width + 1, image.Height + 1);
                    LinearGradientBrush brush = new LinearGradientBrush(rect, Color.FromArgb(0xf6, 0xf7, 0xfb), Color.FromArgb(0xdb, 0xe4, 0xeb), LinearGradientMode.Vertical);
                    graphics.FillRectangle(brush, rect);
                    this.AIYbPEZFPIB(c7C(graphics);
                    this.AGeIcrrVo5pC(snrJL(graphics);
                    this.AJK4pBZQRuLk5fMo8iYo(graphics);
                    if (this.AJK4pBZQRuLk5fMo8iYo)
                    {
                        this.AHxCaphXxC5uvu(graphics);
                    }
                }
                return image;
            }

            public Color BackgroundColor
            {
                set
                {
                    this.AFCkJKk = value;
                }
            }

            public int BottomBuffer
            {
                set
                {
                    this.AHxCaphXxC5uvu = Convert.ToSingle(value);
                }
            }

            public Color FontColor
            {
                set
                {
                    this.AKCpNDo = value;
                }
            }

            public string FontFamily
            {
                get
                {
                    return this.ALQRCRR5Is;
                }
                set
                {
                    this.ALQRCRR5Is = value;
                }
            }

            public int Height
            {
                get
                {
                    return Convert.ToInt32(this.AXOdS9UDLghHtu);
                }
                set
                {
                    this.AXOdS9UDLghHtu = Convert.ToSingle(value);
                }
            }

            public bool ShowData
            {
                get
                {
                    return this.AIYbPEZFPIB(c7C;
                }
                set
                {
                    this.AIYbPEZFPIB(c7C = value;
                }
            }

            public bool ShowLegend
            {
                get
                {
                    return this.AJK4pBZQRuLk5fMo8iYo;
                }
                set
                {
                    this.AJK4pBZQRuLk5fMo8iYo = value;
                }
            }

            public int TopBuffer
            {
                set
                {
                    this.AW1XO4UolR7IJnrUf = Convert.ToSingle(value);
                }
            }

            public string VerticalLabel
            {
                get
                {
                    return this.A(3CMasI7ZWb;
                }
                set
                {
                    this.A(3CMasI7ZWb = value;
                }
            }

            public int VerticalTickCount
            {
                get
                {
                    return this.A0CvdP16Is;
                }
                set
                {
                    this.A0CvdP16Is = value;
                }
            }

            public int Width
            {
                get
                {
                    return Convert.ToInt32(this.AYHfyoSxRhu8eJED4H);
                }
                set
                {
                    this.AYHfyoSxRhu8eJED4H = Convert.ToSingle(value);
                }
            }
        }

        public abstract class Chart
        {
            private Color[] AAZ0JeEma(2x58C7QQ)d9L4MH = new Color[] { Color.Chocolate, Color.YellowGreen, Color.Olive, Color.DarkKhaki, Color.Sienna, Color.PaleGoldenrod, Color.Peru, Color.Tan, Color.Khaki, Color.DarkGoldenrod, Color.Maroon, Color.OliveDrab };
            private CustomChart.ChartItemsCollection ABOZyE4bfGcbd5Z8N5SO7b2 = new CustomChart.ChartItemsCollection();

            protected Chart()
            {
            }

            public abstract Bitmap Draw();
            public Color GetColor(int index)
            {
                if (index == 11)
                {
                    return this.AAZ0JeEma(2x58C7QQ)d9L4MH[index];
                }
                return this.AAZ0JeEma(2x58C7QQ)d9L4MH[index % 12];
            }

            public void SetColor(int index, Color NewColor)
            {
                if (index == 11)
                {
                    this.AAZ0JeEma(2x58C7QQ)d9L4MH[index] = NewColor;
                }
                else
                {
                    this.AAZ0JeEma(2x58C7QQ)d9L4MH[index % 12] = NewColor;
                }
            }

            public CustomChart.ChartItemsCollection DataPoints
            {
                get
                {
                    return this.ABOZyE4bfGcbd5Z8N5SO7b2;
                }
                set
                {
                    this.ABOZyE4bfGcbd5Z8N5SO7b2 = value;
                }
            }
        }

        public class ChartItem
        {
            private Color AAZ0JeEma(2x58C7QQ)d9L4MH;
            private string ABOZyE4bfGcbd5Z8N5SO7b2;
            private string AC2pFbvHCa1v4C1DIZ8SngolueQb;
            private float AD)2Z(NKy;
            private float AE)AZJK0MajbU;
            private float AFCkJKk;

            private ChartItem()
            {
            }

            public ChartItem(string label, string desc, float AxZF, float start, float sweep, Color clr)
            {
                this.AC2pFbvHCa1v4C1DIZ8SngolueQb = label;
                this.ABOZyE4bfGcbd5Z8N5SO7b2 = desc;
                this.AFCkJKk = AxZF;
                this.AD)2Z(NKy = start;
                this.AE)AZJK0MajbU = sweep;
                this.AAZ0JeEma(2x58C7QQ)d9L4MH = clr;
            }

            public string Description
            {
                get
                {
                    return this.ABOZyE4bfGcbd5Z8N5SO7b2;
                }
                set
                {
                    this.ABOZyE4bfGcbd5Z8N5SO7b2 = value;
                }
            }

            public Color ItemColor
            {
                get
                {
                    return this.AAZ0JeEma(2x58C7QQ)d9L4MH;
                }
                set
                {
                    this.AAZ0JeEma(2x58C7QQ)d9L4MH = value;
                }
            }

            public string Label
            {
                get
                {
                    return this.AC2pFbvHCa1v4C1DIZ8SngolueQb;
                }
                set
                {
                    this.AC2pFbvHCa1v4C1DIZ8SngolueQb = value;
                }
            }

            public float StartPos
            {
                get
                {
                    return this.AD)2Z(NKy;
                }
                set
                {
                    this.AD)2Z(NKy = value;
                }
            }

            public float SweepSize
            {
                get
                {
                    return this.AE)AZJK0MajbU;
                }
                set
                {
                    this.AE)AZJK0MajbU = value;
                }
            }

            public float Value
            {
                get
                {
                    return this.AFCkJKk;
                }
                set
                {
                    this.AFCkJKk = value;
                }
            }
        }

        public class ChartItemsCollection : CollectionBase
        {
            public int Add(CustomChart.ChartItem value)
            {
                return base.List.Add(value);
            }

            public bool Contains(CustomChart.ChartItem value)
            {
                return base.List.Contains(value);
            }

            public int IndexOf(CustomChart.ChartItem value)
            {
                return base.List.IndexOf(value);
            }

            public void Remove(CustomChart.ChartItem value)
            {
                base.List.Remove(value);
            }

            public CustomChart.ChartItem this[int index]
            {
                get
                {
                    return (CustomChart.ChartItem) base.List[index];
                }
                set
                {
                    base.List[index] = value;
                }
            }
        }

        public class PieChart : CustomChart.Chart
        {
            private const int AAZ0JeEma(2x58C7QQ)d9L4MH = 0x7d;
            private Color ABOZyE4bfGcbd5Z8N5SO7b2;
            private Color AC2pFbvHCa1v4C1DIZ8SngolueQb;
            private ArrayList AD)2Z(NKy;
            private int AE)AZJK0MajbU;
            private float AFCkJKk;
            private string AGeIcrrVo5pC(snrJL;
            private int AHxCaphXxC5uvu;
            private int AIYbPEZFPIB(c7C;
            private int AJK4pBZQRuLk5fMo8iYo;
            private float AKCpNDo;

            public PieChart()
            {
                this.AD)2Z(NKy = new ArrayList();
                this.AJK4pBZQRuLk5fMo8iYo = 250;
                this.ABOZyE4bfGcbd5Z8N5SO7b2 = Color.White;
                this.AC2pFbvHCa1v4C1DIZ8SngolueQb = Color.FromArgb(0x3f, 0x3f, 0x3f);
                this.AFCkJKk = 8f;
                this.AGeIcrrVo5pC(snrJL = "Verdana";
            }

            public PieChart(Color bgColor)
            {
                this.AD)2Z(NKy = new ArrayList();
                this.AJK4pBZQRuLk5fMo8iYo = 250;
                this.ABOZyE4bfGcbd5Z8N5SO7b2 = bgColor;
                this.AC2pFbvHCa1v4C1DIZ8SngolueQb = Color.FromArgb(0x3f, 0x3f, 0x3f);
                this.AFCkJKk = 8f;
                this.AGeIcrrVo5pC(snrJL = "Verdana";
            }

            private void AAZ0JeEma(2x58C7QQ)d9L4MH()
            {
                Font font = new Font(this.AGeIcrrVo5pC(snrJL, this.AFCkJKk);
                this.AE)AZJK0MajbU = font.Height + 5;
                this.AHxCaphXxC5uvu = font.Height * (this.AD)2Z(NKy.Count + 1);
                if (this.AHxCaphXxC5uvu > this.AJK4pBZQRuLk5fMo8iYo)
                {
                    this.AJK4pBZQRuLk5fMo8iYo = this.AHxCaphXxC5uvu;
                }
                this.AIYbPEZFPIB(c7C = this.AJK4pBZQRuLk5fMo8iYo + 0x7d;
            }

            public void CollectDataPoints(string[] xValues, string[] yValues)
            {
                this.AKCpNDo = 0f;
                for (int i = 0; i < xValues.Length; i++)
                {
                    float axZF = Convert.ToSingle(yValues[i]);
                    this.AD)2Z(NKy.Add(new CustomChart.ChartItem(xValues[i], xValues.ToString(), axZF, 0f, 0f, Color.AliceBlue));
                    this.AKCpNDo += axZF;
                }
                float num3 = 0f;
                int num4 = 0;
                foreach (CustomChart.ChartItem item in this.AD)2Z(NKy)
                {
                    item.StartPos = num3;
                    item.SweepSize = (item.Value / this.AKCpNDo) * 360f;
                    num3 = item.StartPos + item.SweepSize;
                    item.ItemColor = base.GetColor(num4++);
                }
                this.AAZ0JeEma(2x58C7QQ)d9L4MH();
            }

            public override Bitmap Draw()
            {
                int width = this.AJK4pBZQRuLk5fMo8iYo;
                Rectangle rect = new Rectangle(0, 0, width, width - 1);
                Bitmap image = new Bitmap(width + this.AIYbPEZFPIB(c7C, width);
                Graphics graphics = null;
                StringFormat format = null;
                SolidBrush brush = null;
                try
                {
                    graphics = Graphics.FromImage(image);
                    format = new StringFormat();
                    graphics.FillRectangle(new SolidBrush(this.ABOZyE4bfGcbd5Z8N5SO7b2), 0, 0, width + this.AIYbPEZFPIB(c7C, width);
                    format.Alignment = StringAlignment.Far;
                    for (int i = 0; i < this.AD)2Z(NKy.Count; i++)
                    {
                        CustomChart.ChartItem item = (CustomChart.ChartItem) this.AD)2Z(NKy[i];
                        using ((SolidBrush) (brush = null))
                        {
                            brush = new SolidBrush(item.ItemColor);
                            graphics.FillPie(brush, rect, item.StartPos, item.SweepSize);
                            graphics.FillRectangle(brush, width + 0x7d, (i * this.AE)AZJK0MajbU) + 15, 10, 10);
                            graphics.DrawString(item.Label, new Font(this.AGeIcrrVo5pC(snrJL, this.AFCkJKk), new SolidBrush(Color.Black), (float) ((width + 0x7d) + 20), (float) ((i * this.AE)AZJK0MajbU) + 13));
                            graphics.DrawString(item.Value.ToString(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("QwA=")), new Font(this.AGeIcrrVo5pC(snrJL, this.AFCkJKk), new SolidBrush(Color.Black), (float) ((width + 0x7d) + 200), (float) ((i * this.AE)AZJK0MajbU) + 13), format);
                        }
                    }
                    graphics.DrawEllipse(new Pen(this.AC2pFbvHCa1v4C1DIZ8SngolueQb, 2f), rect);
                    graphics.DrawRectangle(new Pen(this.AC2pFbvHCa1v4C1DIZ8SngolueQb, 1f), (width + 0x7d) - 10, 10, 220, (this.AD)2Z(NKy.Count * this.AE)AZJK0MajbU) + 0x19);
                    graphics.DrawString(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("VABvAHQAYQBsAA=="), new Font(this.AGeIcrrVo5pC(snrJL, this.AFCkJKk, FontStyle.Bold), new SolidBrush(Color.Black), (float) ((width + 0x7d) + 30), (float) ((this.AD)2Z(NKy.Count + 1) * this.AE)AZJK0MajbU), format);
                    graphics.DrawString(this.AKCpNDo.ToString(A0CvdP16Is.AC2pFbvHCa1v4C1DIZ8SngolueQb.AGeIcrrVo5pC(snrJL("QwA=")), new Font(this.AGeIcrrVo5pC(snrJL, this.AFCkJKk, FontStyle.Bold), new SolidBrush(Color.Black), (float) ((width + 0x7d) + 200), (float) ((this.AD)2Z(NKy.Count + 1) * this.AE)AZJK0MajbU), format);
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;
                }
                finally
                {
                    if (format != null)
                    {
                        format.Dispose();
                    }
                    if (graphics != null)
                    {
                        graphics.Dispose();
                    }
                }
                return image;
            }
        }
    }
}


using Hidistro.ControlPanel.Commodities;
using Hidistro.ControlPanel.Store;
using Hidistro.Core;
using Hidistro.Entities.Commodities;
using Hidistro.Entities.Store;
using Hidistro.UI.Common.Controls;
using Hishop.Components.Validation;
using kindeditor.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hidistro.UI.Web.Admin
{
    [PrivilegeCheck(Privilege.EditProducts)]
    public class EditProductLetter : ProductBasePage
    {

        protected string ReUrl = "productonsales.aspx";
        protected System.Web.UI.WebControls.Button btnSave;
        protected System.Web.UI.WebControls.CheckBox ckbIsDownPic;
        protected KindeditorControl fckProductLetter;
        private int productId;
        protected Script Script1;
        protected Script Script2;
        //private string toline = "";
        protected TrimTextBox txtProductName;
        protected TrimTextBox txtProductShortLetter;
        protected ProductFlashUpload ucFlashUpload1;

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            // 获取文案的上传图片
            string str1 = this.ucFlashUpload1.Value.Trim();
            string[] strArrays = str1.Split(new char[] { ',' });
            string[] strArrays1 = new string[] { "", "", "", "", "" };
            string[] strArrays2 = strArrays1;
            for (int i = 0; i < (int)strArrays.Length && i < 5; i++)
            {
                strArrays2[i] = strArrays[i];
            }

            // 获取文案内容
            string text = this.fckProductLetter.Text;
            // 下载文案内容中的图片
            if (this.ckbIsDownPic.Checked)
            {
                text = base.DownRemotePic(text);
            }

            // 设置商品需要更新的信息
            ProductInfo target = new ProductInfo
            {
                ProductId = this.productId,
                LetterImgUrl1 = strArrays2[0],
                LetterImgUrl2 = strArrays2[1],
                LetterImgUrl3 = strArrays2[2],
                LetterImgUrl4 = strArrays2[3],
                LetterImgUrl5 = strArrays2[4],
                LetterTbUrl40 = strArrays2[0].Replace("/images/", "/thumbs40/40_"),
                LetterTbUrl60 = strArrays2[0].Replace("/images/", "/thumbs60/60_"),
                LetterTbUrl100 = strArrays2[0].Replace("/images/", "/thumbs100/100_"),
                LetterTbUrl160 = strArrays2[0].Replace("/images/", "/thumbs160/160_"),
                LetterTbUrl180 = strArrays2[0].Replace("/images/", "/thumbs180/180_"),
                LetterTbUrl220 = strArrays2[0].Replace("/images/", "/thumbs220/220_"),
                LetterTbUrl310 = strArrays2[0].Replace("/images/", "/thumbs310/310_"),
                LetterTbUrl410 = strArrays2[0].Replace("/images/", "/thumbs410/410_"),
                ProductShortLetter = this.txtProductShortLetter.Text,
                ProductLetter = (!string.IsNullOrEmpty(text) && text.Length > 0) ? text : null
            };

            bool updateFlag = ProductHelper.UpdateProductLetter(target);

            if (updateFlag)
            {
                // 更新成功
                if (base.Request.QueryString["reurl"] != null)
                {
                    this.ReUrl = base.Request.QueryString["reurl"].ToString();
                }
                this.ShowMsgAndReUrl("修改商品文案信息成功", true, this.ReUrl);
                this.ShowMsg("修改商品文案信息成功", true);
            }
            else
            {
                this.ShowMsg("修改商品文案信息失败", false);
            }

        }
        private void LoadProduct(ProductInfo product, System.Collections.Generic.Dictionary<int, System.Collections.Generic.IList<int>> attrs)
        {
            this.txtProductName.Text = product.ProductName;
            this.txtProductShortLetter.Text = product.ProductShortLetter;
            this.fckProductLetter.Text = product.ProductLetter;

            string[] imageUrl1 = new string[] { product.LetterImgUrl1, ",", product.LetterImgUrl2, ",", product.LetterImgUrl3, ",", product.LetterImgUrl4, ",", product.LetterImgUrl5 };
            string str4 = string.Concat(imageUrl1);
            ProductFlashUpload productFlashUpload = this.ucFlashUpload1;
            string str5 = str4.Replace(",,", ",").Replace(",,", ",");
            char[] chrArray = new char[] { ',' };
            productFlashUpload.Value = str5.Trim(chrArray);            
            
        }
        protected override void OnInitComplete(System.EventArgs e)
        {
            base.OnInitComplete(e);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
        }
        protected void Page_Load(object sender, System.EventArgs e)
        {
            int.TryParse(base.Request.QueryString["productId"], out this.productId);
            if (!this.Page.IsPostBack)
            {
                System.Collections.Generic.IList<int> tagsId = null;
                System.Collections.Generic.Dictionary<int, System.Collections.Generic.IList<int>> dictionary;
                ProductInfo product = ProductHelper.GetProductDetails(this.productId, out dictionary, out tagsId);
                if (product == null)
                {
                    base.GotoResourceNotFound();
                }
                else
                {
                    this.LoadProduct(product, dictionary);
                }
            }
        }
        
    }
}

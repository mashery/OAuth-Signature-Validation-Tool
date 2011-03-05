using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OAuthSig
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            OAuth.OAuthBase.SignatureTypes signatureType = OAuth.OAuthBase.SignatureTypes.HMACSHA1;
            string normalizedUrl;
            string normalizedRequestParameters;
            OAuth.OAuthBase myOAuth = new OAuth.OAuthBase();

            try
            {
                Uri uri = new Uri(txtURI.Text);
                string consumerKey = txtConsKey.Text;
                string consumerSecret = txtConsSecret.Text;
                string token = txtToken.Text;
                string tokenSecret = txtTokenSecret.Text;
                string httpMethod = drpHTTPMethod.SelectedItem.ToString();
                string timeStamp = txtTimestamp.Text;
                string nonce = txtNonce.Text;

                if (string.IsNullOrEmpty(timeStamp))
                {
                    timeStamp = myOAuth.GenerateTimeStamp();
                    txtTimestamp.Text = timeStamp;
                }

                if (string.IsNullOrEmpty(nonce))
                {
                    nonce = myOAuth.GenerateNonce();
                    txtNonce.Text = nonce;
                }

                switch (drpSigMethod.SelectedIndex)
                {
                    case 0:
                        signatureType = OAuth.OAuthBase.SignatureTypes.HMACSHA1;
                        break;
                    case 1:
                        signatureType = OAuth.OAuthBase.SignatureTypes.PLAINTEXT;
                        break;
                    case 2:
                        signatureType = OAuth.OAuthBase.SignatureTypes.RSASHA1;
                        break;
                }

                myOAuth.includeVersion = chkVersion.Checked;

                string signature = myOAuth.GenerateSignature(uri, consumerKey, consumerSecret,
                    token, tokenSecret, httpMethod, timeStamp, nonce, signatureType,
                    out normalizedUrl, out normalizedRequestParameters);

                txtRawSig.Text = signature;
                txtEncodedSig.Text = myOAuth.UrlEncode(signature);

                txtGenURL.Text = normalizedUrl + "?" + normalizedRequestParameters + "&oauth_signature=" + txtEncodedSig.Text;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            drpSigMethod.SelectedIndex = 0;
            drpHTTPMethod.SelectedIndex = 0;
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }
    }
}

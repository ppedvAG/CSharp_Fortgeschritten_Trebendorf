using Contracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookPreviewPlugin
{
    public class BookPreview : IBookPlugin
    {
        //nur notwendig ohne Lamdba
        private IBook _book;

        public void AddFurtherBookItems(object panel, IBook book)
        {
            _book = book;
            Button button = new Button();
            button.Text = "Zeige Vorschau";
            button.Tag = book;
            //button.Click += Button_Click;
            button.Click += (sender, e) => Process.Start(book.PreviewURL);

            if(panel is FlowLayoutPanel flowLayoutPanel)
            {
                flowLayoutPanel.Controls.Add(button);
            }
        }
        #region Variante ohne Lambda
        private void Button_Click(object sender, EventArgs e)
        {
            if (sender is Button button && button.Tag is IBook book)
            {
                Process.Start(book.PreviewURL);
            }
            //Process.Start(_book.PreviewURL);
        }
        #endregion

    }
}

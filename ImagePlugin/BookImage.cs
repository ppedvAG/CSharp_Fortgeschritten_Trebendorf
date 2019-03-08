using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImagePlugin
{
    class BookImage : IBookPlugin
    {
        public void AddFurtherBookItems(object panel, IBook book)
        {
            PictureBox thumbnailBox = new PictureBox();
            thumbnailBox.ImageLocation = book.ImageURL;
            thumbnailBox.Height = 100;
            thumbnailBox.SizeMode = PictureBoxSizeMode.StretchImage;

            if(panel is FlowLayoutPanel flowLayoutPanel)
            {
                flowLayoutPanel.Controls.Add(thumbnailBox);
            }

        }
    }
}

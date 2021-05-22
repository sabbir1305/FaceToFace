using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FacesApiTest
{
    public class ImageUtility
    {
        public byte[] ConvertToBytes(string imgPath)
        {
            using (MemoryStream memStream = new MemoryStream())
            {
                using (FileStream stream = new FileStream(imgPath, FileMode.Open))
                {
                    stream.CopyTo(memStream);
                }
                var bytes = memStream.ToArray();

                return bytes;
            }
        }

        public void FromBytesToImage(byte[] imageBytes,string fileName)
        {
            using (var ms = new MemoryStream(imageBytes))
            {
                Image img = Image.FromStream(ms);
                img.Save(fileName + ".jpg", ImageFormat.Jpeg);

            }
        }
    }
}

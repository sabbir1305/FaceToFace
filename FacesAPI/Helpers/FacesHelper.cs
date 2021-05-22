using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FacesAPI.Helpers
{
    public class FacesHelper : IFacesHelper
    {
        public List<byte[]> GetFaces(byte[] image)
        {
            Mat src = Cv2.ImDecode(image, ImreadModes.Color);
            src.SaveImage("image.jpg", new ImageEncodingParam(ImwriteFlags.JpegProgressive, 255));
            var file = Path.Combine(Directory.GetCurrentDirectory(), "CascadeFile", "haarcascade_frontalface_default.xml");
            var faceCascade = new CascadeClassifier();
            faceCascade.Load(file);

            var faces = faceCascade.DetectMultiScale(src, 1.1, 6, HaarDetectionTypes.DoRoughSearch, new Size(60, 60));

            var faceList = new List<byte[]>();
            int j = 0;
            foreach (var rect in faces)
            {
                var faceImage = new Mat(src, rect);

                faceList.Add(faceImage.ToBytes(".jpg"));
                faceImage.SaveImage("face" + j + ".jpg", new ImageEncodingParam(ImwriteFlags.JpegProgressive, 255));
                j++;
            }
            return faceList;

        }
    }
}

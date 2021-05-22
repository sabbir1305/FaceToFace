using System.Collections.Generic;

namespace FacesAPI.Helpers
{
    public interface IFacesHelper
    {
        List<byte[]> GetFaces(byte[] image);
    }
}
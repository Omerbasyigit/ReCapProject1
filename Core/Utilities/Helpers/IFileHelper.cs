using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Helpers
{
    public interface IFileHelper
    {
        //filePath: 'CarImageManager'dan gelen dosyanın kaydedildiği adres ve adı
        //IFormFile: HttpRequest ile gönderilen bir dosyayı temsil eder.
        //string root: Bu dosyanın kaydedileceği adresi tutar. 
        void Delete(string filePath);
        string Upload(IFormFile file, string root);
        string Update(IFormFile file, string filePath, string root);

    }
}

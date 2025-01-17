﻿using Microsoft.AspNetCore.Components.Forms;

namespace RoyalVilla_server.Service.IService
{
    public interface IFileUpload
    {
        Task<string> UploadFile(IBrowserFile file);
        bool DeleteFile(string filename);
    }
}

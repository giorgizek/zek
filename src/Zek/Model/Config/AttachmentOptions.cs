﻿namespace Zek.Model.Config
{
    public class AttachmentOptions
    {
        public string Directory { get; set; }
        public long MaxFileSize { get; set; }
        public string[] AllowedExtensions { get; set; }
    }
}

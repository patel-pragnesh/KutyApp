﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KutyApp.Client.Services.ServiceCollector.Interfaces;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions.Abstractions;

namespace KutyApp.Client.Services.ServiceCollector.Implementations
{
    public class MediaManager : IMediaManager
    {
        IPermissionManager PermissionManager { get; }

        public MediaManager(IPermissionManager pm)
        {
            PermissionManager = pm;
        }

        public async Task<MediaFile> PickPhotoAsync(PickMediaOptions options = null)
        {
            if (await PermissionManager.CheckAndGrantPermissionsAsyc(new List<Permission> { Permission.Camera, Permission.Storage }))
            {
                    var file = await CrossMedia.Current.PickPhotoAsync(options ?? new PickMediaOptions
                    {
                        PhotoSize = PhotoSize.Medium
                    });
                    if (file != null)
                        return file;
            }

            throw new System.Exception("perm");
        }

        public async Task<MediaFile> TakePhotoAsync(StoreCameraMediaOptions options = null)
        {
            throw new NotImplementedException();
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                throw new System.Exception("support");

            try
            {
                var file = await CrossMedia.Current.TakePhotoAsync(options ?? new StoreCameraMediaOptions
                {
                    SaveToAlbum = true,
                    //Name = $"{DateTime.Now}Tmp.jpg",
                    CompressionQuality = 75,
                    //CustomPhotoSize = 50,
                    //PhotoSize = PhotoSize.MaxWidthHeight,
                    //MaxWidthHeight = 2000,
                });

                if (file != null)
                    return file;
            }
            catch (Exception e)
            {

                throw;
            }

            throw new Exception("nul");
        }
    }
}

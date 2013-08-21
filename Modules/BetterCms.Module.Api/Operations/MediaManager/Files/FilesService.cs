﻿using System.Linq;

using BetterCms.Core.DataAccess;
using BetterCms.Module.Api.Helpers;
using BetterCms.Module.Api.Infrastructure;
using BetterCms.Module.MediaManager.Models;
using BetterCms.Module.MediaManager.Services;

using ServiceStack.ServiceInterface;

namespace BetterCms.Module.Api.Operations.MediaManager.Files
{
    public class FilesService : Service, IFilesService
    {
        private readonly IRepository repository;

        private readonly IMediaFileService fileService;

        public FilesService(IRepository repository, IMediaFileService fileService)
        {
            this.repository = repository;
            this.fileService = fileService;
        }

        public GetFilesResponse Get(GetFilesRequest request)
        {
            request.Data.SetDefaultOrder("Title");

            var query = repository.AsQueryable<Media>()
                .Where(m => m.Original == null && m.Folder.Id == request.Data.FolderId && m.Type == MediaType.File);

            if (!request.Data.IncludeFolders)
            {
                query = query.Where(media => media.ContentType != Module.MediaManager.Models.MediaContentType.Folder);
            }

            if (!request.Data.IncludeFiles)
            {
                query = query.Where(media => media.ContentType != Module.MediaManager.Models.MediaContentType.File);
            }

            if (!request.Data.IncludeArchived)
            {
                query = query.Where(m => !m.IsArchived);
            }

            query = query.ApplyTagsFilter(
                request.Data,
                tagName => { return media => media.MediaTags.Any(tag => tag.Tag.Name == tagName); });

            var listResponse = query.Select(media => new MediaModel
                        {
                            Id = media.Id,
                            Version = media.Version,
                            CreatedBy = media.CreatedByUser,
                            CreatedOn = media.CreatedOn,
                            LastModifiedBy = media.ModifiedByUser,
                            LastModifiedOn = media.ModifiedOn,

                            Title = media.Title,
                            MediaContentType = media is MediaFolder 
                                                    ? (MediaContentType)((int)MediaContentType.Folder) 
                                                    : (MediaContentType)((int)MediaContentType.File),
                            FileExtension = media is MediaFile ? ((MediaFile)media).OriginalFileExtension : null,
                            FileSize = media is MediaFile ? ((MediaFile)media).Size : (long?)null,
                            FileUrl = media is MediaFile ? ((MediaFile)media).PublicUrl : null,
                            IsArchived = media.IsArchived,
                            ThumbnailCaption = media.Image != null ? media.Image.Caption : null,
                            ThumbnailUrl = media.Image != null ? media.Image.PublicThumbnailUrl : null,
                            ThumbnailId = media.Image != null ? media.Image.Id : (System.Guid?)null

                        }).ToDataListResponse(request);

            listResponse.Items.ToList().ForEach(media =>
                {
                    if (media.MediaContentType == MediaContentType.File)
                    {
                        media.FileUrl = fileService.GetDownloadFileUrl(MediaType.File, media.Id, media.FileUrl);
                    }
                });

            return new GetFilesResponse
                       {
                           Data = listResponse
                       };
        }
    }
}
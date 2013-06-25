﻿using System.Collections.Generic;
using System.Runtime.Serialization;

using BetterCms.Module.WebApi.Models.MediaManager;

namespace BetterCms.Module.WebApi.Models.Blog.GetBlogPostPropertiesById
{
    [DataContract]
    public class GetBlogPostPropertiesByIdResponse : ResponseBase<BlogPostPropertiesModel>
    {
        /// <summary>
        /// Gets or sets the layout.
        /// </summary>
        /// <value>
        /// The layout.
        /// </value>
        [DataMember(Order = 20, Name = "layout")]
        public LayoutModel Layout { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        [DataMember(Order = 30, Name = "category")]
        public CategoryModel Category { get; set; }

        /// <summary>
        /// Gets or sets the author.
        /// </summary>
        /// <value>
        /// The author.
        /// </value>
        [DataMember(Order = 10, Name = "author")]
        public AuthorModel Author { get; set; }

        /// <summary>
        /// Gets or sets the list of tags.
        /// </summary>
        /// <value>
        /// The list of tags.
        /// </value>
        [DataMember(Order = 50, Name = "tags")]
        public List<TagModel> Tags { get; set; }

        /// <summary>
        /// Gets or sets the main image.
        /// </summary>
        /// <value>
        /// The main image.
        /// </value>
        [DataMember(Order = 60, Name = "mainImage")]
        public ImageModel MainImage { get; set; }

        /// <summary>
        /// Gets or sets the featured image.
        /// </summary>
        /// <value>
        /// The featured image.
        /// </value>
        [DataMember(Order = 70, Name = "featuredImage")]
        public ImageModel FeaturedImage { get; set; }

        /// <summary>
        /// Gets or sets the secondary image.
        /// </summary>
        /// <value>
        /// The secondary image.
        /// </value>
        [DataMember(Order = 80, Name = "secondaryImage")]
        public ImageModel SecondaryImage { get; set; }   
    }
}